using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Elang.LD53
{
	public class GameUI : Singleton<GameUI> {
		[SerializeField]
		TextMeshProUGUI _distance;
		[SerializeField]
		TextMeshProUGUI _velocity;
		[SerializeField]
		TextMeshProUGUI _altitude;
		[SerializeField]
		TextMeshProUGUI _angle;

		[SerializeField]
		TextMeshProUGUI _sunshine;
		[SerializeField]
		TextMeshProUGUI _floating;
		[SerializeField]
		TextMeshProUGUI _windlevel;
		[SerializeField]
		GameObject _controlAxisLeft, _controlAxisRight;
		[SerializeField]
		WindLevelInterface _windInterface;

		[SerializeField]
		TextMeshProUGUI _sunshineMenu;

		public void SetDistance(float value) {
			_distance.text = $"{Math.Round(value, 1)}";
		}
		public void SetVelocity(float value) {
			_velocity.text = $"{Math.Round(value, 2)}";
		}
		public void SetAltitude(float value) {
			_altitude.text = $"{Math.Round(value, 2)}";
		}
		public void SetAngle(float value) {
			if (value < 0)
				value += 360;
			_angle.text = $"{Math.Round(value)}";
		}
		public void SetControlAxis(float value) {
			_controlAxisRight.SetActive(false);
			_controlAxisLeft.SetActive(false);
			if (value < 0) {
				_controlAxisLeft.SetActive(true);
				_controlAxisLeft.transform.localScale = new Vector3(Mathf.Abs(value), 1, 1);
			}
			if (value > 0) {
				_controlAxisRight.SetActive(true);
				_controlAxisRight.transform.localScale = new Vector3(Mathf.Abs(value), 1, 1);
			}
		}
		public void SetWindLevel(int value) {
			_windInterface.SetLevel(value);
		}

		public void SetSun(int value) {
			_sunshineMenu.text = _sunshine.text = $"{value}";
		}
		public void TurnFloatingTextOn() {
			_floating.text = "Floating";
		}
		public void TurnFloatingTextOff() {
			_floating.text = "Not Floating";
		}
	}

}
