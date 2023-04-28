using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Elang
{
    [System.Serializable]
    public class Rank
    {
        public string name;
        public float time;
        public int score;
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

        public void GetRankByScore(Rank rank, ILeaderBoardRequester requester) {
            StartCoroutine(GetRankByScoreCO(rank, requester));
        }

        public void GetRankByTime(Rank rank, ILeaderBoardRequester requester) {
            StartCoroutine(GetRankByTimeCO(rank, requester));
        }

        IEnumerator SendRankCO(Rank rank, ILeaderBoardRequester requester) {
            // RankRoutes.cs is not included in the git for security purposes.
            using (UnityWebRequest req = UnityWebRequest.Post(Networking.Host + RankRoutes.EL_POST_RANK, JsonUtility.ToJson(rank))) {
                req.SetRequestHeader("Content-Type", "application/json");
                yield return req.SendWebRequest();
                if (req.result == UnityWebRequest.Result.Success) {
                    requester.onGetRankList(JsonUtility.FromJson<RankList>(req.downloadHandler.text).ranks);
                }
            }
        }
        IEnumerator GetRankByScoreCO(Rank rank, ILeaderBoardRequester requester) {
            // RankRoutes.cs is not included in the git for security purposes.
            using (UnityWebRequest req = UnityWebRequest.Get(Networking.Host + RankRoutes.EL_GET_RANK_SCORE)) {
                yield return req.SendWebRequest();
                if (req.result == UnityWebRequest.Result.Success) {
                    requester.onGetRankList(JsonUtility.FromJson<RankList>(req.downloadHandler.text).ranks);
                }
            }
        }
        IEnumerator GetRankByTimeCO(Rank rank, ILeaderBoardRequester requester) {
            // RankRoutes.cs is not included in the git for security purposes.
            using (UnityWebRequest req = UnityWebRequest.Get(Networking.Host + RankRoutes.EL_GET_RANK_TIME)) {
                yield return req.SendWebRequest();
                if (req.result == UnityWebRequest.Result.Success) {
                    requester.onGetRankList(JsonUtility.FromJson<RankList>(req.downloadHandler.text).ranks);
                }
            }
        }
    }
}