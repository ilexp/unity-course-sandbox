using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class FirstPersonCharacterController : MonoBehaviour
	{
		[SerializeField] private float jumpForce = 4.0f;
		[SerializeField] private float moveSpeed = 6.0f;
		[SerializeField] private float moveAcceleration = 10.0f;
		[SerializeField] private float lookSensitivity = 5.0f;
		[SerializeField] private float lookAcceleration = 25.0f;
		[SerializeField] private Transform head = null;
		[SerializeField] private CollisionSensor floorSensor = null;

		private Vector2 targetLookAngle = Vector2.zero;
		private Vector3 targetMove = Vector3.zero;
		private bool triggerJump = false;

		private void Start()
		{
			this.targetLookAngle.x = this.transform.rotation.eulerAngles.y;
			Cursor.lockState = CursorLockMode.Locked;
		}
		private void Update()
		{
			Vector2 moveInput = new Vector2(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"));
			Vector2 lookInput = new Vector2(
				Input.GetAxis("Mouse X"),
				Input.GetAxis("Mouse Y"));
			bool jumpInput = Input.GetButtonDown("Jump");
			if (moveInput.magnitude > 1.0f)
				moveInput /= moveInput.magnitude;

			this.targetMove =
				this.transform.forward * moveInput.y +
				this.transform.right * moveInput.x;
			this.targetLookAngle.x += lookInput.x * this.lookSensitivity;
			this.targetLookAngle.y += lookInput.y * -this.lookSensitivity;
			this.targetLookAngle.y = Mathf.Clamp(this.targetLookAngle.y, -90.0f, 90.0f);
			if (jumpInput) this.triggerJump = true;
		}
		private void FixedUpdate()
		{
			Rigidbody body = this.GetComponent<Rigidbody>();
			bool isOnGround = this.floorSensor.IsTriggered;

			//Vector3 targetBodyVelocity = this.targetMove * this.moveSpeed;
			//targetBodyVelocity.y = bodyVelocity.y;
			//body.velocity = targetBodyVelocity;
			//body.MoveRotation(Quaternion.Euler(0.0f, this.targetLookAngle.x, 0.0f));
			//this.head.localRotation = Quaternion.Euler(this.targetLookAngle.y, 0.0f, 0.0f);

			Vector3 bodyVelocity = body.velocity;
			Vector3 targetBodyVelocity = this.targetMove * this.moveSpeed;
			targetBodyVelocity.y = bodyVelocity.y;
			if (!isOnGround)
				body.velocity = Vector3.Lerp(bodyVelocity, targetBodyVelocity, this.moveAcceleration * Time.fixedDeltaTime * 0.1f);
			else
				body.velocity = Vector3.Lerp(bodyVelocity, targetBodyVelocity, this.moveAcceleration * Time.fixedDeltaTime);
			
			Quaternion bodyRotation = body.rotation;
			Quaternion targetBodyRotation = Quaternion.Euler(0.0f, this.targetLookAngle.x, 0.0f);
			body.MoveRotation(Quaternion.Lerp(bodyRotation, targetBodyRotation, this.lookAcceleration * Time.fixedDeltaTime));
			
			Quaternion headRotation = this.head.localRotation;
			Quaternion targetHeadRotation = Quaternion.Euler(this.targetLookAngle.y, 0.0f, 0.0f);
			this.head.localRotation = Quaternion.Lerp(headRotation, targetHeadRotation, this.lookAcceleration * Time.fixedDeltaTime);

			if (this.triggerJump)
			{
				this.triggerJump = false;
				if (isOnGround)
				{
					body.AddForce(Vector3.up * this.jumpForce, ForceMode.VelocityChange);
				}
			}
		}
	}
}