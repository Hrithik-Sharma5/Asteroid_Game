using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] WeaponPickup m_weaponPickPrefab;   //Prefab that will spawn all the weapons
    [SerializeField] ShieldPickup m_shieldPickPrefab;   //Prefab that will spawn all the shields

    [SerializeField] ShieldData m_shieldData;   //Scriptable Object that contains data of all the shields
    [SerializeField] WeaponData m_weaponData;   //Scriptable Object that contains data of all the weapons

    [SerializeField] int m_maxPowerupAtTime=1;

    [SerializeField] float m_spawnDuration;
    [Range(1,10)]
    [SerializeField] int m_weaponShieldRatio=5;

    public static int ActivePowerupsCount { get; set; }

    private void Start()
    {
        GameManager.OnGameStart += OnGameStart;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= OnGameStart;
    }

    private void OnGameStart()
    {
        ActivePowerupsCount = 0;
        InvokeRepeating(nameof(SpawnRandomPowerUp), 7, m_spawnDuration);
    }

    void SpawnRandomPowerUp()
    {
        if (ActivePowerupsCount >= m_maxPowerupAtTime) return;

        int l_refNumber = Random.Range(1, 11);
        if (l_refNumber > m_weaponShieldRatio) SpawnRandomShield();
        else SpawnRandomWeapon();

        ActivePowerupsCount++;
    }


    /// <summary>
    /// To spawn a random weapon pickup from WeaponData Scriptable Object
    /// </summary>
    void SpawnRandomWeapon()
    {
        int l_weaponIndex = Random.Range(0, m_weaponData.WeaponList.Count);
        WeaponInfo l_weaponInfo = m_weaponData.WeaponList[l_weaponIndex];

        PowerPickup<WeaponInfo> l_weaponPickup =  Instantiate(m_weaponPickPrefab, GetRandomPointOnScreen(), Quaternion.identity);
        l_weaponPickup.PickupSprite.sprite = l_weaponInfo.m_weaponIcon;
        l_weaponPickup.SetPowerPickupData(l_weaponInfo);
    }

    /// <summary>
    /// To spawn a random shield pickup from ShieldData Scriptable Object
    /// </summary>
    void SpawnRandomShield()
    {
        int l_shieldIndex = Random.Range(0, m_shieldData.ShieldList.Count);
        ShieldInfo l_shieldInfo = m_shieldData.ShieldList[l_shieldIndex];

        PowerPickup<ShieldInfo> l_shieldPickup = Instantiate(m_shieldPickPrefab, GetRandomPointOnScreen(), Quaternion.identity);
        l_shieldPickup.PickupSprite.sprite = l_shieldInfo.m_shieldSprite;
        l_shieldPickup.SetPowerPickupData(l_shieldInfo);
    }

    /// <summary>
    /// Random point on screen to spawn powerups
    /// </summary>
    /// <returns></returns>
    Vector2 GetRandomPointOnScreen()
    {
        //Random range is between 0.05 and 0.95 to make sure the objects dont spawn at extreme right or extreme left part of screen
        Vector2 l_randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.05f, 0.95f), Random.Range(0.05f, 0.95f)));
        return l_randomPositionOnScreen;
    }
}
