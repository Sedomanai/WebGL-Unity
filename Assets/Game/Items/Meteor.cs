using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Elang.LD53
{
	public class Meteor : MonoBehaviour
	{
		[SerializeField]
		float _mass = 0.02f;
		[SerializeField]
		float _gravity = 2.0f;
		float _force = 0.0f;

		[SerializeField]
		GameEvent _gameEvent;

		void OnEnable() {
			_force = -5.0f * _mass;
		}

		public void OnHit() {
			if (GameInput.Instance.Playing.enabled)
				_gameEvent.Strike.Invoke();
		}
		void FixedUpdate() {
			_force -= _mass * _gravity;
			transform.position += _force * transform.position.normalized;
			transform.position = GameMath.AngularPositionFromOrigin(transform.position, Random.Range(-0.02f, 0.02f));
		}
	}
}
