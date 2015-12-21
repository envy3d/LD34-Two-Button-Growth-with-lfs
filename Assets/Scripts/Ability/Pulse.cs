using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour
{
    public AnimationCurve pulseForce;
    public Animator pulseAnim;

    private bool pulseActive = false;

    void Start()
    {
        //pulseAnim.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (pulseActive)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float force = pulseForce.Evaluate((other.transform.position - transform.position).magnitude);
                rb.AddForce((other.transform.position - transform.position).normalized * force, ForceMode.Impulse);
            }
        }
    }

    public void ActivatePulse()
    {
        pulseActive = true;
        //pulseAnim.enabled = true;
        pulseAnim.SetTrigger("action");
        StartCoroutine(DeactivatePulse());
    }

    private IEnumerator DeactivatePulse()
    {
        //Debug.Log("Pulse Start");
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        pulseActive = false;
        //pulseAnim.enabled = false;
        //Debug.Log("Pulse End");
    }
}
