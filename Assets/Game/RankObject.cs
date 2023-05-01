using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Elang.LD53
{
	public class RankObject : MonoBehaviour
	{
		[SerializeField]
		TextMeshProUGUI _name;
		[SerializeField]
		TextMeshProUGUI _field;

		public void SetFields(string name, string field) {
			_name.text = name;
			_field.text = field;
		}

	}
}