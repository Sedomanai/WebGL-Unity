using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class GenericSpawn : RandomSpawn
	{
		[SerializeField]
		float _maxAltitude = 10000.0f;
		[SerializeField]
		float _minAltitude = 8000.0f;

		[SerializeField]
		float spawnRate = 2.0f;
		[SerializeField]
		Vector2Int spawnCount = new Vector2Int(6, 10);
		[SerializeField]
		float spawnPrevent = 1.0f;
		[SerializeField]
		float spawnSheate = 1.0f;
		[SerializeField]
		bool randomizeAngle = false;

		void Start() {
			_playAnytime = true;
			_randomizeAngle = randomizeAngle;
			_spawnRate = spawnRate;
			_spawnCount = spawnCount;
			_spawnPrevent = spawnPrevent;
			_spawnSheathe = spawnSheate;
		}

		public override void FindSeed() {
			base.FindSeed();
		}

		new void FixedUpdate() {
			if (_seed) {
				float delt = _maxAltitude - _minAltitude;
				float mid = (_maxAltitude + _minAltitude) * 0.5f;
				var dist = Mathf.Abs(_seed.Altitude - mid) * 2.0f / delt;
				_spawnAltMultiplier = 1.0f - Mathf.Clamp(dist, 0.0f, 1.0f);

			} base.FixedUpdate();
		}
	}
}

