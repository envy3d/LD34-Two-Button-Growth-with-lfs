using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ForwardVelocity : MonoBehaviour
{
    public float speed = 1;

    private Rigidbody2D rb;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update()
    {
		if (Input.GetKey("up"))
		{
        	rb.MovePosition(transform.position + (Vector3.up * speed * Time.deltaTime));
		}
        
	}
}
