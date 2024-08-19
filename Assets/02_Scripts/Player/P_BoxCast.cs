using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        private float Cooldown;                 // 해당 시간동안 BoxCast를 사용하지 않음

        private float downCoolTimer;            // 아래쪽 BoxCast 타이머 갱신
        private float leftCoolTimer;            // 왼쪽 BoxCast 타이머 갱신
        private float rightCoolTimer;           // 오른쪽 BoxCast 타이머 갱신
        private float upCoolTimer;              // 위쪽 BoxCast 타이머 갱신

        public bool isDownTouch;                // 아래쪽 BoxCast 충돌 감지         충돌 감지 변수 나중에 private으로 변경
        public bool isLeftTouch;                // 왼쪽 BoxCast 충돌 감지
        public bool isRightTouch;               // 오른쪽 BoxCast 충돌 감지
        public bool isUpTouch;                  // 위쪽 BoxCast 충돌 감지

        private float col_x;                    // BoxCollider2D의 x축 길이
        private float col_y;                    // BoxCollider2D의 y축 길이

        private float boxLength;                // BoxCast의 길이
        private float boxMaxDist;               // BoxCast를 판별하는 최대 거리

        private Vector2 boxHorizontalSize;      // Vector2(col_x, boxLength). Vertical에서 사용하는 BoxCast
        private Vector2 boxVerticalSize;        // Vector2(boxLength, col_y). Horizontal에서 사용하는 BoxCast

        private RaycastHit2D hitboxDown;        // 아래쪽 BoxCast
        private RaycastHit2D hitboxLeft;        // 왼쪽 BoxCast
        private RaycastHit2D hitboxRight;       // 오른쪽 BoxCast
        private RaycastHit2D hitboxUp;          // 위쪽 BoxCast

        public Vector2 hitboxOffset;           // BoxCast가 충돌 거리 안으로 들어간 거리의 합
        private Vector2 hitboxOffsetDown;       // 아래쪽 BoxCast가 충돌 거리 안으로 들어간 거리
        private Vector2 hitboxOffsetLeft;       // 왼쪽 BoxCast가 충돌 거리 안으로 들어간 거리
        private Vector2 hitboxOffsetRight;      // 오른쪽 BoxCast가 충돌 거리 안으로 들어간 거리
        private Vector2 hitboxOffsetUp;         // 위쪽 BoxCast가 충돌 거리 안으로 들어간 거리

        /// <summary> BoxCast를 사용하여 충돌 감지. <para> true = up, false = down </para> </summary>
        private void BoxVerticalCast(ref RaycastHit2D hitbox, ref float coolTimer, ref bool isTouch, bool up)
        {
            Vector2 direction = up ? Vector2.up : Vector2.down;

            if (up) hitboxOffset = hitboxOffsetDown + hitboxOffsetLeft + hitboxOffsetRight;
            else    hitboxOffset = hitboxOffsetUp + hitboxOffsetLeft + hitboxOffsetRight;

            if (coolTimer > 0)
                coolTimer -= Time.deltaTime;
            if (coolTimer <= 0)
            {
                hitbox = Physics2D.BoxCast(transform.position + (Vector3)hitboxOffset, boxHorizontalSize, 0f, direction, boxMaxDist, layer.Block);
                if (hitbox.collider != null)
                {
                    if (hitbox.distance <= col_y * 0.5f + boxLength * 1.5f)
                    {
                        isTouch = true;
                        if (up)
                            hitboxOffsetUp = new Vector2(0f, -(col_y * 0.5f + boxLength * 1.5f - hitbox.distance));
                        else
                            hitboxOffsetDown = new Vector2(0f, col_y * 0.5f + boxLength * 1.5f - hitbox.distance);
                    }
                    else
                    {
                        isTouch = false;
                        hitboxOffsetUp = new Vector2(0f, 0f);
                        hitboxOffsetDown = new Vector2(0f, 0f);
                    }
                }
                else
                    isTouch = false;
            }
        }
        /// <summary> BoxCast 충돌 감지 후 위치 고정. <para> true = up, false = down </para> </summary>
        private void VerticalPosLock(RaycastHit2D hitbox, ref bool isTouch, bool up)
        {
            if (isTouch && ((up && rb.velocity.y >= 0f) || (!up && rb.velocity.y <= 0f)))
            {
                rb.gravityScale = up ? rb.gravityScale : 0.0f;
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                float y = up ? hitbox.point.y - (col_y * 0.5f + boxLength * 2f) : hitbox.point.y + (col_y * 0.5f + boxLength * 2f);
                transform.position = new Vector2(transform.position.x, y);
            }
            else
                rb.gravityScale = up ? rb.gravityScale : originalGravityScale;
        }
        /// <summary> BoxCast를 사용하여 충돌 감지. <para> true = right, false = left </para> </summary>
        private void BoxHorizontalCast(ref RaycastHit2D hitbox, ref float coolTimer, ref bool isTouch, bool right)
        {
            Vector2 direction = right ? Vector2.right : Vector2.left;

            if (right)  hitboxOffset = hitboxOffsetLeft + hitboxOffsetUp + hitboxOffsetDown;
            else        hitboxOffset = hitboxOffsetRight + hitboxOffsetUp + hitboxOffsetDown;

            if (coolTimer > 0)
                coolTimer -= Time.deltaTime;
            if (coolTimer <= 0)
            {
                hitbox = Physics2D.BoxCast(transform.position + (Vector3)hitboxOffset, boxVerticalSize, 0f, direction, boxMaxDist, layer.Block);
                if (hitbox.collider != null)
                {
                    if (hitbox.distance <= col_x * 0.5f + boxLength * 1.5f)
                    {
                        isTouch = true;
                        if (right)
                            hitboxOffsetRight = new Vector2(-(col_x * 0.5f + boxLength * 1.5f - hitbox.distance), 0f);
                        else
                            hitboxOffsetLeft = new Vector2(col_x * 0.5f + boxLength * 1.5f - hitbox.distance, 0f);
                    }
                    else
                    {
                        isTouch = false;
                        hitboxOffsetRight = new Vector2(0f, 0f);
                        hitboxOffsetLeft = new Vector2(0f, 0f);
                    }
                }
                else
                    isTouch = false;
            }
        }
        /// <summary> BoxCast 충돌 감지 후 위치 고정. <para> true = right, false = left </para> </summary>
        private void HorizontalPosLock(RaycastHit2D hitbox, ref bool isTouch, bool right)
        {
            if (isTouch)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                float x = right ? hitbox.point.x - (col_x * 0.5f + boxLength * 2f) : hitbox.point.x + (col_x * 0.5f + boxLength * 2f);
                transform.position = new Vector2(x, transform.position.y);
            }
        }
    }
}