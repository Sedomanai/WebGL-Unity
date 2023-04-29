using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Elang
{
    namespace LD53 {
		public class CamLowest : MonoBehaviour
		{
			[SerializeField]
			float yOffset;

			Camera _cam;
			PixelPerfectCamera _pcam;

			void Awake() {
				_cam = GetComponent<Camera>();
			}
			void LateUpdate() {
				float dest = _cam.orthographicSize - yOffset;
				if (transform.position.y < dest)
					transform.position = new Vector3(transform.position.x, dest, transform.position.z);
			}
		}
	}
}
