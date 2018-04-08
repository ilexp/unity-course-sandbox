using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class HelloInput : MonoBehaviour
	{
		[SerializeField] private float moveSpeed = 3.0f;
		[SerializeField] private float rotateSpeed = 90.0f;

		private void Update()
		{
			Vector2 moveInput = new Vector2(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"));

			Vector3 pos = this.transform.position;
			pos += this.transform.forward * moveInput.y * this.moveSpeed * Time.deltaTime;
			this.transform.position = pos;

			Quaternion rotation = this.transform.rotation;
			rotation *= Quaternion.AngleAxis(moveInput.x * this.rotateSpeed * Time.deltaTime, Vector3.up);
			this.transform.rotation = rotation;
		}
	}
}