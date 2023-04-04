using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider m_healthSlider;
    [SerializeField] Text m_startText;
    [SerializeField] Text m_scoreText;
    [SerializeField] GameObject m_restartGameButton;

    private int m_score;

    private void Start()
    {
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnGameOver += OnGameOver;
        GameManager.OnPlayerGetDamage += OnplayerGetsDamage;
        GameManager.OnAsteroidDestroyed += OnAsteroidDestroyed;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= OnGameStart;
        GameManager.OnGameOver -= OnGameOver;
        GameManager.OnPlayerGetDamage -= OnplayerGetsDamage;
        GameManager.OnAsteroidDestroyed -= OnAsteroidDestroyed;
    }

    public void EnableStartText(bool a_startTextVisibility)
    {
        m_startText.enabled = a_startTextVisibility;
    }

    private void OnGameStart()
    {
        m_startText.enabled = false;
    }

    private void OnGameOver()
    {
        m_scoreText.text = "Asteroid Destroyed: " + m_score;
        m_scoreText.enabled = true;
        m_restartGameButton.SetActive(true);
    }

    private void OnAsteroidDestroyed()
    {
        m_score++;
    }

    private void OnplayerGetsDamage(float a_playerHealth)
    {
        int l_playerTotalHealth = GameManager.s_instance.m_player.TotalHealth;
        m_healthSlider.value = a_playerHealth / l_playerTotalHealth;
    }
}
