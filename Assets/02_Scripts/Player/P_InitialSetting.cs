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
            rb.gravityScale = 10f;                                                                   // 중력 설정
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
            Cooldown = 0.05f;                                   // 해당 시간동안 BoxCast를 사용하지 않음 (Cast 무시)
            boxLength = 0.05f;                                  // BoxCast의 두께
            boxMaxDist = 2.0f;                                  // BoxCast를 판별하는 최대 거리
            boxHorizontalSize = new Vector2(col_x, boxLength);      // Vector2(col_x, boxLength). Vertical에서 사용하는 BoxCast
            boxVerticalSize = new Vector2(boxLength, col_y);        // Vector2(boxLength, col_y). Horizontal에서 사용하는 BoxCast
            hitboxOffsetDown = new Vector2(0f, 0f);         // 아래쪽 BoxCast가 충돌 거리 안으로 들어간 거리. 초기화
            hitboxOffsetLeft = new Vector2(0f, 0f);         // 왼쪽 BoxCast가 충돌 거리 안으로 들어간 거리. 초기화
            hitboxOffsetRight = new Vector2(0f, 0f);        // 오른쪽 BoxCast가 충돌 거리 안으로 들어간 거리. 초기화
            hitboxOffsetUp = new Vector2(0f, 0f);           // 위쪽 BoxCast가 충돌 거리 안으로 들어간 거리. 초기화
        }
        /// <summary> 초기 설정. <para> Movement 변수 초기화 </para> </summary>
        private void Start_Movement_Set()
        {
            originalGravityScale = rb.gravityScale;                 // 초기 중력 설정값 저장
            jumpForce = 50f;
        }
    }
}