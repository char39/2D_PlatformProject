using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player : MonoBehaviour
    {
        [HideInInspector] public Vector2 velocity;          // 플레이어의 속도. (Editor폴더의 스크립트에서 사용됨. Inspector 빠른 갱신을 위함)

        public LayerMasks layer;                            // LayerMasks 중첩 클래스
        public DrawGizmos drawGizmos;                       // DrawGizmos 중첩 클래스
        public PlayerInput playerInput;                     // PlayerInput 스크립트
        public PlayerMovement playerMovement;               // PlayerMovement 스크립트
        internal Transform tr;
        internal SpriteRenderer sr;
        internal Rigidbody2D rb;

        [Header("Setting")]
        public bool isPlayer1p = true;                      // 1P or 2P
        public bool HideGizmos = false;                     // Gizmos Draw 여부

        private int horizontal;                             // 좌우 입력값
        private int vertical;                               // 상하 입력값
        //----------------------------------------------------------------------------------//
        void Start()                        // void Start()
        {
            StartComponents();                  // 컴포넌트 초기화
            StartSetting();                     // 초기 설정
        }
        void FixedUpdate()                  // void FixedUpdate()
        {
            MoveUpdate();
        }
        void Update()                       // void Update()
        {
            GetValueUpdate();                   // 값 할당 업데이트
        }
        //----------------------------------------------------------------------------------//

        private void GetValueUpdate()           // 값 할당 업데이트
        {
            velocity = rb.velocity;                                                             // 플레이어의 속도를 할당함. (런타임 수치 확인용)
            horizontal = isPlayer1p ? playerInput.Horizontal_1p : playerInput.Horizontal_2p;    // 1P 2P에 따라 입력값을 받아옴
            vertical = isPlayer1p ? playerInput.Vertical_1p : playerInput.Vertical_2p;          // 1P 2P에 따라 입력값을 받아옴
        }

        private void MoveUpdate()                           // 아직 테스트중
        {
            //if (horizontal != 0)
            rb.velocity = new Vector2(horizontal * 10f, rb.velocity.y);
        }

        private void OnDrawGizmos()             // Gizmos 표현
        {
            if (HideGizmos || !Application.isPlaying || drawGizmos == null) return;
            drawGizmos.DrawVectorGizmos(tr, rb.velocity, Color.green);
        }
    }
}