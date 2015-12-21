using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float leadDistance = 0;
    public float stickiness = 0;

    private Transform _transform;
    private Vector3 localDisplacementFromTarget;

    void Awake()
    {
        _transform = transform;
        localDisplacementFromTarget = target.localToWorldMatrix * Vector3.up * leadDistance;
        _transform.position = new Vector3(target.position.x + localDisplacementFromTarget.x,
                                          target.position.y + localDisplacementFromTarget.y,
                                          _transform.position.z);
    }
	
	void Update()
    {
        localDisplacementFromTarget = Vector3.Lerp(localDisplacementFromTarget,
                                                   target.localToWorldMatrix * Vector3.up * leadDistance,
                                                   stickiness * Time.deltaTime);
        _transform.position = new Vector3(target.position.x + localDisplacementFromTarget.x,
                                          target.position.y + localDisplacementFromTarget.y,
                                          _transform.position.z);
    }
}
