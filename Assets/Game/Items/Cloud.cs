using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class Cloud : MonoBehaviour
	{
		float _speed;

		void OnEnable() {
			int sign = Random.Range(0, 2);
			if (sign == 0)
				sign--;
			_speed = Random.Range(0.005f, 0.01f) * sign;
		}
		void FixedUpdate() {
			transform.position = GameMath.AngularPositionFromOrigin(transform.position, _speed);
			transform.eulerAngles = new Vector3(0, 0, -GameMath.Vector2ToAngle(transform.position) * Mathf.Rad2Deg);
		}
	}
}
