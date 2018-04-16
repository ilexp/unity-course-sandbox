using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class LightSwitchEffect : MonoBehaviour
	{
		[SerializeField] private Light lightObj = null;
		[SerializeField] private float fadeDuration = 1.0f;

		private float currentFade = 0.0f;
		private float targetFade = 0.0f;

		public void StartEffect()
		{
			this.targetFade = 1.0f;
		}
		public void StopEffect()
		{
			this.targetFade = 0.0f;
		}
		public void ToggleEffect()
		{
			this.targetFade = 1.0f - this.targetFade;
		}

		private void Awake()
		{
			this.currentFade = this.lightObj.intensity;
			this.targetFade = this.currentFade;
		}
		private void Update()
		{
			this.currentFade = Mathf.MoveTowards(
				this.currentFade, 
				this.targetFade, 
				Time.deltaTime / this.fadeDuration);
			this.lightObj.intensity = this.currentFade;
			this.lightObj.enabled = this.currentFade > 0.0f;
		}
	}
}