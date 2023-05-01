using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class Bird : MonoBehaviour
	{
		[SerializeField]
		GameEvent _gameEvent;

		public void OnHit() {
			if (GameInput.Instance.Playing.enabled)
				_gameEvent.Fly.Invoke();
		}

		void FixedUpdate() {
			transform.position = GameMath.AngularPositionFromOrigin(transform.position, -0.01f);
			transform.eulerAngles = new Vector3(0, 0, -GameMath.Vector2ToAngle(transform.position) * Mathf.Rad2Deg);
		}
	}
}
