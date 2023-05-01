using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class ItemDisabler : MonoBehaviour
	{
		[SerializeField]
		GameObject obj;
		
		void Disable() {
			obj.gameObject.SetActive(false);
		}
	}


}
