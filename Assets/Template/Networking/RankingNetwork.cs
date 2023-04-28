using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Elang
{
   [System.Serializable]
   public struct Rank
   {
      public string name;
      public float time;
      public int score;

      public Rank(string name, float time, int score) {
         this.name = name;
			this.time = time;
			this.score = score;
		}
   }
   public interface ILeaderBoardRequester
   {
      void onGetRankList(List<Rank> rank);
   }
   
   public class RankingNetwork : MonoBehaviour
   {
      [System.Serializable]
      class RankList
      {
         public List<Rank> ranks;
      }

      public void SendRank(Rank rank, ILeaderBoardRequester requester) {
         StartCoroutine(SendRankCO(rank, requester));
      }

      public void GetRankByScore(ILeaderBoardRequester requester) {
         StartCoroutine(GetRankByScoreCO(requester));
      }

      public void GetRankByTime(ILeaderBoardRequester requester) {
         StartCoroutine(GetRankByTimeCO(requester));
      }

      IEnumerator SendRankCO(Rank rank, ILeaderBoardRequester requester) {
			// RankRoutes.cs is not included in the git for security purposes.
			using (UnityWebRequest req = new UnityWebRequest(Networking.Host + ExpressRoutes.EL_WWW_POST_RANK, UnityWebRequest.kHttpVerbPOST)) {
				req.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(rank)));
				req.downloadHandler = new DownloadHandlerBuffer();
				req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.Success) {
               requester.onGetRankList(JsonUtility.FromJson<RankList>(req.downloadHandler.text).ranks);
            }
         }
      }
      IEnumerator GetRankByScoreCO(ILeaderBoardRequester requester) {
         // RankRoutes.cs is not included in the git for security purposes.
         using (UnityWebRequest req = UnityWebRequest.Get(Networking.Host + ExpressRoutes.EL_WWW_GET_RANK_SCORE)) {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.Success) {
               requester.onGetRankList(JsonUtility.FromJson<RankList>(req.downloadHandler.text).ranks);
            }
         }
      }
      IEnumerator GetRankByTimeCO(ILeaderBoardRequester requester) {
         // RankRoutes.cs is not included in the git for security purposes.
         using (UnityWebRequest req = UnityWebRequest.Get(Networking.Host + ExpressRoutes.EL_WWW_GET_RANK_TIME)) {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.Success) {
               requester.onGetRankList(JsonUtility.FromJson<RankList>(req.downloadHandler.text).ranks);
            }
         }
      }
   }
}