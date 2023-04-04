using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamagable
{
    [SerializeField] Rigidbody2D m_rigidbody;
    [SerializeField] SpriteRenderer m_asteroidImage;

    AsteroidInfo m_asteroidInfo = new AsteroidInfo();

    public void SetAsteroidData(Sprite a_asteroidImg, float a_asteroidSpeed, int a_asteroidSplitCount, float a_asteroidDamagePower, float a_mass=0)
    {
        m_asteroidInfo.m_asteroidSprite = a_asteroidImg;
        m_asteroidInfo.m_speed = a_asteroidSpeed;
        m_asteroidInfo.m_splitCount = a_asteroidSplitCount;
        m_asteroidInfo.m_damagePower = a_asteroidDamagePower;

        //if (a_mass > 0) m_rigidbody.mass = a_mass;

        m_asteroidImage.sprite = a_asteroidImg;
    }

    public void AddForce(Vector2 a_dir)
    {
        m_rigidbody.AddForce(a_dir * m_asteroidInfo.m_speed * GameManager.s_instance.AsteroidSpeedMultiplier);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Asteroid")) return; //donr need to damage other asteroid on collision

        IDamagable l_damagableObj = collision.gameObject.GetComponent<IDamagable>();
        if (l_damagableObj != null) l_damagableObj.TakeDamage(m_asteroidInfo.m_damagePower * GameManager.s_instance.AsteroidDamageMultiplier);
    }

    public void TakeDamage(float a_damageAmount = 0)
    {
        if (m_asteroidInfo.m_splitCount > 0) AsteroidManager.s_instance.SplitAsteroid(m_asteroidInfo, this.transform, m_rigidbody.mass);
        else GameManager.OnAsteroidDestroyed.Invoke();

        Destroy(this.gameObject);
    }
}
