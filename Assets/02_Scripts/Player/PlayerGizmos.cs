using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        public class DrawGizmos
        {
            /// <summary> 인스턴스의 실시간 벡터를 표현. </summary>
            public void DrawVectorGizmos(Transform tr, Vector2 velocity, Color color)
            {
                Gizmos.color = color;
                Gizmos.DrawLine(tr.position, tr.position + (Vector3)velocity);
            }
            /// <summary> 미정 </summary>
            public void DrawBlockGizmos()
            {

            }
        }
    }
}