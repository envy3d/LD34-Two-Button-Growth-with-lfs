using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class ProgressionTracker : MonoBehaviour
{
    public BezierSpline trackSpline;
    public int numberOfLaps = 3;
    public VehicleProgressionInfo[] progressInfo;
    public float maxScale = 3;
    public float distanceForMaxScale = 0.6f;

    public AudioClip announceLap2;
    public AudioClip announceLap3;
    public AudioSource lap2Music;
    public AudioSource lap3Music;
    public AnimationCurve musicFadeInCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public float musicFadeInTime = 0.5f;
    public float musicVolume = 0.5f;
    public float preGameTime = 4;
    public float postGameTime = 3;

    private List<float> totalProgressRank = new List<float>();
    private int currentHighestLap = 0;
    private AudioSource audioSource;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (progressInfo != null && trackSpline != null)
        {
            foreach (VehicleProgressionInfo pt in progressInfo)
            {
                pt.transform = pt.vehicleController.transform;
                pt.UpdateProgress(0);
            }
        }
        StartCoroutine(StartGame());
	}

	void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Title");
        }
        if (progressInfo != null && trackSpline != null)
        {
            totalProgressRank.Clear();
            foreach (VehicleProgressionInfo pt in progressInfo)
            {
                float prevProgress = pt.progress;
                float progress = GetSplineDistanceToNearestPoint(pt.transform.position, pt.progress, 10, 0.01f, 5);
                if (prevProgress < 0.1 && progress > 0.3)
                {
                    progress = 0;
                }
                else if (prevProgress > 0.9 && progress < 0.1)
                {
                    pt.currentLap += 1;
                    if (pt.currentLap > currentHighestLap)
                    {
                        currentHighestLap = pt.currentLap;
                        if (currentHighestLap == 1)
                        {
                            StartCoroutine(FadeInMusic(lap2Music));
                            audioSource.clip = announceLap2;
                            audioSource.Play();
                        }
                        else if (currentHighestLap == 2)
                        {
                            StartCoroutine(FadeInMusic(lap3Music));
                            audioSource.clip = announceLap3;
                            audioSource.Play();
                        }
                        else if (currentHighestLap == 3)
                        {
                            pt.vehicleController.PlayAudio(pt.vehicleController.audioWin);
                        }
                    }
                }
                pt.UpdateProgress(progress);

                totalProgressRank.Add(pt.totalProgress);
            }

            totalProgressRank.Sort();
            totalProgressRank.Reverse();
            float avgTotalProgress = 0;
            for (int i = 0; i < totalProgressRank.Count; i++)
            {
                avgTotalProgress += totalProgressRank[i];
            }
            avgTotalProgress /= totalProgressRank.Count + 1;

            foreach (VehicleProgressionInfo pt in progressInfo)
            {
                for (int i = 0; i < totalProgressRank.Count; i++)
                {
                    //print(totalProgressRank[i]);
                    if (pt.totalProgress == totalProgressRank[i])
                    {
                        int prevRank = pt.rank;
                        pt.rank = i;
                        pt.rankText.ChangeRank(pt.rank);

                        if (prevRank != 0 && pt.rank == 0)
                        {
                            pt.vehicleController.PlayAudio(pt.vehicleController.audioSuccess);
                        }
                        float newScale = ((pt.totalProgress - avgTotalProgress) * maxScale) / distanceForMaxScale;
                        newScale = Mathf.Clamp(newScale, 1, maxScale);
                        pt.vehicleController.SetVehicleScale(newScale);
                        break;
                    }
                }
                if (pt.currentLap == 3)
                {
                    // End game

                    foreach (VehicleProgressionInfo pj in progressInfo)
                    {
                        pj.vehicleController.EndCar();
                        StartCoroutine(EndGame());
                    }
                }
            }
        }
    }

    public float GetSplineDistanceToNearestPoint(Vector3 point, float t, int numberOfSamples,
                                                 float sampleSpacing, int recursionDepth)
    {
        if (recursionDepth == 0)
        {
            return t;
        }

        float startSample = t - (numberOfSamples / 2.0f) * sampleSpacing;
        startSample = startSample < 0 ? 1 + startSample : startSample;

        float smallestSampleDistance = float.MaxValue;
        float bestSample = 0;

        float nextSample;
        for (int i = 0; i < numberOfSamples; i++)
        {
            nextSample = startSample + (i * sampleSpacing);
            nextSample = nextSample > 1 ? nextSample - 1 : nextSample;
            float distance = (trackSpline.GetPoint(nextSample) - point).sqrMagnitude;
            if (distance < smallestSampleDistance)
            {
                smallestSampleDistance = distance;
                bestSample = nextSample;
            }
        }
        return GetSplineDistanceToNearestPoint(point,
                                               bestSample,
                                               numberOfSamples,
                                               sampleSpacing / numberOfSamples,
                                               recursionDepth - 1);
    }

    private IEnumerator FadeInMusic(AudioSource source)
    {
        float timer = 0;
        while (timer <= musicFadeInTime)
        {
            yield return null;
            source.volume = musicFadeInCurve.Evaluate(timer / musicFadeInTime) * musicVolume;
            timer += Time.deltaTime;
        }
        source.volume = musicVolume;
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(preGameTime);
        foreach (VehicleProgressionInfo vpi in progressInfo)
        {
            vpi.vehicleController.StartCar();
        }
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(postGameTime);
        SceneManager.LoadScene("Title");
    }
}
