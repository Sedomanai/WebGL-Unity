using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class Boosts : MonoBehaviour
	{
		[SerializeField]
		GameObject[] boosts;
		int c;

		void OnEnable() {
			foreach (GameObject boost in boosts) {
				boost.SetActive(true);
			} c = boosts.Length;
		}

		public bool Use() {
			if (c > 0) {
				c--;
				boosts[c].SetActive(false);
				return true;
			} else return false;
		}
	}
}
