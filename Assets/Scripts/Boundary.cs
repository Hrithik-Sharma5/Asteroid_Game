using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] Transform m_teleportPoint;
    [SerializeField] bool m_teleportX;
    [SerializeField] bool m_teleportY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 l_colliderObjPos = collision.transform.position;
        if (m_teleportX) l_colliderObjPos.x = m_teleportPoint.position.x;
        if (m_teleportY) l_colliderObjPos.y = m_teleportPoint.position.y;

        collision.transform.position = l_colliderObjPos;
    }
}
