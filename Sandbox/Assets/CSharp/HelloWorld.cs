using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeProject
{
	public class HelloWorld : MonoBehaviour
	{
		private void Start()
		{
			Debug.LogFormat("Hello World");
			Debug.LogFormat("Number: {0}", 123);
			Debug.LogFormat("String: {0}", "Test");

			int numberVariable = 234;
			float numberVariable2 = 1.234f;
			string stringVariable = "Something";
			Debug.LogFormat("Number variable: {0}", numberVariable);
			Debug.LogFormat("Number variable 2: {0}", numberVariable2);
			Debug.LogFormat("String variable: {0}", stringVariable);

			this.CountToTen();
		}

		private void CountToTen()
		{
			for (int i = 0; i < 10; i++)
			{
				Debug.LogFormat("Counter: {0}", i);
				if (this.IsBiggerThanFive(i))
				{
					Debug.LogFormat("{0} is greater than 5", i);
				}
			}
		}
		private bool IsBiggerThanFive(int i)
		{
			//if (i > 5)
			//	return true;
			//else
			//	return false;
			return i > 5;
		}
	}
}