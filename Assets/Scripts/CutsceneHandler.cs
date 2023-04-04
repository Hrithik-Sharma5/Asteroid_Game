using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneHandler : MonoBehaviour
{
    [SerializeField] Transform m_player;
    [SerializeField] Asteroid m_demoAsteroid;
    [SerializeField] float m_moveSpeed;

    [SerializeField] float m_timeToReachPlayerToCenter;
    [SerializeField] float m_timeToResetCameraToCenter;
    [SerializeField] float m_timeToReachAsteroidToScreen;

    [SerializeField] UIManager m_uiManager;
    [SerializeField] AsteroidManager m_asteroidManager;

    float m_timeElapsed = 0;

    bool m_canStartGame;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = 2;

        StartCoroutine(MovePlayerToCenter());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_canStartGame)
        {
            if (Input.GetKeyDown(KeyCode.Space)) StartGame();
        }
    }

    IEnumerator MovePlayerToCenter()
    {
        Vector3 m_startPosValue = m_player.position;

        while (m_timeElapsed < m_timeToReachPlayerToCenter)
        {
            m_player.position = Vector3.Lerp(m_startPosValue, Vector3.zero, m_timeElapsed / m_timeToReachPlayerToCenter);
            Camera.main.transform.position = new Vector3(m_player.position.x, 0, -10);
            m_timeElapsed += Time.deltaTime;
            yield return null;
        }
        m_timeElapsed = 0;
        StartCoroutine(MoveCameraToCenter());
    }

    IEnumerator MoveCameraToCenter()
    {
        float m_startPosValue = Camera.main.orthographicSize;
        while (m_timeElapsed < m_timeToResetCameraToCenter)
        {
            Camera.main.orthographicSize = Mathf.Lerp(m_startPosValue, 5, m_timeElapsed / m_timeToReachPlayerToCenter);
            m_timeElapsed += Time.deltaTime;
            yield return null;
        }
        m_timeElapsed = 0;
        StartCoroutine(MoveAsteroidToScreen());
    }

    IEnumerator MoveAsteroidToScreen()
    {
        Vector3 m_startPosValue = m_demoAsteroid.transform.position;
        while (m_timeElapsed < m_timeToReachAsteroidToScreen)
        {
            m_demoAsteroid.transform.position = Vector3.Lerp(m_startPosValue, new Vector3(5, 0, 0), m_timeElapsed / m_timeToReachPlayerToCenter);
            m_timeElapsed += Time.deltaTime;
            yield return null;
        }
        m_timeElapsed = 0;
        m_canStartGame = true;
        StartCoroutine(HoverAsteroid());
        m_uiManager.EnableStartText(true);
    }

    IEnumerator HoverAsteroid()
    {
        Vector2 l_hoverDestionation =new Vector2(m_demoAsteroid.transform.position.x, 0.4f);
        bool l_goingDown = false;
        while (m_canStartGame)
        {
            if (Vector2.Distance(m_demoAsteroid.transform.position, l_hoverDestionation) > 0.1f)
            {
                m_demoAsteroid.transform.position = Vector3.MoveTowards(m_demoAsteroid.transform.position, l_hoverDestionation, Time.deltaTime*0.2f);
            }

            else
            {
                m_timeElapsed = 0;

                l_hoverDestionation = l_goingDown ? new Vector2(m_demoAsteroid.transform.position.x, 0.4f) : new Vector2(m_demoAsteroid.transform.position.x, -0.4f);

                l_goingDown = !l_goingDown;
            }
            yield return null;

        }
    }

    void StartGame()
    {
        m_canStartGame = false;
        m_uiManager.EnableStartText(false);
        PushAstroidTowardsPlayer();

        GameManager.OnGameStart.Invoke();
    }

    void PushAstroidTowardsPlayer()
    {
        Sprite l_demoAsteroidSprite = m_demoAsteroid.transform.GetComponent<SpriteRenderer>().sprite;
        m_demoAsteroid.transform.GetComponent<BoxCollider2D>().enabled= true;   //this line will only be called once so getting the reference directly through getcomponent
        
        Vector2 l_playerDir = (m_player.position - m_demoAsteroid.transform.position).normalized;

        AsteroidInfo l_asteroidInfo = AsteroidManager.s_instance.GetRandomAsteroidInfo();
        m_demoAsteroid.SetAsteroidData(l_demoAsteroidSprite, 20, l_asteroidInfo.m_splitCount, l_asteroidInfo.m_damagePower);
        m_demoAsteroid.AddForce(l_playerDir);
    }
}
