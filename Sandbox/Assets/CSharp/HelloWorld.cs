using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class HelloWorld : MonoBehaviour
	{
		private GameObject spawnedObj = null;

		private void Start()
		{
			Debug.LogFormat("Hello World");
			Debug.LogFormat("Number: {0}", 123);
			Debug.LogFormat("String: {0}", "Test");

			int numberVariable = 234;
			string stringVariable = "Something";
			Debug.LogFormat("Number variable: {0}", numberVariable);
			Debug.LogFormat("String variable: {0}", stringVariable);

			for (int i = 0; i < 10; i++)
			{
				Debug.LogFormat("Counter: {0}", i);
				if (i > 5)
				{
					Debug.LogFormat("{0} is greater than 5", i);
				}
			}

			this.spawnedObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			this.spawnedObj.transform.SetParent(this.transform);

			MeshRenderer renderer = this.spawnedObj.GetComponent<MeshRenderer>();
			renderer.material.color = Color.red;
		}
		private void Update()
		{
			MeshRenderer renderer = this.spawnedObj.GetComponent<MeshRenderer>();
			if (this.spawnedObj.transform.position.x < 0.0f)
			{
				renderer.material.color = Color.green;
			}
			else
			{
				renderer.material.color = Color.red;
			}
		}
	}
}