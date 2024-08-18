using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        private float jumpForce;
        private float Cooldown;

        private float downCoolTimer;
        private float leftCoolTimer;
        private float rightCoolTimer;
        private float upCoolTimer;

        private bool isDownTouch;
        private bool isLeftTouch;
        private bool isRightTouch;
        private bool isUpTouch;

        private float col_x;
        private float col_y;

        private float boxLength;

        private Vector2 boxVerticalSize;
        private Vector2 boxHorizontalSize;

        private float boxMaxDist;

        public RaycastHit2D hitboxDown;
        public RaycastHit2D hitboxLeft;
        public RaycastHit2D hitboxRight;
        public RaycastHit2D hitboxUp;



        private void BoxDownCast()
        {
            if (downCoolTimer > 0)                  // 쿨다운 타이머 업데이트
                downCoolTimer -= Time.deltaTime;
            if (downCoolTimer <= 0)                 // 쿨다운 시간이 0일 때만 바닥 감지
            {
                hitboxDown = Physics2D.BoxCast(transform.position, boxVerticalSize, 0f, Vector2.down, boxMaxDist, layer.Block);
                if (hitboxDown.collider != null)
                {
                    if (hitboxDown.distance <= col_y * 0.5f + boxLength * 1.5f)
                        isDownTouch = true;
                    else
                        isDownTouch = false;
                }
                else
                    isDownTouch = false;
            }
        }
        private void DownPosLock()
        {
            if (isDownTouch)                             // 바닥에 있을 때만 위치 고정
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                transform.position = new Vector2(transform.position.x, hitboxDown.point.y + (col_y * 0.5f + boxLength * 2f));
            }
            else
                rb.gravityScale = 2;
        }
        private void BoxLeftCast()
        {
            if (leftCoolTimer > 0)
                leftCoolTimer -= Time.deltaTime;
            if (leftCoolTimer <= 0)
            {
                hitboxLeft = Physics2D.BoxCast(transform.position, boxHorizontalSize, 0f, Vector2.left, boxMaxDist, layer.Block);
                if (hitboxLeft.collider != null)
                {
                    if (hitboxLeft.distance <= col_x * 0.5f + boxLength * 1.5f)
                        isLeftTouch = true;
                    else
                        isLeftTouch = false;
                }
                else
                    isLeftTouch = false;
            }
        }
        private void LeftPosLock()
        {
            if (isLeftTouch)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                transform.position = new Vector2(hitboxLeft.point.x + (col_x * 0.5f + boxLength * 2f), transform.position.y);
            }
        }
        private void BoxRightCast()
        {
            if (rightCoolTimer > 0)
                rightCoolTimer -= Time.deltaTime;
            if (rightCoolTimer <= 0)
            {
                hitboxRight = Physics2D.BoxCast(transform.position, boxHorizontalSize, 0f, Vector2.right, boxMaxDist, layer.Block);
                if (hitboxRight.collider != null)
                {
                    if (hitboxRight.distance <= col_x * 0.5f + boxLength * 1.5f)
                        isRightTouch = true;
                    else
                        isRightTouch = false;
                }
                else
                    isRightTouch = false;
            }
        }
        private void RightPosLock()
        {
            if (isRightTouch)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                transform.position = new Vector2(hitboxRight.point.x - (col_x * 0.5f + boxLength * 2f), transform.position.y);
            }
        }
        private void BoxUpCast()
        {
            if (upCoolTimer > 0)
                upCoolTimer -= Time.deltaTime;
            if (upCoolTimer <= 0)
            {
                hitboxUp = Physics2D.BoxCast(transform.position, boxVerticalSize, 0f, Vector2.up, boxMaxDist, layer.Block);
                if (hitboxUp.collider != null)
                {
                    if (hitboxUp.distance <= col_y * 0.5f + boxLength * 1.5f)
                        isUpTouch = true;
                    else
                        isUpTouch = false;
                }
                else
                    isUpTouch = false;
            }
        }
        private void UpPosLock()
        {
            if (isUpTouch)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                transform.position = new Vector2(transform.position.x, hitboxUp.point.y - (col_y * 0.5f + boxLength * 2f));
            }
        }


        private void JumpUpdate()
        {
            if (Input.GetButtonDown("Jump") && isDownTouch)      // 점프 처리
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isDownTouch = false;                                 // 점프 시 바닥 고정 해제
                downCoolTimer = Cooldown;                   // 쿨다운 타이머 시작
            }
        }
        private void CoolTimersUpdate()
        {
            if (Input.GetKeyDown(KeyCode.A) && isRightTouch)
            {
                isRightTouch = false;
                rightCoolTimer = Cooldown;
            }
            if (Input.GetKeyDown(KeyCode.D) && isLeftTouch)
            {
                isLeftTouch = false;
                leftCoolTimer = Cooldown;
            }
            if (isUpTouch)
            {
                isUpTouch = false;
                upCoolTimer = Cooldown;
            }

        }
    }
}