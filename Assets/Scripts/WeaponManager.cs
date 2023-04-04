using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    PistolBasic,
    Blaster
}

public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponInfo m_defaultWeaponInfo;

    private Transform m_specialWeapon;
    private List<Weapon> m_activeBulletsList = new List<Weapon>();
    private List<Weapon> m_inactiveBulletsList = new List<Weapon>();

    private bool m_isBurstFireWeapon;

    public static WeaponManager s_instance;

    private WeaponInfo m_currentWeapon;

    private void Start()
    {
        s_instance = this;
        m_currentWeapon = m_defaultWeaponInfo;
        //CreateDefaultBulletPool();
    }

    public void SwitchToSpecialWeapon(WeaponInfo a_weaponInfo)
    {
        m_currentWeapon = a_weaponInfo;

        CancelInvoke(nameof(SwitchToBaseWeapon)); //if the invoke is already running and the player picked up a new weapon previous invoke will get cancelled
        Invoke(nameof(SwitchToBaseWeapon), a_weaponInfo.m_weaponTime);
    }

    public void SwitchToBaseWeapon()
    {
        m_currentWeapon = m_defaultWeaponInfo;
    }

    public void ShootWeapon(Vector2 a_bulletPosition, Vector3 a_bulletRotation, Vector2 a_shootDir)
    {
        if (m_currentWeapon.m_isBurstFireWeapon) FireBurst(a_bulletPosition, a_bulletRotation, a_shootDir);
        else FireBullet(a_bulletPosition, a_bulletRotation, a_shootDir);
    }

    /// <summary>
    /// To fire single bullet each time
    /// </summary>
    /// <param name="a_bulletPosition"></param>
    /// <param name="a_bulletRotation"></param>
    /// <param name="a_shootDir"></param>
    public void FireBullet(Vector2 a_bulletPosition, Vector3 a_bulletRotation, Vector2 a_shootDir)
    {
        //Not using object pooling for bullets because it wont impact any kind of performance for now
        Weapon l_bullet = Instantiate(m_currentWeapon.m_weaponPrefab);
        l_bullet.Fire(a_bulletPosition, a_bulletRotation, m_currentWeapon.m_fireSpeed);
    }

    /// <summary>
    /// To fire burst
    /// </summary>
    /// <param name="a_bulletPosition"></param>
    /// <param name="a_bulletRotation"></param>
    /// <param name="a_shootDir"></param>
    private void FireBurst(Vector2 a_bulletPosition, Vector3 a_bulletRotation, Vector2 a_shootDir)
    {
        float l_angleBetweenEachBullet = m_currentWeapon.m_trajectoryAngle / (m_currentWeapon.m_burstCount - 1);
        float l_angleForbullet = m_currentWeapon.m_trajectoryAngle / 2;


        for (int i = 0; i < m_currentWeapon.m_burstCount; i++)
        {
            //Not using object pooling for bullets because it wont impact any kind of performance for now
            Weapon l_bullet = Instantiate(m_currentWeapon.m_weaponPrefab);

            Vector3 l_bulletRotation = new Vector3(a_bulletRotation.x, a_bulletRotation.y, a_bulletRotation.z + l_angleForbullet);

            l_bullet.Fire(a_bulletPosition, l_bulletRotation, m_currentWeapon.m_fireSpeed);
            l_angleForbullet -= l_angleBetweenEachBullet;
        }
    }


    //public void DisableBullet(Weapon a_disabledBullet)
    //{
    //    m_activeBulletsList.Remove(a_disabledBullet);
    //    m_inactiveBulletsList.Add(a_disabledBullet);
    //}

    //private void CreateDefaultBulletPool()
    //{
    //    for (int i = 0; i < 40; i++)
    //    {
    //        Weapon l_bullet = Instantiate(m_defaultBulletPrefab, Vector3.zero, Quaternion.identity);
    //        m_inactiveBulletsList.Add(l_bullet);
    //    }
    //}

    //private Weapon GetBullet()
    //{
    //    Weapon l_refBullet = null;
    //    if (m_inactiveBulletsList.Count > 0)
    //    {
    //        l_refBullet = m_inactiveBulletsList[0];
    //        m_inactiveBulletsList.RemoveAt(0);
    //        m_activeBulletsList.Add(l_refBullet);
    //    }
    //    else
    //    {
    //        l_refBullet = Instantiate(m_defaultBulletPrefab, Vector3.zero, Quaternion.identity);
    //        m_activeBulletsList.Add(l_refBullet);
    //    }
    //    return l_refBullet;
    //}
}
