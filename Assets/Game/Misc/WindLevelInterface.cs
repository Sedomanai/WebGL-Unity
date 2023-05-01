using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elang.LD53
{
	public class WindLevelInterface : MonoBehaviour
	{
		[SerializeField]
		Sprite _none;
		[SerializeField]
		Sprite[] _left;
		[SerializeField]
		Sprite[] _right;
		[SerializeField]
		Image _leftImage;
		[SerializeField]
		Image _rightImage;


		public void SetLevel(int level) {
			_leftImage.sprite = _none;
			_rightImage.sprite = _none;
			if (level > 0)
				_rightImage.sprite = _right[level - 1];
			else if (level < 0)
				_leftImage.sprite = _left[-level - 1];

		}


	}
}
