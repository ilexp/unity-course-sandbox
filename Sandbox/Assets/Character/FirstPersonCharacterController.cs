using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class FirstPersonCharacterController : MonoBehaviour
	{
		[SerializeField] private float jumpForce = 4.0f;
		[SerializeField] private float moveSpeed = 4.0f;
		[SerializeField] private float moveAcceleration = 10.0f;
		[SerializeField] private float lookSensitivity = 4.0f;
		[SerializeField] private float lookAcceleration = 25.0f;
		[SerializeField] private Transform head = null;
		[SerializeField] private CollisionSensor floorSensor = null;
		[SerializeField] private Collider bodyCollider = null;
		[SerializeField] private GameObject createPrefab = null;

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
			bool useObject = Input.GetButtonDown("Fire1");
			bool createObject = Input.GetButtonDown("Fire2");
			if (moveInput.magnitude > 1.0f)
				moveInput /= moveInput.magnitude;

			this.targetMove =
				this.transform.forward * moveInput.y +
				this.transform.right * moveInput.x;
			this.targetLookAngle.x += lookInput.x * this.lookSensitivity;
			this.targetLookAngle.y += lookInput.y * -this.lookSensitivity;
			this.targetLookAngle.y = Mathf.Clamp(this.targetLookAngle.y, -90.0f, 90.0f);
			if (jumpInput) this.triggerJump = true;

			if (useObject)
			{
				Ray viewRay = new Ray(this.head.position, this.head.forward);
				RaycastHit hitInfo;
				bool hitAnything = Physics.Raycast(viewRay, out hitInfo, 1.5f);
				if (hitAnything)
				{
					UsableObject usableObject = hitInfo.transform.GetComponent<UsableObject>();
					if (usableObject != null)
						usableObject.Use();
				}
			}
			if (createObject)
			{
				GameObject obj = GameObject.Instantiate(this.createPrefab);
				obj.transform.position = this.head.position + this.head.forward * 1.5f;
			}
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
			float moveAccModifier = 1.0f;
			if (!isOnGround)
			{
				moveAccModifier *= 0.1f;
			}
			else
			{
				if (this.targetMove.magnitude < 0.1f)
				{
					targetBodyVelocity.y = 0.0f;
					this.bodyCollider.material.frictionCombine = PhysicMaterialCombine.Average;
					this.bodyCollider.material.staticFriction = 1.0f;
					this.bodyCollider.material.dynamicFriction = 1.0f;
				}
				else
				{
					this.bodyCollider.material.frictionCombine = PhysicMaterialCombine.Minimum;
					this.bodyCollider.material.staticFriction = 0.0f;
					this.bodyCollider.material.dynamicFriction = 0.0f;
				}
			}
			body.velocity = Vector3.Lerp(bodyVelocity, targetBodyVelocity, this.moveAcceleration * Time.fixedDeltaTime * moveAccModifier);

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