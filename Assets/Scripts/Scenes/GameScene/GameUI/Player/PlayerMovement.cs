using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float f_walkspeed = 4f;
	public float f_maxVelocityChange = 10f;

	private Vector2 input;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		input.Normalize();
	}

	private void FixedUpdate()
	{
		rb.AddForce(CalculateMovement(f_walkspeed), ForceMode.VelocityChange);
	}

	Vector3 CalculateMovement(float speed)
	{
		Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
		targetVelocity = transform.TransformDirection(targetVelocity);

		targetVelocity *= speed;

		Vector3 velocity = rb.velocity;

		if (input.magnitude > 0.5f)
		{
			Vector3 velocityChange = targetVelocity - velocity;

			velocityChange.x = Mathf.Clamp(velocityChange.x, -f_maxVelocityChange, f_maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -f_maxVelocityChange, f_maxVelocityChange);

			velocityChange.y = 0;

			return (velocityChange);
		}
		else
		{
			return new Vector3();
		}

	}
}

