using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        /// <summary> 컴포넌트 초기화. <para> 스크립트도 불러옴 </para> </summary>
        private void StartComponents()
        {
            drawGizmos = GetComponent<DrawGizmos>();
            playerInput = GetComponent<PlayerInput>();
            tr = GetComponent<Transform>();
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            bc = GetComponent<BoxCollider2D>();
        }
        /// <summary> 초기 설정. <para> Rigidbody2D 초기화 </para> </summary>
        private void StartSetting()
        {
            rb.velocity = Vector2.zero;                                                             // 속도 초기화
            rb.gravityScale = 2f;                                                                   // 중력 설정
            rb.bodyType = RigidbodyType2D.Dynamic;                                                  // Dynamic으로 설정
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;   // 회전 제한 설정
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;                        // 충돌 감지 설정
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;                                // 보간 설정
        }
        /// <summary> Movement 초기 변수 설정. <para> HitBox 크기 등 설정 </para> </summary>
        private void StartMovementSetting()
        {
            jumpForce = 10f;
            Cooldown = 0.05f;
            col_x = bc.size.x;
            col_y = bc.size.y;
            boxLength = 0.05f;
            boxMaxDist = 2.0f;
            boxHorizontalSize = new Vector2(boxLength, col_y);
            boxVerticalSize = new Vector2(col_x, boxLength);
        }
    }
}