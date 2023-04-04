using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;

    public Player m_player;

    [SerializeField] int m_difficultyIncreaseDuration;
    [SerializeField] float m_asteroidSpeedMultiplier = 1;
    [SerializeField] float m_asteroidDamageMultiplier = 1;

    public static Action<float> OnPlayerGetDamage;
    public static Action OnGameStart;
    public static Action OnGameOver;
    public static Action OnAsteroidDestroyed;

    private GameObject m_activeShield;

    public float AsteroidSpeedMultiplier { get; set; } = 1;
    public float AsteroidDamageMultiplier { get; set; } = 1;

    private void Awake()
    {
        s_instance = this;
    }

    private void Start()
    {
        OnGameStart += GameStart;
        OnGameOver += GameOver;
    }

    private void OnDestroy()
    {
        OnGameStart -= GameStart;
        OnGameOver -= GameOver;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameStart()
    {
        m_player.enabled = true;
        InvokeRepeating(nameof(IncreaseDifficulty), m_difficultyIncreaseDuration, m_difficultyIncreaseDuration);
    }

    private void GameOver()
    {
        m_player.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Increase the damage count and speed over the time to increase difficulty
    /// </summary>
    public void IncreaseDifficulty()
    {
        AsteroidSpeedMultiplier *= m_asteroidSpeedMultiplier;
        AsteroidDamageMultiplier *= m_asteroidDamageMultiplier;
    }

    public void SetShieldAroundPlayer(ShieldInfo a_shieldInfo)
    {
        //If player already haave shield then destroy that shield and give him new shield
        if (m_activeShield) Destroy(m_activeShield);

        Shield l_shield = Instantiate(a_shieldInfo.m_shieldPrefab, m_player.transform);
        m_activeShield = l_shield.gameObject;
        l_shield.SetData(a_shieldInfo.m_hitCount);
    }

}
