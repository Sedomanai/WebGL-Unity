using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class GarbageCollect : MonoBehaviour
	{
		[SerializeField]
		float _collectDistance;

		void LateUpdate() {
			var distance = Vector2.Distance(Camera.main.transform.position, transform.position);
			if (distance > _collectDistance) {
				gameObject.SetActive(false);
			}
		}
	}

}
