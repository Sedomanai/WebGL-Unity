using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Elang.LD53
{
	public class UpgradeBar : MonoBehaviour
	{
		[SerializeField]
		TextMeshProUGUI _cost;
		[SerializeField]
		GameObject[] _bars;
		[SerializeField]
		Button _button;

		bool _distivated = false;

		void OnEnable() {
			if (!_distivated) {
				foreach (var bar in _bars) {
					bar.gameObject.SetActive(false);
				} _distivated = true;
			}
		}

		public void SunshineUpgradeButton() {
			StatFlow(ref GameData.Instance.SunshineSpawn);
		}
		public void InitVelUpgradeButton() {
			StatFlow(ref GameData.Instance.InitialVelocity);
		}
		public void BoostDuraButton() {
			StatFlow(ref GameData.Instance.BoostDuration);
		}
		public void ControlCapButton() {
			StatFlow(ref GameData.Instance.ControlCap);
		}
		public void MeteorSpawn() {
			StatFlow(ref GameData.Instance.MeteorSpawn);
		}

		void StatFlow(ref GameData.Stat<float> stat) {
			if (_cost.text.Length == 0 || _cost.text == "Complete")
				return;

			var cost = int.Parse(_cost.text);
			if (GameData.Instance.Sunshine >= cost) {
				SoundMgr.Instance.PlaySFX("upgrade");
				GameData.Instance.AddSunshine(-cost);
				stat.Upgrade();
				var lvl = stat.Level;
				for (int i = 0; i < lvl; i++) {
					_bars[i].gameObject.SetActive(true);
				}

				var ncost = stat.NextCost();
				if (ncost >= 0) {
					_cost.text = stat.NextCost().ToString();
				} else {
					_cost.text = "Complete";
					_button.enabled = false;
				}
			}
		}
	}
}
