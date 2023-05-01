using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elang.LD53
{
	public class RandomSpawn : MonoBehaviour
	{
		[SerializeField]
		GameEvent _gameEvent;

		float _spawnTick = 0;
		ObjectPool _pool;
		protected Seed _seed;
		float _groundAltitude;

		protected bool _randomizeAngle = false;
		protected bool _setAngle = false;
		protected bool _playAnytime = false;
		protected float _spawnRate;
		protected float _spawnPrevent = 4.0f;
		protected float _spawnAltMultiplier = 1.0f;
		protected Vector2Int _spawnCount;
		protected float _spawnSheathe = 2.0f;

		void Awake() {
			_pool = GetComponent<ObjectPool>();
			_gameEvent.OnStart.AddListener(FindSeed);
		}

		void OnDestroy() {
			_gameEvent.OnStart.RemoveListener(FindSeed);
		}

		public virtual void FindSeed() {
			if (!_seed) {
				_seed = GameObject.FindGameObjectWithTag("Player").GetComponent<Seed>();
				var circ = GameObject.FindGameObjectWithTag("Respawn").GetComponent<CircleCollider2D>();
				_groundAltitude = circ.radius* circ.transform.lossyScale.y;
			}
		}

		protected void FixedUpdate() {
			if (!((_playAnytime || GameInput.Instance.Playing.enabled)))
				return;

			Vector2 force = _seed ? _seed.Force : Vector2.one * 0.01f;

			if (_spawnTick >= _spawnRate) {
				var angle = GameMath.Vector2ToAngle(force);
				var count = Random.Range(_spawnCount.x, _spawnCount.y + 1);
				Random.Range(0, count);

				while (count-- > 0) {
					var currangle = GameMath.RandomizeAngle(angle, 0.25f);

					float length = 2.0f * (Camera.main.orthographicSize + _spawnSheathe);
					var campos = Camera.main.transform.position;
					Vector2 spawnPos = GameMath.AngleToVector2(currangle, length) + new Vector2(campos.x, campos.y);

					if (spawnPos.magnitude > _spawnPrevent + _groundAltitude) {
						var obj = _pool.Get();
						obj.transform.position = spawnPos;
						obj.SetActive(true);
						if (_randomizeAngle)
							obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(-180.0f, 180.0f));
						if (_setAngle)
							obj.transform.eulerAngles = new Vector3(0, 0, -GameMath.Vector2ToAngle(spawnPos) * Mathf.Rad2Deg);
					}	
				}
				_spawnTick -= _spawnRate;
			}

			_spawnTick += Mathf.Abs(force.magnitude) * _spawnAltMultiplier;
		}
	}
}

