using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

namespace Elang
{
	namespace LD53
	{
		public class Flower : MonoBehaviour
        {
            [SerializeField]
            ObjectPool _seedpool;
			[SerializeField]
			Camera _camera;

			//Seed _seed;
			Transform _spawntr;
            void Awake() {
                _spawntr = GetComponentsInChildren<Transform>()[1];
				gameObject.SetActive(false);
            }

			void OnEnable() {
				StartCoroutine(GrowFlower());
			}

			Vector3 _deltaCam;
			IEnumerator GrowFlower() {
				_deltaCam = _spawntr.transform.position - _camera.transform.position;
				_deltaCam.z = 0.0f;
				// animate or something here.
				yield return StartCoroutine(Lerp());
				//GameInput.Instance.Menu.Enable();
				Spawn();
			}

			IEnumerator Lerp() {
				int times = 300;
				float rate = 1.0f / times;
				while (times > 0) {
					_camera.transform.position += (_deltaCam * rate);
					yield return null;
					times--;
				}
			}

			public void Spawn() {
				var seed = _seedpool.Get();
				seed.transform.position = _spawntr.transform.position;
				seed.tag = "Player";
				seed.SetActive(true);
			}
        }
    }

}