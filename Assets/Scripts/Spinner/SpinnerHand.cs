using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class SpinnerHand : MonoBehaviour
{
    public float spinBaseSpeed = 60;
    public float boostedSpinSpeedModifier = 2;
    public float spinnerBoostedTime = 3;
    public float deactivationDuration = 1;
    public Animator spinnerSelectionAnim;
    public AudioSource audioPulse;

    public UnityEvent PulseEvent;
    public UnityEvent BoostEvent;
    public UnityEvent BoomerangEvent;

    private float spinnerAngle = 0;
    private float spinSpeedModifier = 1;
    private bool deactivated = false;
    private List<SpinnerHand> enemySpinners;

    void Start()
    {
        audioPulse = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (deactivated == false)
        {
            float spinSpeed = spinBaseSpeed * spinSpeedModifier;
            spinnerAngle += spinSpeed * Time.deltaTime;

            transform.rotation = Quaternion.AngleAxis(spinnerAngle, new Vector3(0, 0, 1));

            //print(transform.rotation.eulerAngles.z.ToString("#.00"));
        }
    }

    public void UseAbility()
    {
        if (deactivated == false)
        {
            deactivated = true;
            float angle = transform.rotation.eulerAngles.z;
            
            if (angle >= 0 && angle < 90)
            {
                spinnerSelectionAnim.gameObject.SetActive(true);
                spinnerSelectionAnim.transform.rotation = Quaternion.Euler(0, 0, 0);
                BoostEvent.Invoke();
            }
            else if (angle >= 90 && angle < 180)
            {
                spinnerSelectionAnim.gameObject.SetActive(true);
                spinnerSelectionAnim.transform.rotation = Quaternion.Euler(0, 0, 90);
                BoomerangEvent.Invoke();
            }
            else if (angle >= 180 && angle < 270)
            {
                spinnerSelectionAnim.gameObject.SetActive(true);
                spinnerSelectionAnim.transform.rotation = Quaternion.Euler(0, 0, 180);
                audioPulse.Play();
                PulseEvent.Invoke();
            }
            else if (angle >= 270)
            {
                spinnerSelectionAnim.gameObject.SetActive(true);
                spinnerSelectionAnim.transform.rotation = Quaternion.Euler(0, 0, 270);
                SpinnerSpeedAttack();
            }

            StartCoroutine(deactivationTimer());
        }
    }

    private void PrepSpinnerAttack()
    {
        if (enemySpinners == null)
        {
            enemySpinners = new List<SpinnerHand>();
        }
        enemySpinners.Clear();
        GameObject[] spinners = GameObject.FindGameObjectsWithTag("Spinner");
        foreach (GameObject go in spinners)
        {
            if (go != this.gameObject)
            {
                SpinnerHand sh = go.GetComponent<SpinnerHand>();
                if (sh != null)
                {
                    enemySpinners.Add(sh);
                }
            }
        }
    }

    private void SpinnerSpeedAttack()
    {
        if (enemySpinners == null)
        {
            PrepSpinnerAttack();
        }
        foreach (SpinnerHand sh in enemySpinners)
        {
            sh.BoostSpinnerSpeed();
        }
    }

    private IEnumerator deactivationTimer()
    {
        yield return new WaitForSeconds(deactivationDuration);
        deactivated = false;
        spinnerSelectionAnim.gameObject.SetActive(false);
    }

    public void BoostSpinnerSpeed()
    {
        spinSpeedModifier = boostedSpinSpeedModifier;
        StopCoroutine(RemoveSpinnerSpeedBoost());
        StartCoroutine(RemoveSpinnerSpeedBoost());
    }

    private IEnumerator RemoveSpinnerSpeedBoost()
    {
        yield return new WaitForSeconds(spinnerBoostedTime);
        spinSpeedModifier = 1;
    }
}
