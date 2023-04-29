using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEditor.PackageManager.Requests;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Elang.LD53
{
	public class GameData : Singleton<GameData>
	{
		[SerializeField]
		InputActionReference _addSunshine1000;

		[SerializeField]
        InputActionReference _upgradeSunshineSpawnRate;
		[SerializeField]
		InputActionReference _upgradeSunshineCount;

#if UNITY_EDITOR
		void Start() {
            GameInput.Instance.DebugC.Enable();
		}
#endif

		int Sunshine;
		
        int SunshineSpawnRateLevel = 0;
        List<float> SunshineSpawnRateData = new List<float>() { 5.0f, 3.0f, 2.0f };
        public float SunshineSpawnRate { get { return SunshineSpawnRateData[SunshineSpawnRateLevel]; } }

		int SunshineCountLevel = 0;
		List<Vector2Int> SunshineCountData = new List<Vector2Int>() { 
            new Vector2Int(1, 3),
			new Vector2Int(2, 4),
			new Vector2Int(3, 4),
			new Vector2Int(3, 5),
			new Vector2Int(3, 6),
			new Vector2Int(4, 6),
			new Vector2Int(4, 7),
			new Vector2Int(4, 8),
			new Vector2Int(5, 9),
			new Vector2Int(6, 10),
		};
		public Vector2Int SunshineCount { get { return SunshineCountData[SunshineCountLevel]; } }

		//int WindFloatingInterval = 0;
		//int WindFloatPower = 0;
		//int BoostPower = 0;
		//int BoostDuration = 0;

        public void UpgradeSunshineSpawnRate() {
            SunshineSpawnRateLevel++; 
            if (SunshineSpawnRateLevel >= SunshineSpawnRateData.Count)
                SunshineSpawnRateLevel--;
            else { Debug.Log($"Upgrade Sunshine Rate {SunshineSpawnRateLevel}"); } // upgrade wow
        }

		public void UpgradeSunshineCount() {
			SunshineCountLevel++;
			if (SunshineCountLevel >= SunshineCountData.Count)
				SunshineCountLevel--;
			else { Debug.Log($"Upgrade Sunshine Count {SunshineCountLevel}"); } // upgrade wow
		}

		public void AddSunshine(int value) {
            Sunshine += value;
			GameUI.Instance.SetSun(Sunshine);
		}

		void Update() {
            if (_addSunshine1000.action.triggered)
                AddSunshine(1000);
			if (_upgradeSunshineSpawnRate.action.triggered)
                UpgradeSunshineSpawnRate();
			if (_upgradeSunshineCount.action.triggered)
				UpgradeSunshineCount();
		}
	}
}

/**
#if UNITY_EDITOR
    [CustomEditor(typeof(#SCRIPTNAME#))]
    public class #SCRIPTNAME#Editor : Editor
    {
        #SCRIPTNAME# #SCRIPTNAME#var { get { return target as #SCRIPTNAME#; } }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            // Inspect Here w/ EditorGUILayout Fields

            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(#SCRIPTNAME#var);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
#endif 
/**/