using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldData", menuName = "ScriptableObjects/ShieldData", order = 1)]
public class ShieldData : ScriptableObject
{
    [SerializeField]List<ShieldInfo> m_shieldInfo;
    public List<ShieldInfo> ShieldList { get { return m_shieldInfo; } }
}

[System.Serializable]
public class ShieldInfo
{
    public Sprite m_shieldSprite;
    public Shield m_shieldPrefab;
    public int m_hitCount;
}
