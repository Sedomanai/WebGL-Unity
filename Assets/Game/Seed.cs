using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

namespace Elang
{
	namespace LD53 
	{
		public class Seed : MonoBehaviour
		{
			[SerializeField]
			InputActionReference _fire;
			[SerializeField]
			InputActionReference _horizontal;
			[SerializeField]
			InputActionReference _windward;
			[SerializeField]
			InputActionReference _boost;

			[SerializeField]
			float _mass = 0.0001f;
			[SerializeField]
			Vector2 _initialVelocity = new Vector2(2, 2);
			[SerializeField]
			float _boostPower = 15.0f;
			[SerializeField]
			float _controlPower = 2.0f;
			[SerializeField]
			float _controlCap = 0.2f;
			//[SerializeField]
			//float _windFriction = 0.1f;

			[SerializeField]
			Wind _wind;

			[SerializeField]
			ObjectPool _flowerPool;

			[SerializeField]
			float _gravity;
			float _windFriction = 15.0f;

			int _boostDuration = 0;
			float _boostVelocity = 0.0f;
			float _controlVelocity = 0.0f;

			Vector2 _traveledDistance, _initialPosition;
			Vector2 _force = new Vector2();
			public Vector2 Force { get { return _force; } }

			[SerializeField]
			GameEvent _gameEvent;

			void Awake() {
				gameObject.SetActive(false);
			}

			void OnEnable() {
				_force = new Vector2();
				_controlVelocity = _boostVelocity = 0.0f;
				_fire.action.Reset(); // prevent bug
				_initialPosition = transform.position;
				GameInput.Instance.Startup.Enable();
			}

			// Update is called once per frame
			void Update() {
				Startup();
				if (_boost.action.triggered)
					_boostDuration = 60;
				UpdateCam();
				GameUI.Instance.traveledDistanceText.text = _traveledDistance.ToString() + " " + _force.magnitude * 30;
			}

			void Startup() {
				if (GameInput.Instance.Startup.enabled && _fire.action.triggered) {
					_force = _initialVelocity;
					GameInput.Instance.Startup.Disable();
					GameInput.Instance.Playing.Enable();
					_gameEvent.OnStart.Invoke();
				}
			}
			void Playing() {
				if (GameInput.Instance.Playing.enabled) {
					var hori = _horizontal.action.ReadValue<float>();
					_controlVelocity += _controlPower * hori;
					_controlVelocity = Mathf.Clamp(_controlVelocity, -_controlCap * _mass, _controlCap * _mass);

					_boostVelocity = 0;
					if (_boostDuration > 0) {
						_boostVelocity = _boostPower;
						_boostDuration--;
					}

					var boost = _force.normalized * _boostVelocity;

					//var norm = (transform.position - Vector3.zero).normalized;
					//Vector3.Distance(Vector3.zero, transform.position);

					_force.x += _mass * (_wind.WindLevel + boost.x) + _controlVelocity;
					_force.y += _mass * (_wind.FloatLevel - _gravity + boost.y);
					_force.x *= (1.0f - _windFriction * _mass);

					var hit = Physics2D.Raycast(transform.position, _force.normalized, _force.magnitude, 1 << LayerMask.NameToLayer("Wall"));
					transform.position += new Vector3(_force.x, _force.y, 0);
					_traveledDistance = new Vector2(transform.position.x, transform.position.y) - _initialPosition;
					if (hit)
						Hittest(hit);
				}
			}

			void UpdateCam() {
				var pos = transform.position;
				pos.z = Camera.main.transform.position.z;
				Camera.main.transform.position = pos;
			}

			void FixedUpdate() {
				Playing();
			}

			public void Hittest(RaycastHit2D hit) {
				GameInput.Instance.Playing.Disable();
				gameObject.SetActive(false);
				var flower = _flowerPool.Get();
				flower.transform.position = hit.point;
				flower.SetActive(true);
				_gameEvent.OnFinished.Invoke();
			}
		}
	}

}