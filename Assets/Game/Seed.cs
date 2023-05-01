using UnityEngine;
using UnityEngine.InputSystem;

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
			//[SerializeField]
			//float _initialVelocity;
			[SerializeField]
			float _boostPower = 15.0f;
			[SerializeField]
			float _controlPower = 2.0f;
			[SerializeField]
			float _controlCap = 0.2f;

			[SerializeField]
			Wind _wind;

			[SerializeField]
			ObjectPool _flowerPool;


			[SerializeField]
			float _gravity;
			float _windFriction = 45.0f;

			int _boostDuration = 0;
			float _boostVelocity = 0.0f, _controlVelocity = 0.0f, _windVelocity = 0.0f;

			Vector2 _force = new Vector2();
			Vector2 _finalForce = new Vector2();
			public Vector2 Force { get { return _finalForce; } }

			Vector2 _angularShift;
			public Vector2 AngularShift { get { return _angularShift; } }

			float _groundAltitude, _altitude;
			public float Altitude { get { return _altitude; } }

			float _maxAltitude, _maxVelocity;

			float _forceVelocity;
			float _traveledDistance;

			[SerializeField]
			GameEvent _gameEvent;

			[SerializeField]
			Boosts _boosts;
			[SerializeField]
			Boosts _diffuser;

			Animator[] _anim;

			void Awake() {
				gameObject.SetActive(false);
				var circ = GameObject.FindGameObjectWithTag("Respawn").GetComponent<CircleCollider2D>();
				_anim = GetComponentsInChildren<Animator>();
				_groundAltitude = circ.radius * circ.transform.lossyScale.y;
				_gameEvent.Fly.AddListener(Fly);
				_gameEvent.Strike.AddListener(Strike);
			}

			void OnEnable() {
				_maxVelocity = _maxAltitude = 0.0f;

				_anim[0].gameObject.SetActive(false);
				_anim[1].gameObject.SetActive(false);
				_force = new Vector2();
				_boostVelocity = _controlVelocity = _windVelocity = 0.0f;
				_boostDuration = 0;
				_fire.action.Reset();
				GameInput.Instance.Startup.Enable();
			}

			// Update is called once per frame
			void Update() {
				Startup();
				if (_boostDuration < 2 && _boost.action.triggered) {
					if (_boosts.Use()) {
						SoundMgr.Instance.PlaySFX("boost");
						_boostDuration = (int)GameData.Instance.BoostDuration.Current;
						_anim[1].gameObject.SetActive(true);
						_anim[1].Play("boost");
					}
				}
					
				UpdateCam();
			}

			void Startup() {
				if (GameInput.Instance.Startup.enabled && _fire.action.triggered) {
					SoundMgr.Instance.PlaySFX("start");
					GameInput.Instance.Menu.Enable();
					_anim[0].gameObject.SetActive(true);
					_forceVelocity = _mass * GameData.Instance.InitialVelocity.Current;
					GameInput.Instance.Startup.Disable();
					GameInput.Instance.Playing.Enable();
					_gameEvent.OnStart.Invoke();
					_traveledDistance = 0;
					float angle = -GameMath.Vector2ToAngle(transform.position) * Mathf.Rad2Deg - 90;
					transform.eulerAngles = new Vector3(0, 0, angle);
					_anim[0].Play("seed_begin");
				}
			}

			void Playing() {
				if (GameInput.Instance.Playing.enabled) {
					var origin = transform.position;
					var mag = origin.magnitude;
					_altitude = (mag - _groundAltitude) * 60.0f;
					_maxAltitude = Mathf.Max(_altitude, _maxAltitude);

					var dir = GameMath.DirectionFromOrigin(origin);
					LateralControl();
					BoostControl();
					WindControl();

					var angle = GameMath.ShiftedAngle(GameMath.Vector2ToAngle(dir), _controlVelocity + _windVelocity);
					transform.position = GameMath.AngleToVector2(angle, mag);
					_angularShift = transform.position - origin;

					//recalc
					dir = GameMath.DirectionFromOrigin(transform.position);
					float buoyancy = _boostVelocity + _wind.FloatLevel - _gravity;

					_forceVelocity += _mass * buoyancy;
					_force = dir * _forceVelocity;

					_finalForce = _angularShift + _force;
					var fmag = _finalForce.magnitude * 60;
					_traveledDistance += fmag;

					var hit = Physics2D.Raycast(origin, _finalForce.normalized, _finalForce.magnitude, 1 << LayerMask.NameToLayer("Wall"));
					transform.position += new Vector3(_force.x, _force.y, 0);

					_maxVelocity = Mathf.Max(fmag, _maxVelocity);
					var fangle = -angle * Mathf.Rad2Deg;

					GameUI.Instance.SetDistance(_traveledDistance);
					GameUI.Instance.SetVelocity(fmag);
					GameUI.Instance.SetAltitude(_altitude);
					GameUI.Instance.SetAngle(fangle);
					transform.eulerAngles = new Vector3(0, 0, fangle - 90);

					if (hit)
						Hittest(hit);
				}
			}

			void WindControl() {
				var mult = _mass / _altitude;
				var power = 50 * mult;
				var target = _wind.WindLevel * 200 * mult;

				if (_windVelocity < target) {
					_windVelocity += power;
					if (_windVelocity > target)
						_windVelocity = target;
				} else if (_windVelocity > target) {
					_windVelocity -= power;
					if (_windVelocity < target)
						_windVelocity = target;
				}

				GameUI.Instance.SetWindLevel(_wind.WindLevel);
			}

			void Fly() {
				if (_forceVelocity < -0.01f) {
					_forceVelocity = _mass * 450.0f;
				} else
					_forceVelocity += _mass * 20.0f;
			}
			void Strike() {
				if (!_diffuser.Use()) {
					if (_forceVelocity > 0.05f) {
						_forceVelocity = _mass * -250.0f;
					} else
						_forceVelocity -= _mass * 120.0f;
				}
			}

			void BoostControl() {
				_boostVelocity = 0;
				if (_boostDuration > 0) {
					_boostVelocity = _boostPower;
					_boostDuration--;
					if (_boostDuration == 0)
						_anim[1].gameObject.SetActive(false);
				}
			}

			void LateralControl() {
				var mult = _mass / _altitude;
				var power = _controlPower * mult;
				var hori = _horizontal.action.ReadValue<float>();
				if (hori == 0.0f) {
					if (_controlVelocity > 0) {
						_controlVelocity -= power * 3;
						if (_controlVelocity < 0)
							_controlVelocity = 0;
					} else if (_controlVelocity < 0) {
						_controlVelocity += power * 3;
						if (_controlVelocity > 0)
							_controlVelocity = 0;
					}
				} else
					_controlVelocity += power * hori;

				var cap = _controlCap * mult;
				_controlVelocity = Mathf.Clamp(_controlVelocity, -cap, cap);
				GameUI.Instance.SetControlAxis((_controlVelocity / mult) / _controlCap);
				_controlVelocity *= (1.0f - _windFriction * mult);
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
				var flower = _flowerPool.Get();
				flower.transform.position = hit.point;
				flower.transform.rotation = Quaternion.Euler(0, 0, 180 - GameMath.Vector2ToAngle(Vector2.Perpendicular(hit.normal)) * Mathf.Rad2Deg);
				flower.SetActive(true);
				_gameEvent.OnFinished.Invoke();
				gameObject.SetActive(false);

				GameData.Instance.OnGameEnd(_maxAltitude, _maxVelocity);
			}
		}
	}

}