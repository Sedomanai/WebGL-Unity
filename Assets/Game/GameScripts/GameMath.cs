using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

namespace Elang.LD53
{
	public class GameMath
	{
		public static Vector2 AngularPositionFromOrigin(Vector2 pos, float axis) {
			var mag = pos.magnitude;
			return AngleToVector2(ShiftedAngle(AngleFromOrigin(pos), axis / mag), mag);
		}
		public static float Vector2ToAngle(Vector2 vec) {
			return Mathf.Atan2(-vec.y, vec.x);
		}

		public static Vector2 AngleToVector2(float angle, float length) {
			return new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle)) * length;
		}
		public static Vector2 DirectionFromOrigin(Vector2 origin) {
			return origin.normalized;
		}
		public static float AngleFromOrigin(Vector2 vec) {
			return Vector2ToAngle(vec.normalized);
		}
		public static float ShiftedAngle(float angle, float shift) {
			return angle + Mathf.PI * shift;
		}

		public static float RandomizeAngle(float angle, float window) {
			float hw = Mathf.PI * window;
			return Random.Range(-hw, hw) + angle;
		}
	}
}
