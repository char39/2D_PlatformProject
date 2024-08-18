using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        [System.Serializable]
        public class LayerMasks
        {
            public LayerMask Block;         // Block 레이어.    지금은 인스펙터에서 설정함
            public LayerMask Player;        // Player 레이어.   지금은 인스펙터에서 설정함
        }
    }
}