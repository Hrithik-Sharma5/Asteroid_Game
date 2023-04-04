using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 2)]
public class WeaponData : ScriptableObject
{
    [SerializeField]List<WeaponInfo> m_weaponsList;

    public List<WeaponInfo> WeaponList { get { return m_weaponsList; } }
}

[System.Serializable]
public class WeaponInfo
{
    public Sprite m_weaponIcon;
    public WeaponType m_weaponType;
    public float m_weaponTime; //weapon lifespan
    public Weapon m_weaponPrefab;
    public float m_fireSpeed;
    public bool m_isBurstFireWeapon; //if the weapon fires bullet in burse mode
    public int m_burstCount=3;
    public float m_trajectoryAngle; //the angle between first and last bullet it is a burst weapon
}
