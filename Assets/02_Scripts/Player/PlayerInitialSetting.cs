using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        private void StartComponents()          // 컴포넌트 초기화
        {
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
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
    }
}