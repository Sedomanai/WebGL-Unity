using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Elang
{
    public class Networking : MonoBehaviour
    {
        public const string Host =
#if UNITY_EDITOR
    "http://localhost:3001";
#else
    "https://twowolf-ld53.herokuapp.com"; // change this to your domain.
#endif
    }

}
