using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Elang.LD53
{
   public class Networking : MonoBehaviour
   {
      public const string Host =
#if UNITY_EDITOR
    "http://localhost:3001";
#else
    ExpressRoutes.EL_WWW_HOST_NAME;
#endif
    }

}
