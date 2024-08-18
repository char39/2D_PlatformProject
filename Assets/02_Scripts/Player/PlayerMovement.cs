using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float rayLength = 1.05f;
    public LayerMask groundLayer;
    private float jumpForce = 10f;
    private float jumpCooldown = 0.05f; // 쿨다운 시간
    private bool isGrounded;
    private Rigidbody2D rb;
    private float jumpCooldownTimer;
    public RaycastHit2D hit;

    public RaycastHit2D hitbox;
    private float boxCastMaxDistance = 2.0f;
    private Vector2 boxCastSize = new Vector2(1.7f, 0.05f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()           // 아직 테스트중
    {
        GroundBoxCast();
        Jump();
        GroundPosLock();
    }

    private void GroundBoxCast()
    {
        if (jumpCooldownTimer > 0)                  // 쿨다운 타이머 업데이트
            jumpCooldownTimer -= Time.deltaTime;

        if (jumpCooldownTimer <= 0)                 // 쿨다운 시간이 0일 때만 바닥 감지
        {
            hitbox = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, groundLayer);

            if (hitbox.collider != null)
            {
                if (hitbox.distance <= 1.05f)
                    isGrounded = true;
                else
                    isGrounded = false;
            }
            else
                isGrounded = false;
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)      // 점프 처리
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;                                 // 점프 시 바닥 고정 해제
            jumpCooldownTimer = jumpCooldown;                   // 쿨다운 타이머 시작
        }
    }
    private void GroundPosLock()
    {
        if (isGrounded)                             // 바닥에 있을 때만 위치 고정
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            transform.position = new Vector2(transform.position.x, hitbox.point.y + 1.039f);
        }
        else
            rb.gravityScale = 2;
    }

    void OnDrawGizmos()
    {
        if (hitbox.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector2.down * hitbox.distance);
            Gizmos.DrawWireCube(transform.position + Vector3.down * hitbox.distance, boxCastSize);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Vector2.down * boxCastMaxDistance);
            Gizmos.DrawWireCube(transform.position + Vector3.down * boxCastMaxDistance, boxCastSize);
        }
    }
}