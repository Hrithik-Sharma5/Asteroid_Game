using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : PowerPickup<ShieldInfo>
{
    private ShieldInfo m_shieldInfo;

    public override void SetPowerPickupData(ShieldInfo a_shieldInfo)
    {
        //setting the shield property
        m_shieldInfo = a_shieldInfo;
    }

    public override void OnObjectCollect()
    {
        GameManager.s_instance.SetShieldAroundPlayer(m_shieldInfo);
        base.OnObjectCollect();
    }
}
