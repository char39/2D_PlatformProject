using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public partial class Player
    {
        private float jumpForce;

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