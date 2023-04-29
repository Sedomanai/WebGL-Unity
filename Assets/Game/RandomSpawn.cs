using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elang.LD53
{
	public class RandomSpawn : MonoBehaviour
	{
		[SerializeField]
		InputActionAsset _controls;

		float _spawnInterval = 0;
		ObjectPool _pool;
		Seed _seed;

		void Awake() {
			_pool = GetComponent<ObjectPool>();
		}

		public void FindSeed() {
			if (!_seed)
				_seed = GameObject.FindGameObjectWithTag("Player").GetComponent<Seed>();
		}

		void FixedUpdate() {
			if (!GameInput.Instance.Playing.enabled || !_seed)
				return;

			Vector2 force = _seed.Force;

			if (_spawnInterval >= GameData.Instance.SunshineSpawnRate) {
				var magnitude = force.magnitude;
				var angle = Mathf.Atan2(-force.y, force.x);

				var count = Random.Range(GameData.Instance.SunshineCount.x, GameData.Instance.SunshineCount.y + 1);
				Random.Range(0, count);

				while (count-- > 0) {
					var obj = _pool.Get();
					var wide = Mathf.PI * 1/4;
					var currangle = Random.Range(-wide, wide) + angle;
					float size = 2.0f * (Camera.main.orthographicSize + 1);
					Vector3 spawnPos = new Vector3(Mathf.Cos(currangle), -Mathf.Sin(currangle), 0) * size  + Camera.main.transform.position;
					if (spawnPos.y > 3.0f) {
						spawnPos.z = 0.0f;
						obj.GetComponent<GarbageCollect>().SetSeed(_seed);
						obj.transform.position = spawnPos;
						obj.SetActive(true); //debug
					}
						
				}
				_spawnInterval -= GameData.Instance.SunshineSpawnRate;
			}
			_spawnInterval += Mathf.Abs(force.magnitude);
		}
	}
}

