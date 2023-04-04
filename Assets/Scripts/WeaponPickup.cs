using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PowerPickup<WeaponInfo>
{
    private WeaponInfo m_weaponInfo;

    public override void SetPowerPickupData(WeaponInfo a_weaponInfo)
    {
        m_weaponInfo = a_weaponInfo;
    }

    public override void OnObjectCollect()
    {
        WeaponManager.s_instance.SwitchToSpecialWeapon(m_weaponInfo);
        base.OnObjectCollect();
    }

}
