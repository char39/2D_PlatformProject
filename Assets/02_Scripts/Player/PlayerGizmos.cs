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
    public void DrawHorizontalHitBoxGizmos(Transform tr, RaycastHit2D hitbox, Vector2 hitboxOffset, Vector2 boxSize, float col_x, float boxLength, float maxBoxDistance, bool right)
    {
        Vector2 direction = right ? Vector2.right : Vector2.left;

        if (hitbox.collider != null)
        {
            if (hitbox.distance <= col_x * 0.5f + boxLength * 1.5f)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.yellow;
            Gizmos.DrawRay(tr.position + (Vector3)hitboxOffset, direction * hitbox.distance);
            Gizmos.DrawWireCube(tr.position + (Vector3)hitboxOffset + (Vector3)direction * hitbox.distance, boxSize);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(tr.position + (Vector3)hitboxOffset, direction * maxBoxDistance);
            Gizmos.DrawWireCube(tr.position + (Vector3)hitboxOffset + (Vector3)direction * maxBoxDistance, boxSize);
        }
    }
    /// <summary> 인스턴스의 상하 충돌 박스를 표현. </summary>
    public void DrawVerticalHitBoxGizmos(Transform tr, RaycastHit2D hitbox, Vector2 hitboxOffset, Vector2 boxSize, float col_y, float boxLength, float maxBoxDistance, bool up)
    {
        Vector2 direction = up ? Vector2.up : Vector2.down;

        if (hitbox.collider != null)
        {
            if (hitbox.distance <= col_y * 0.5f + boxLength * 1.5f)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.yellow;
            Gizmos.DrawRay(tr.position + (Vector3)hitboxOffset, direction * hitbox.distance);
            Gizmos.DrawWireCube(tr.position + (Vector3)hitboxOffset + (Vector3)direction * hitbox.distance, boxSize);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(tr.position + (Vector3)hitboxOffset, direction * maxBoxDistance);
            Gizmos.DrawWireCube(tr.position + (Vector3)hitboxOffset + (Vector3)direction * maxBoxDistance, boxSize);
        }
    }


}
