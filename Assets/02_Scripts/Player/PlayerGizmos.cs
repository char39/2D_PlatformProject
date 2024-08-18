using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    /// <summary> 인스턴스의 실시간 벡터를 표현. </summary>
    public void DrawVectorGizmos(Transform tr, Vector2 velocity, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(tr.position, tr.position + (Vector3)velocity);
    }

    /// <summary> 인스턴스의 좌우 충돌 박스를 표현. </summary>
    public void DrawHorizontalHitBoxGizmos(Transform tr, RaycastHit2D hitbox, float vertical, float boxLength, float maxBoxDistance, bool right)
    {
        Vector2 direction = right ? Vector2.right : Vector2.left;
        Vector2 boxSize = new(boxLength, vertical);

        if (hitbox.collider != null)
        {
            if (hitbox.distance <= vertical * 0.5f + boxLength * 1.5f)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.yellow;
            Gizmos.DrawRay(tr.position, direction * hitbox.distance);
            Gizmos.DrawWireCube(tr.position + (Vector3)direction * hitbox.distance, boxSize);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(tr.position, direction * maxBoxDistance);
            Gizmos.DrawWireCube(tr.position + (Vector3)direction * maxBoxDistance, boxSize);
        }
    }
    /// <summary> 인스턴스의 상하 충돌 박스를 표현. </summary>
    public void DrawVerticalHitBoxGizmos(Transform tr, RaycastHit2D hitbox, float horizontal, float boxLength, float maxBoxDistance, bool up)
    {
        Vector2 direction = up ? Vector2.up : Vector2.down;
        Vector2 boxSize = new(horizontal, boxLength);

        if (hitbox.collider != null)
        {
            if (hitbox.distance <= horizontal * 0.5f + boxLength * 1.5f)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.yellow;
            Gizmos.DrawRay(tr.position, direction * hitbox.distance);
            Gizmos.DrawWireCube(tr.position + (Vector3)direction * hitbox.distance, boxSize);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(tr.position, direction * maxBoxDistance);
            Gizmos.DrawWireCube(tr.position + (Vector3)direction * maxBoxDistance, boxSize);
        }
    }


}
