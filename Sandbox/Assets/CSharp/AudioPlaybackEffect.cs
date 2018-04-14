using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class AudioPlaybackEffect : MonoBehaviour
	{
		[SerializeField] private AudioSource source = null;
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

		private void Update()
		{
			// Adjust fade value
			//if (this.targetFade > this.currentFade)
			//{
			//	float diffToTarget = this.targetFade - this.currentFade;
			//	this.currentFade += Mathf.Min(Time.deltaTime / this.fadeDuration, diffToTarget);
			//}
			//if (this.targetFade < this.currentFade)
			//{
			//	float diffToTarget = this.targetFade - this.currentFade;
			//	this.currentFade -= Mathf.Min(Time.deltaTime / this.fadeDuration, -diffToTarget);
			//}
			this.currentFade = Mathf.MoveTowards(
				this.currentFade, 
				this.targetFade, 
				Time.deltaTime / this.fadeDuration);

			// Apply fade value to volume
			this.source.volume = this.currentFade;

			// Start and stop audio depending on volume
			bool shouldBePlaying = this.currentFade > 0.0f;
			if (shouldBePlaying && !this.source.isPlaying)
				this.source.Play();
			else if (!shouldBePlaying && this.source.isPlaying)
				this.source.Stop();
		}
	}
}