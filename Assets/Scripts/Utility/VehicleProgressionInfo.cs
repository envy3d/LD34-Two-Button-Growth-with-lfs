
using UnityEngine;
using System;

[Serializable]
public class VehicleProgressionInfo
{
    public VehicleController vehicleController;
    public RankText rankText;
    [NonSerialized]
    public Transform transform;
    public int currentLap;
    public int rank = 1;
    public float progress;
    public float totalProgress;

    public void UpdateProgress(float progress)
    {
        this.progress = progress;
        this.totalProgress = currentLap + progress;
    }
}
