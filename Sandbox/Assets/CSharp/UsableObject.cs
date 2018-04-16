using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace AwesomeProject
{
	public class UsableObject : MonoBehaviour
	{
		[SerializeField] private UnityEvent eventTriggerUsed = new UnityEvent();

		public void Use()
		{
			this.eventTriggerUsed.Invoke();
		}
	}
}