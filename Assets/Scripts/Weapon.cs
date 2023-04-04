using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached on every bullet/ammo Fired
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_weaponRigidBody;

    public void Fire(Vector2 a_bulletPos, Vector3 a_bulletRot , float a_fireSpeed)
    {
        this.transform.position = a_bulletPos;
        this.transform.eulerAngles = a_bulletRot;

        m_weaponRigidBody.AddForce(this.transform.up * a_fireSpeed, ForceMode2D.Impulse);

        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if collided object have damagable interface, it will called here after bullet gets collided with it
        IDamagable l_damagableObj = collision.gameObject.GetComponent<IDamagable>();
        if (l_damagableObj != null) l_damagableObj.TakeDamage();
        
        Destroy(gameObject);
    }

    /// <summary>
    /// This triggered will be called when the bullet triggers with the boundries of screen
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    //private void DisableBullet()
    //{
    //    this.gameObject.SetActive(false);
    //    WeaponManager.s_instance.DisableBullet(this);
    //}
}
