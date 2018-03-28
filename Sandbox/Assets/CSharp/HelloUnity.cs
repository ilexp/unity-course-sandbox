using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class HelloUnity : MonoBehaviour
	{
		private GameObject spawnedObj = null;

		private void Start()
		{
			this.spawnedObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			this.spawnedObj.transform.SetParent(this.transform);

			MeshRenderer renderer = this.spawnedObj.GetComponent<MeshRenderer>();
			renderer.material.color = Color.red;
		}
		private void Update()
		{
			MeshRenderer renderer = this.spawnedObj.GetComponent<MeshRenderer>();
			Vector3 pos = this.spawnedObj.transform.position;

			Debug.LogFormat("Position: {0}", pos);

			if (pos.x < 0.0f)
			{
				renderer.material.color = Color.green;
			}
			else
			{
				renderer.material.color = Color.red;
			}

			if (pos.y < 0.0f)
			{
				pos.y = 0.0f;
				this.spawnedObj.transform.position = pos;
			}
		}
	}
}