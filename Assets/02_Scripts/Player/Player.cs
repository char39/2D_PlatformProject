using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        [System.Serializable]
        public class Settings
        {
            public bool isPlayer1p = true;
            public bool hideGizmos = false;
        }
        [HideInInspector]
        public Vector2 velocity;                            // 플레이어의 속도. (Editor폴더의 스크립트에서 사용됨. Inspector 빠른 갱신을 위함)

        public Settings settings;                           // Settings 중첩 클래스
        public LayerMasks layer;                            // LayerMasks 중첩 클래스
        public DrawGizmos drawGizmos;                       // DrawGizmos 스크립트
        public PlayerInput playerInput;                     // PlayerInput 스크립트

        internal Transform tr;
        internal SpriteRenderer sr;
        internal Rigidbody2D rb;
        internal BoxCollider2D bc;

        private int horizontal;                             // 좌우 입력값
        private int vertical;                               // 상하 입력값
    }

    public partial class Player : MonoBehaviour
    {
        private bool IsPlayer1p { get { return settings.isPlayer1p; } }      // 1P or 2P
        private bool HideGizmos { get { return settings.hideGizmos; } }      // Gizmos Draw 여부

        //----------------------------------------------------------------------------------//
        void Awake()                        // void Awake()
        {
            Start_Components();
            Start_Rigid2D_Set();
            Start_BoxCast_Set();
            Start_Movement_Set();
        }
        void Start()                        // void Start()
        {

        }
        void FixedUpdate()                  // void FixedUpdate()
        {
            MoveUpdate();
        }
        void Update()                       // void Update()
        {
            GetValueUpdate();
            JumpUpdate();

            CoolTimersUpdate();

            BoxHorizontalCast(ref hitboxRight, ref rightCoolTimer, ref isRightTouch, true);     // 오른쪽 BoxCast
            BoxHorizontalCast(ref hitboxLeft, ref leftCoolTimer, ref isLeftTouch, false);       // 왼쪽 BoxCast
            BoxVerticalCast(ref hitboxUp, ref upCoolTimer, ref isUpTouch, true);                // 위쪽 BoxCast
            BoxVerticalCast(ref hitboxDown, ref downCoolTimer, ref isDownTouch, false);         // 아래쪽 BoxCast

            GetHitboxOffset();

            HorizontalPosLock(hitboxRight, ref isRightTouch, true);                         // 오른쪽 BoxCast 위치 고정
            HorizontalPosLock(hitboxLeft, ref isLeftTouch, false);                          // 왼쪽 BoxCast 위치 고정
            VerticalPosLock(hitboxUp, ref isUpTouch, true);                                 // 위쪽 BoxCast 위치 고정
            VerticalPosLock(hitboxDown, ref isDownTouch, false);                            // 아래쪽 BoxCast 위치 고정

        }
        //----------------------------------------------------------------------------------//

        private void GetValueUpdate()           // 값 할당 업데이트
        {
            velocity = rb.velocity;                                                             // 플레이어의 속도를 할당함. (런타임 수치 확인용)
            horizontal = IsPlayer1p ? playerInput.Horizontal_1p : playerInput.Horizontal_2p;    // 1P 2P에 따라 입력값을 받아옴
            vertical = IsPlayer1p ? playerInput.Vertical_1p : playerInput.Vertical_2p;          // 1P 2P에 따라 입력값을 받아옴
        }

        private void OnDrawGizmos()             // Gizmos 표현
        {
            if (HideGizmos || !Application.isPlaying || drawGizmos == null) return;
            drawGizmos.DrawVectorGizmos(tr, rb.velocity, Color.blue);
            drawGizmos.DrawHorizontalHitBoxGizmos(tr, hitboxRight, hitboxOffsetRightApply, boxVerticalSize, col_x, boxLength, boxMaxDist, true);
            drawGizmos.DrawHorizontalHitBoxGizmos(tr, hitboxLeft, hitboxOffsetLeftApply, boxVerticalSize, col_x, boxLength, boxMaxDist, false);
            drawGizmos.DrawVerticalHitBoxGizmos(tr, hitboxUp, hitboxOffsetUpApply, boxHorizontalSize, col_y, boxLength, boxMaxDist, true);
            drawGizmos.DrawVerticalHitBoxGizmos(tr, hitboxDown, hitboxOffsetDownApply, boxHorizontalSize, col_y, boxLength, boxMaxDist, false);
        }
    }
}