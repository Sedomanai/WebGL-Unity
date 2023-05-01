using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Elang.LD53
{
	public class GameData : Singleton<GameData>, ILeaderBoardRequester
	{
		[SerializeField]
		InputActionReference _addSunshine1000;

		[SerializeField]
        InputActionReference _upgradeSunshineSpawnRate;
		[SerializeField]
		InputActionReference _upgradeSunshineCount;

		[SerializeField]
		RankingNetwork _network;

		[SerializeField]
		RankUI _rankUI;
		[SerializeField]
		RankObject[] _myRank;

		[SerializeField]
		TextMeshProUGUI _nameField;

#if UNITY_EDITOR
		void Start() {
            GameInput.Instance.DebugC.Enable();
		}
#endif

		int sunshine;
		public int Sunshine { get { return sunshine; } }

		public struct Stat<T> {
			int level;
			List<T> data;
			List<int> cost;
			public T Current { get { return data[level]; } }
			public int Level { get { return level; } }

			public int NextCost() {
				return (level < 5) ? cost[level] : -1;
			}

			public void Upgrade() {
				level++;
				if (level >= data.Count)
					level--;
				//else { Debug.Log($"Upgrade {GetType().Name} Rate {level}"); } // upgrade wow
			}

			public Stat(List<T> data_, List<int> cost_) { level = 0; data = data_; cost = cost_; }
		}

		public Stat<float> SunshineSpawn = new Stat<float>(new List<float>() { 5.0f, 4.5f, 4.0f, 3.5f, 3.0f, 2.5f }, new List<int>() { 20, 50, 110, 200, 320 });
		public Stat<float> InitialVelocity = new Stat<float>(new List<float>() { 1200, 1600, 2000, 2500, 3000, 4000 }, new List<int>() { 50, 100, 180, 250, 350 });
		public Stat<float> BoostDuration = new Stat<float>(new List<float>() { 60, 65, 72, 81, 92, 105, 130 }, new List<int>() { 40, 135, 225, 305, 500 });
		public Stat<float> ControlCap = new Stat<float>(new List<float>() { 7000, 7800, 8800, 10000, 11400, 13000 }, new List<int>() { 20, 60, 120, 240, 400 });
		public Stat<float> MeteorSpawn = new Stat<float>(new List<float>() { 5.0f, 6.0f, 8.0f, 11.0f, 15.0f, 20.0f }, new List<int>() { 80, 140, 250, 330, 500 });

		public Stat<Vector2Int> SunshineCount = new Stat<Vector2Int>(new List<Vector2Int>() {
				new Vector2Int(3, 4)
			}, new List<int>() { 0 });

		public Stat<float> BirdSpawn = new Stat<float>(new List<float>() { 10.0f }, new List<int>() { 0 });
		public Stat<Vector2Int> BirdCount = new Stat<Vector2Int>(new List<Vector2Int>() {
				new Vector2Int(1, 2),
			}, new List<int>() { 0 });


		public Stat<Vector2Int> MeteorCount = new Stat<Vector2Int>(new List<Vector2Int>() {
				new Vector2Int(2, 3),
			}, new List<int>() { 0 });

		public void AddSunshine(int value) {
            sunshine += value;
			GameUI.Instance.SetSun(sunshine);
		}

		float MaxAltitude = 0.0f, MaxVelocity = 0.0f;
		float PrevAltitude = 0.0f, PrevVelocity = 0.0f;

		bool _altitude = false;
		public void OnGameEnd(float maxAltitude, float maxVelocity) {
			PrevAltitude = maxAltitude; PrevVelocity = maxVelocity;
			MaxAltitude = Mathf.Max(maxAltitude, MaxAltitude);	
			MaxVelocity	= Mathf.Max(maxVelocity, MaxVelocity);
			_altitude = false;

			var name = (_nameField.text == null || _nameField.text == "") ? "Nan" : _nameField.text;
			if ((MaxAltitude < maxAltitude && maxAltitude > 10000) || (MaxVelocity < maxVelocity && maxVelocity > 10.0f))
				_network.SendRank(new Rank(name, MaxVelocity, MaxAltitude), this);
			_myRank[0].SetFields("Current", maxAltitude.ToString());
			_myRank[1].SetFields("Highest", MaxAltitude.ToString());
		}

		public void QueryByAltitude() {
			_altitude = true;
			_network.GetRankByScore(this);
			_myRank[0].SetFields("Current", PrevAltitude.ToString());
			_myRank[1].SetFields("Highest", MaxAltitude.ToString());
		}
		public void QueryByVelocity() {
			_altitude = false;
			_network.GetRankByTime(this);
			_myRank[0].SetFields("Current", PrevVelocity.ToString());
			_myRank[1].SetFields("Highest", MaxVelocity.ToString());
		}


		//public void InsertRank() {
		//	network.SendRank(new Rank(nametext.text, int.Parse(time.text), int.Parse(score.text)), this);
		//}
		//public void SortByTime() {
		//	network.GetRankByTime(this);
		//}
		//public void SortByScore() {
		//	network.GetRankByScore(this);
		//}

		public void onGetRankList(List<Rank> rank) {
			_rankUI.ListRanks(rank, _altitude);
		}

		void Update() {
            if (_addSunshine1000.action.triggered)
                AddSunshine(1000);
			if (_upgradeSunshineSpawnRate.action.triggered) {
				SunshineSpawn.Upgrade();
				BirdSpawn.Upgrade();
				MeteorSpawn.Upgrade();
			}
				
			if (_upgradeSunshineCount.action.triggered) {
				SunshineCount.Upgrade();
				BirdCount.Upgrade();
				MeteorCount.Upgrade();
			}
				
		}

	}
}