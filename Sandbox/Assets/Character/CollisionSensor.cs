using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class CollisionSensor : MonoBehaviour
	{
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
			this.overlapCounter++;
			this.UpdateDebugDisplay();
		}
		private void OnTriggerExit(Collider other)
		{
			this.overlapCounter--;
			this.UpdateDebugDisplay();
		}
	}
}