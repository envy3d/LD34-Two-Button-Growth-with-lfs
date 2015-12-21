using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour
{
    public float degreesPerSecond = 180;

	void Update()
    {
        transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime);
	}
}
