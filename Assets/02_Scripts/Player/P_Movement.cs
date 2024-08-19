using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        internal float originalGravityScale;            // 초기 중력 설정값 저장

        internal bool canJump;                          // 점프 가능한가
        internal bool isJump;                           // 점프 중인가
        internal bool isFall;                           // 낙하 중인가 (땅에서 떨어졌을 때)
        internal int jumpCount;                         // 점프 가능 횟수
        internal float jumpForce;                       // 점프력
        internal float maxFallSpeed;                    // 최대 낙하 속도

        internal bool canMove;                          // 이동 가능한가
        internal bool isMove;                           // 이동 중인가
        internal bool isMoveLeft;                       // 왼쪽으로 이동 중가
        internal bool isMoveRight;                      // 오른쪽으로 이동 중인가
        internal bool isRun;                            // 달리기 중인가
        internal float moveForce;                       // 이동 속도
        internal float maxMoveSpeed;                    // 최대 이동 속도

        private void CoolTimersUpdate()
        {
            if (horizontal < 0 && isRightTouch)
            {
                isRightTouch = false;
                rightCoolTimer = Cooldown;
            }
            if (horizontal > 0 && isLeftTouch)
            {
                isLeftTouch = false;
                leftCoolTimer = Cooldown;
            }
            if (isUpTouch)
            {
                isUpTouch = false;
                upCoolTimer = Cooldown;
            }
            if (Input.GetButtonDown("Jump") && isDownTouch)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isDownTouch = false;
                downCoolTimer = Cooldown;
            }
        }

        private void JumpUpdate()
        {
            if (Input.GetButtonDown("Jump") && isDownTouch)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        private void MoveUpdate()               // 아직 테스트중
        {
            if (!(isLeftTouch || isRightTouch))
                rb.velocity = new Vector2(horizontal * 10f, rb.velocity.y);
        }
    }
}