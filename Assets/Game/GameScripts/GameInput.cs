using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Elang.LD53
{
	public class GameInput : Singleton<GameInput>
	{
		[SerializeField]
		InputActionAsset _controls;

		public InputActionMap Startup { private set; get; }
		public InputActionMap Playing { private set; get; }
		public InputActionMap Menu { private set; get; }
		public InputActionMap DebugC { private set; get; }

		// Start is called before the first frame update
		void Awake() {
			Startup = _controls.FindActionMap("Startup");
			Playing = _controls.FindActionMap("Playing");
			Menu = _controls.FindActionMap("Menu");
			DebugC = _controls.FindActionMap("Debug");
		}
	}

}

