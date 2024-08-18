using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        /// <summary> 컴포넌트 초기화. <para> 스크립트도 불러옴 </para> </summary>
        private void Start_Components()
        {
            drawGizmos = GetComponent<DrawGizmos>();
            playerInput = GetComponent<PlayerInput>();
            tr = GetComponent<Transform>();
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            bc = GetComponent<BoxCollider2D>();
        }
        /// <summary> 초기 설정. <para> Rigidbody2D 초기화 </para> </summary>
        private void Start_Rigid2D_Set()
        {
            rb.velocity = Vector2.zero;                                                             // 속도 초기화
            rb.gravityScale = 2f;                                                                   // 중력 설정
            rb.bodyType = RigidbodyType2D.Dynamic;                                                  // Dynamic으로 설정
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;   // 회전 제한 설정
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;                        // 충돌 감지 설정
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;                                // 보간 설정
        }
        /// <summary> 초기 설정. <para> BoxCast 변수 초기화 </para> </summary>
        private void Start_BoxCast_Set()
        {
            col_x = bc.size.x;                                  // BoxCollider2D의 x축 길이
            col_y = bc.size.y;                                  // BoxCollider2D의 y축 길이
            Cooldown = 0.05f;                                   // 해당 시간동안 BoxCast를 사용하지 않음
            boxLength = 0.05f;                                  // BoxCast의 길이
            boxMaxDist = 2.0f;                                  // BoxCast를 판별하는 최대 거리
            boxHorizontalSize = new Vector2(col_x, boxLength);      // Vector2(col_x, boxLength). Vertical에서 사용하는 BoxCast
            boxVerticalSize = new Vector2(boxLength, col_y);        // Vector2(boxLength, col_y). Horizontal에서 사용하는 BoxCast
        }
        /// <summary> 초기 설정. <para> Movement 변수 초기화 </para> </summary>
        private void Start_Movement_Set()
        {
            jumpForce = 15f;
        }
    }
}