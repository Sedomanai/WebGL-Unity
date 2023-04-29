using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class Sunshine : MonoBehaviour
	{
		public void OnSpawn() {
			//Debug.Log("Spawn Sunshine");
		}


		public void OnHit() {
			GameData.Instance.AddSunshine(10);
		}
	}
}
