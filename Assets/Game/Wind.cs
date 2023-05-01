using Elang.LD53;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wind : MonoBehaviour
{
	[SerializeField]
	InputActionAsset _controls;

    const int intervalLength = 30;
    [SerializeField]
    int _maxHorizontalInterval = 5;
	[SerializeField]
	int _maxVerticalInterval = 12;
	[SerializeField]
	int _maxFloatDuration = 10;
	[SerializeField]
	int _floatVelocity = 10;

	int _horizontalWindChangeInterval, _verticalWindBoostInterval;
	int _floatDuration = 0;

	int _windLevel;
    public int WindLevel { get { return _windLevel; } }


	float _floatLevel;
	public float FloatLevel { get { return _floatLevel; } }

	void FixedUpdate() {
		if (GameInput.Instance.Playing.enabled) {
			CalculateHorizontalInterval();
			CalculateVerticalInterval();
		}
    }

	public void ResetWind() {
		_windLevel = _horizontalWindChangeInterval = _verticalWindBoostInterval = _floatDuration = 0;
		_floatLevel = 0.0f;
		GameUI.Instance.SetWindLevel(_windLevel);
		GameUI.Instance.TurnFloatingTextOff();
	}

	void CalculateHorizontalInterval() {
		if (_horizontalWindChangeInterval >= _maxHorizontalInterval * intervalLength) {
			_horizontalWindChangeInterval = 0;
			int dir = Random.Range(0, 3) - 1;
			_windLevel += dir;
			_windLevel = Mathf.Clamp(-_windLevel, -3, 3);
			GameUI.Instance.SetWindLevel(_windLevel);
		}
		_horizontalWindChangeInterval++;
	}

	void CalculateVerticalInterval() {
		if (_verticalWindBoostInterval >= _maxVerticalInterval * intervalLength) {
			_verticalWindBoostInterval = 0;
			_floatDuration = _maxFloatDuration * intervalLength * Random.Range(0, 2);
		} _verticalWindBoostInterval++;

		if (_floatDuration > 0) {
			GameUI.Instance.TurnFloatingTextOn();
			_floatLevel = _floatVelocity;
			_floatDuration--;
		} else {
			_floatLevel = 0.0f;
			GameUI.Instance.TurnFloatingTextOff();
		}
	}
}
