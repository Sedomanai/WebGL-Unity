using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Elang.LD53
{
	public class BirbEnabler : MonoBehaviour
	{
		[SerializeField]
		Projectile2D _projectile;

		public void EnableBirb() {
			_projectile.PreviousState();
		}
	}

}
