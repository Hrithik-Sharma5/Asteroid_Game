using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidsData", menuName = "ScriptableObjects/AsteroidsData", order = 1)]
public class AsteroidsData : ScriptableObject
{
    [SerializeField]List<AsteroidInfo> m_asteroidInfoList;

    public List<AsteroidInfo> AsteroidList { get { return m_asteroidInfoList; } }
}

[System.Serializable]
public class AsteroidInfo
{
    public Sprite m_asteroidSprite;
    public float m_speed;
    public int m_splitCount;
    public float m_damagePower;
}
