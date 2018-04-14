using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace AwesomeProject
{
	public class CollisionSensor : MonoBehaviour
	{
		[SerializeField] private UnityEvent eventTriggerEntered = new UnityEvent();
		[SerializeField] private UnityEvent eventTriggerLeft = new UnityEvent();

		private int overlapCounter = 0;


		public bool IsTriggered
		{
			get { return this.overlapCounter > 0; }
		}


		private void UpdateDebugDisplay()
		{
			MeshRenderer renderer = this.GetComponent<MeshRenderer>();
			if (this.IsTriggered)
				renderer.material.color = Color.red;
			else
				renderer.material.color = Color.white;
		}

		private void OnTriggerEnter(Collider other)
		{
			bool wasTriggered = this.IsTriggered;
			this.overlapCounter++;

			if (!wasTriggered && this.IsTriggered)
				this.eventTriggerEntered.Invoke();

			this.UpdateDebugDisplay();
		}
		private void OnTriggerExit(Collider other)
		{
			bool wasTriggered = this.IsTriggered;
			this.overlapCounter--;

			if (wasTriggered && !this.IsTriggered)
				this.eventTriggerLeft.Invoke();

			this.UpdateDebugDisplay();
		}
	}
}