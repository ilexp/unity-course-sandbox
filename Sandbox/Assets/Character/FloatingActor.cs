using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class FloatingActor : MonoBehaviour
	{
		[SerializeField] private Transform followTarget = null;
		[SerializeField] private float hoverHeight = 1.5f;
		[SerializeField] private float motorForce = 20.0f;
		//[SerializeField] private float targetDistance = 1.5f;
		//[SerializeField] private float stabilization = 6.0f;
		//[SerializeField] private float motorVelocity = 5.0f;

		private Vector3 targetPos;


		private void Update()
		{
			if (this.followTarget != null)
				this.targetPos = this.followTarget.position;

			RaycastHit hit;
			bool hitAnything = Physics.Raycast(new Ray(this.transform.position, Vector3.down), out hit, this.hoverHeight * 3.0f);
			if (hitAnything)
			{
				this.targetPos.y = hit.point.y + this.hoverHeight;
			}
			else
			{
				this.targetPos.y = this.transform.position.y - 1.0f;
			}
		}
		private void FixedUpdate()
		{
			Rigidbody body = this.GetComponent<Rigidbody>();

			Vector3 posDiff = this.targetPos - body.position;
			Vector3 targetVelocity = posDiff;
			body.velocity = Vector3.Lerp(body.velocity, targetVelocity, Time.fixedDeltaTime * this.motorForce);

			Vector3 lookDir = new Vector3(posDiff.x, 0.0f, posDiff.z).normalized;
			Quaternion targetRotation = Quaternion.LookRotation(lookDir, Vector3.up);
			body.MoveRotation(Quaternion.Lerp(body.rotation, targetRotation, this.motorForce * Time.fixedDeltaTime));

			//// Dampen velocity for stabilization
			//body.velocity -= body.velocity * this.stabilization * Time.fixedDeltaTime;
			//body.angularVelocity -= body.angularVelocity * this.stabilization * Time.fixedDeltaTime;
			//
			//// Keep the target height
			//float heightDiff = this.targetPos.y - body.position.y;
			//float upwardForce = Mathf.Clamp01(heightDiff / 0.15f) * this.motorForce;
			//body.AddForce(Vector3.up * upwardForce, ForceMode.Acceleration);
			//
			//// Move along the ground plane
			//Vector3 posDiff = this.targetPos - body.position;
			//Vector2 moveDirection = new Vector2(posDiff.x, posDiff.z).normalized;
			//float moveIntensity = Mathf.Clamp01((posDiff.magnitude - this.targetDistance) / 5.0f);
			//Vector2 targetVelocity = moveDirection * moveIntensity * this.motorVelocity;
			//Vector2 currentVelocity = new Vector2(body.velocity.x, body.velocity.z);
			//Vector2 moveForce = (targetVelocity - currentVelocity) * this.motorForce;
			//body.AddForce(new Vector3(moveForce.x, 0.0f, moveForce.y), ForceMode.Acceleration);
			//
			//// Rotate towards the target direction
			//Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.y), Vector3.up);
			//body.MoveRotation(Quaternion.Lerp(body.rotation, targetRotation, this.motorForce * Time.fixedDeltaTime));
		}
	}
}