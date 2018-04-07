using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class FirstPersonCharacterController : MonoBehaviour
	{
		[SerializeField] private float jumpForce = 4.0f;
		[SerializeField] private float moveAcceleration = 15.0f;
		[SerializeField] private float moveSpeed = 6.0f;
		[SerializeField] private float lookSpeed = 5.0f;
		[SerializeField] private Transform head = null;
		[SerializeField] private CollisionSensor floorSensor = null;

		private float lookAngle = 0.0f;

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		private void Update()
		{
			Rigidbody body = this.GetComponent<Rigidbody>();
			//body.velocity = this.transform.forward;

			bool isOnGround = this.floorSensor.IsTriggered;
			Vector2 moveInput = new Vector2(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"));
			Vector2 lookInput = new Vector2(
				Input.GetAxis("Mouse X"),
				Input.GetAxis("Mouse Y"));
			bool jumpInput = Input.GetButtonDown("Jump");
			if (moveInput.magnitude > 1.0f)
				moveInput /= moveInput.magnitude;

			Vector3 targetVelocity = 
				this.transform.forward * moveInput.y * this.moveSpeed +
				this.transform.right * moveInput.x * this.moveSpeed;
			//Vector3 velocity = body.velocity;
			//velocity.x = targetVelocity.x;
			//velocity.z = targetVelocity.z;
			//body.velocity = velocity;
			Vector3 moveForce = Vector3.zero;
			moveForce.x = (targetVelocity.x - body.velocity.x) * this.moveAcceleration;
			moveForce.z = (targetVelocity.z - body.velocity.z) * this.moveAcceleration;
			if (!isOnGround)
			{
				moveForce *= 0.1f;
			}
			body.AddForce(moveForce, ForceMode.Acceleration);

			Quaternion rotation = body.rotation;
			rotation *= Quaternion.AngleAxis(lookInput.x * this.lookSpeed, Vector3.up);
			body.MoveRotation(rotation);

			this.lookAngle = Mathf.Clamp(this.lookAngle - lookInput.y * this.lookSpeed, -90.0f, 90.0f);
			this.head.localRotation = Quaternion.Euler(this.lookAngle, 0.0f, 0.0f);

			if (isOnGround && jumpInput)
			{
				body.AddRelativeForce(Vector3.up * this.jumpForce, ForceMode.VelocityChange);
			}
		}
	}
}