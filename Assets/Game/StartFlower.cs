using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Elang
{
    namespace LD53 
	{
		public class StartFlower : MonoBehaviour
		{
			[SerializeField]
			ObjectPool _flowerPool;
			
			// Start is called before the first frame update
			void Awake() {
				var flower = _flowerPool.Get();
				flower.transform.position = transform.position;
				flower.SetActive(true);
				SoundMgr.Instance.PlayBGM("bgm");
			}
		}

	}
}