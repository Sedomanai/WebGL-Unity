using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class GameSphere : MonoBehaviour
	{
		Seed _seed;

		[SerializeField]
		Color _firstColor;
		[SerializeField]
		Color _secondColor;
		[SerializeField]
		Color _thirdColor;
		[SerializeField]
		Color _fourthColor;
		[SerializeField]
		Color _fifthColor;
		[SerializeField]
		Color _sixthColor;

		[SerializeField]
		float first;
		[SerializeField]
		float sec;
		[SerializeField]
		float thir;
		[SerializeField]
		float fur;
		[SerializeField]
		float ff;

		Color _targetColor;

		void Start() {
			_targetColor = _firstColor;
		}

		public void GetSeed() {
			_seed = GameObject.FindGameObjectWithTag("Player").GetComponent<Seed>();
		}

		void Update() {
			if (_seed) {
				var alt = _seed.Altitude;
				if (alt < first)
					_targetColor = _firstColor;
				else if (alt < sec)
					_targetColor = _secondColor;
				else if (alt < thir)
					_targetColor = _thirdColor;
				else if(alt < fur)
					_targetColor = _fourthColor;
				else if (alt < ff)
					_targetColor = _fifthColor;
				else
					_targetColor = _sixthColor;
			}

			if (Camera.main.backgroundColor != _targetColor) {
				Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, _targetColor, 0.02f);
			}

		}
	}
}
