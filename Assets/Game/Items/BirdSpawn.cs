using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class BirdSpawn : RandomSpawn
	{
		[SerializeField]
		float _maxAltitude = 30000.0f;
		[SerializeField]
		float _minAltitude = 16000.0f;

		public override void FindSeed() {
			base.FindSeed();
			_spawnRate = GameData.Instance.BirdSpawn.Current;
			_spawnCount = GameData.Instance.BirdCount.Current;
		}

		new void FixedUpdate() {
			if (_seed) {
				float delt = _maxAltitude - _minAltitude;
				float mid = (_maxAltitude + _minAltitude) * 0.5f;
				var dist = Mathf.Abs(_seed.Altitude - mid) * 2.0f / delt;
				_spawnAltMultiplier = 1.0f - Mathf.Clamp(dist, 0.0f, 1.0f);
			}
#if UNITY_EDITOR
			_spawnRate = GameData.Instance.BirdSpawn.Current;
			_spawnCount = GameData.Instance.BirdCount.Current;
#endif
			base.FixedUpdate();
		}
	}
}

