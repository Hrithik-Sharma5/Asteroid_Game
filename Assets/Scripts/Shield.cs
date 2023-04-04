using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int m_hitCount;

    public void SetData(int a_hitCount)
    {
        m_hitCount = a_hitCount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_hitCount--;

        IDamagable l_damageable = collision.gameObject.GetComponent<IDamagable>();

        if (l_damageable != null) l_damageable.TakeDamage();

        if (m_hitCount <= 0) Destroy(gameObject); //destroy the shield if it has reached its power limit
    }
}
