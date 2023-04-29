using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class GarbageCollect : MonoBehaviour
	{
		[SerializeField]
		float _collectDistance;

		GameObject _seed;

		public void SetSeed(Seed seed) {
			_seed = seed.gameObject;
		}
		void LateUpdate() {
			//if (!_seed) {
			//	_seed = GameObject.FindGameObjectWithTag("Player");
			//} 
			if (_seed) { 
				var distance = Vector2.Distance(_seed.transform.position, transform.position);
				if (distance > _collectDistance) {
					gameObject.SetActive(false);
				}
			}
		}
	}

}
