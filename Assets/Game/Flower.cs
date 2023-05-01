using System.Collections;
using UnityEngine;

namespace Elang
{
	namespace LD53
	{
		public class Flower : MonoBehaviour
        {
			static bool firstTime = true;

            [SerializeField]
            ObjectPool _seedpool;
			[SerializeField]
			GameEvent _gameEvent;

			//Seed _seed;
			Transform _spawntr;
			Animator _anim;

            void Awake() {
                _spawntr = GetComponentsInChildren<Transform>()[1];
				gameObject.SetActive(false);
				_anim = GetComponent<Animator>();
				_anim.StopPlayback();
            }

			void OnEnable() {
				StartCoroutine(GrowFlower());
			}

			Vector3 _deltaCam;
			IEnumerator GrowFlower() {
				_deltaCam = _spawntr.transform.position - Camera.main.transform.position;
				_deltaCam.z = 0.0f;
				yield return StartCoroutine(Lerp());
				SoundMgr.Instance.PlaySFX("grow");
				_anim.Play("flower");
			}

			IEnumerator Lerp() {
				int times = 300;
				float rate = 1.0f / times;
				while (times > 0) {
					Camera.main.transform.position += (_deltaCam * rate);
					yield return null;
					times--;
				}
			}

			public void Spawn() {
				var seed = _seedpool.Get();
				seed.transform.position = _spawntr.transform.position;
				seed.tag = "Player";
				seed.SetActive(true);
				if (!firstTime) {
					GameInput.Instance.Menu.Enable();
					_gameEvent.OnMenu.Invoke();
				} firstTime = false;
			}
        }
    }

}