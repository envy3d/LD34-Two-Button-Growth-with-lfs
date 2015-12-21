using UnityEngine;
using System.Collections;

public class FollowPlayerVehicle : MonoBehaviour {

	public Transform objectToFollow;
	public float yMargin = 0;
	public float xMargin = 0;
	public float ySmooth = .5f;
	public float xSmooth = .5f;

	private Transform _transform;

	void Awake() {
		
		_transform = transform;
	}

	// Update is called once per frame
	void LateUpdate() {
	
		if (!objectToFollow) return;

		float xTarget = _transform.position.x;
		float yTarget = _transform.position.y;

		if (AtXMargin ()){

			xTarget = Mathf.Lerp (xTarget, objectToFollow.position.x, xSmooth * Time.deltaTime);
		}

		if (AtYMargin ()){

			yTarget = Mathf.Lerp (yTarget, objectToFollow.position.y, ySmooth * Time.deltaTime);
		}

		_transform.position = new Vector3 (xTarget, yTarget, transform.position.z);
	}

	private bool AtXMargin(){
		return (Mathf.Abs (objectToFollow.position.x) - _transform.position.x) >= xMargin;
	}

	private bool AtYMargin(){
		return (Mathf.Abs (objectToFollow.position.y) - _transform.position.y) >= yMargin;
	}

	public void SetObjectToFollow (Transform objectToFollow){

		this.objectToFollow = objectToFollow;
	}
}
