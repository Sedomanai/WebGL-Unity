using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elang
{
   public class RankUser : MonoBehaviour, ILeaderBoardRequester
    {
      [SerializeField]
      Text nametext, score, time;

      [SerializeField]
      RankingNetwork network;

      public void InsertRank() {
         network.SendRank(new Rank(nametext.text, int.Parse(time.text), int.Parse(score.text)), this);
		}
		public void SortByTime() {
         network.GetRankByTime(this);
		}
		public void SortByScore() {
			network.GetRankByScore(this);
		}

		public void onGetRankList(List<Rank> rank) {
        foreach(Rank rankItem in rank) {
            Debug.Log(rankItem.name + " " + rankItem.score + " " + rankItem.time);
        }
      }
   }

}
