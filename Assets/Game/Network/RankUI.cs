using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Elang.LD53
{
	public class RankUI : MonoBehaviour
	{
		RankObject[] tr;

		void OnEnable() {
			GetRankObjects();
		}

		void GetRankObjects() {
			if (tr == null) {
				tr = GetComponentsInChildren<RankObject>();
				foreach (RankObject obj in tr) {
					obj.gameObject.SetActive(false);
				}
			}
		}

		// Update is called once per frame
		public void ListRanks(List<Rank> rank, bool altitude = true) {
			GetRankObjects();
			for (int i = 0; i < tr.Length; i++) {
				if (i < rank.Count) {
					tr[i].gameObject.SetActive(true);
					var field = altitude ? rank[i].score.ToString() : rank[i].time.ToString();
					tr[i].SetFields(rank[i].name, field);
				}
			}
		}
	}

}