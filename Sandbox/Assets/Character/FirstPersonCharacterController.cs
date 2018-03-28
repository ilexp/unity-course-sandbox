using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class FirstPersonCharacterController : MonoBehaviour
	{
		[SerializeField] private float jumpForce = 5.0f;
		[SerializeField] private float moveSpeed = 10.0f;
		[SerializeField] private float lookSpeed = 200.0f;
		[SerializeField] private Transform head = null;

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		private void Update()
		{
			Rigidbody body = this.GetComponent<Rigidbody>();
			//body.position += this.transform.forward * Time.deltaTime;

			Vector2 moveInput = new Vector2(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"));
			Vector2 lookInput = new Vector2(
				Input.GetAxis("Mouse X"),
				Input.GetAxis("Mouse Y"));
			bool jumpInput = Input.GetButtonDown("Jump");
			if (moveInput.magnitude > 1.0f)
				moveInput /= moveInput.magnitude;

			Vector3 pos = body.position;
			pos += this.transform.forward * moveInput.y * this.moveSpeed * Time.deltaTime;
			pos += this.transform.right * moveInput.x * this.moveSpeed * Time.deltaTime;
			body.MovePosition(pos);

			Quaternion rotation = body.rotation;
			rotation *= Quaternion.AngleAxis(lookInput.x * this.lookSpeed * Time.deltaTime, Vector3.up);
			body.MoveRotation(rotation);

			this.head.rotation *= Quaternion.AngleAxis(-lookInput.y * this.lookSpeed * Time.deltaTime, Vector3.right);

			if (jumpInput)
				body.AddRelativeForce(Vector3.up * this.jumpForce, ForceMode.VelocityChange);
		}
	}
}