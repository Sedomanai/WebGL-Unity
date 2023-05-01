using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class SunshineSpawn : RandomSpawn
	{
		public override void FindSeed() {
			base.FindSeed();
			_randomizeAngle = true;
			_spawnRate = GameData.Instance.SunshineSpawn.Current;
			_spawnCount = GameData.Instance.SunshineCount.Current;
		}

#if UNITY_EDITOR
		new void FixedUpdate() {
			_spawnRate = GameData.Instance.SunshineSpawn.Current;
			_spawnCount = GameData.Instance.SunshineCount.Current;
			base.FixedUpdate();
		}
#endif
	}
}

