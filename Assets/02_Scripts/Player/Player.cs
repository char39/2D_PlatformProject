using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    [System.Serializable]
    public class LayerMasks
    {
        public LayerMask Block;
        public LayerMask Player;
    }
    public class DrawGizmos : MonoBehaviour
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

    public class Player : MonoBehaviour
    {
        [HideInInspector] public Vector2 velocity;          // 플레이어의 속도. (Editor폴더의 스크립트에서 사용됨. Inspector 빠른 갱신을 위함)

        public LayerMasks layer;                            // LayerMasks 클래스
        public PlayerInput playerInput;                     // PlayerInput 스크립트
        public DrawGizmos playerGizmos;                     // DrawGizmos 클래스
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
            StartComponents();
            StartSetting();
        }
        void FixedUpdate()                  // void FixedUpdate()
        {
            MoveUpdate();
        }
        void Update()                       // void Update()
        {
            GetValueUpdate();
        }
        //----------------------------------------------------------------------------------//
        private void StartComponents()          // 컴포넌트 초기화
        {
            playerInput = GetComponent<PlayerInput>();
            playerGizmos = gameObject.AddComponent<DrawGizmos>();
            tr = GetComponent<Transform>();
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }
        private void StartSetting()             // 초기 설정
        {
            rb.velocity = Vector2.zero;                                                             // 속도 초기화
            rb.gravityScale = 2f;                                                                   // 중력 설정
            rb.bodyType = RigidbodyType2D.Dynamic;                                                  // Dynamic으로 설정
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;   // 회전 제한 설정
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;                        // 충돌 감지 설정
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;                                // 보간 설정
        }

        private void GetValueUpdate()           // 값 할당 업데이트
        {
            velocity = rb.velocity;                                                             // 플레이어의 속도를 할당함. (런타임 수치 확인용)
            horizontal = isPlayer1p ? playerInput.Horizontal_1p : playerInput.Horizontal_2p;    // 1P 2P에 따라 입력값을 받아옴
            vertical = isPlayer1p ? playerInput.Vertical_1p : playerInput.Vertical_2p;          // 1P 2P에 따라 입력값을 받아옴
        }

        private void MoveUpdate()
        {
            if (horizontal != 0)
                rb.velocity = new Vector2(horizontal * 5f, rb.velocity.y);
        }

        private void OnDrawGizmos()             // Gizmos 표현
        {
            if (HideGizmos || !Application.isPlaying || playerGizmos == null) return;
            playerGizmos.DrawVectorGizmos(tr, rb.velocity, Color.green);
        }
    }
}