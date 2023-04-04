using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup<T> : MonoBehaviour, ICollectable
{
    [SerializeField] SpriteRenderer m_pickupSpriteRenderer;

    public SpriteRenderer PickupSprite { get {return m_pickupSpriteRenderer; } }

    public virtual void SetPowerPickupData(T a_powerupInfo) { }

    public virtual void OnObjectCollect()
    {
        PowerupManager.ActivePowerupsCount--;
        Destroy(this.gameObject);
    }
}
