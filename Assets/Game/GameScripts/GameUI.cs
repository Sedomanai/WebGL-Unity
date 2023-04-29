using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang.LD53
{
	public class GameUI : Singleton<GameUI>
	{
		public TextMesh windLevelText;
		public TextMesh traveledDistanceText;
		[SerializeField]
		TextMesh sunText;
		[SerializeField]
		TextMesh floatingText;

		public void SetSun(int value) {
			sunText.text = $"Sunshine ${value}";
		}
		public void TurnFloatingTextOn() {
			floatingText.text = "Floating";
		}
		public void TurnFloatingTextOff() {
			floatingText.text = "Not Floating";
		}
	}

}
