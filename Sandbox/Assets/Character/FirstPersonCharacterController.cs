using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class FirstPersonCharacterController : MonoBehaviour
	{
		[SerializeField] private float jumpForce = 4.0f;
		[SerializeField] private float moveSpeed = 10.0f;
		[SerializeField] private float lookSpeed = 6.0f;
		[SerializeField] private Transform head = null;

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		private void Update()
		{
			Rigidbody body = this.GetComponent<Rigidbody>();
			//body.velocity = this.transform.forward;

			Vector2 moveInput = new Vector2(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"));
			Vector2 lookInput = new Vector2(
				Input.GetAxis("Mouse X"),
				Input.GetAxis("Mouse Y"));
			bool jumpInput = Input.GetButtonDown("Jump");
			if (moveInput.magnitude > 1.0f)
				moveInput /= moveInput.magnitude;

			Vector3 targetVelocity = Vector3.zero;
			targetVelocity += this.transform.forward * moveInput.y * this.moveSpeed;
			targetVelocity += this.transform.right * moveInput.x * this.moveSpeed;
			Vector3 velocity = body.velocity;
			velocity.x = targetVelocity.x;
			velocity.z = targetVelocity.z;
			body.velocity = velocity;

			Quaternion rotation = body.rotation;
			rotation *= Quaternion.AngleAxis(lookInput.x * this.lookSpeed, Vector3.up);
			body.MoveRotation(rotation);

			this.head.rotation *= Quaternion.AngleAxis(-lookInput.y * this.lookSpeed, Vector3.right);

			if (jumpInput)
				body.AddRelativeForce(Vector3.up * this.jumpForce, ForceMode.VelocityChange);
		}
	}
}