using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager s_instance;

    [SerializeField] AsteroidsData m_asteroidDataSO;
    [SerializeField] int m_astroidSpawnTime;
    [SerializeField] int m_maxAsteroids;
    [SerializeField] Sprite[] m_asteroidsSprite;
    [SerializeField] Asteroid m_asteroidPrefab;
    [SerializeField] Transform[] m_asteroidsSpawnPoints;

    int m_activeAsteroidsCount = 0;

    private void Start()
    {
        s_instance = this;
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnGameOver += OnGameOver;
        GameManager.OnAsteroidDestroyed += OnAstroidDestroyedCompletely;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= OnGameStart;
        GameManager.OnAsteroidDestroyed -= OnAstroidDestroyedCompletely;
    }

    private void OnGameStart()
    {
        InvokeRepeating(nameof(SpawnRandomAsteroid), 2, m_astroidSpawnTime);
    }

    private void OnGameOver()
    {
        CancelInvoke(nameof(SpawnRandomAsteroid));
    }

    private void SpawnRandomAsteroid()
    {
        if (m_activeAsteroidsCount > m_maxAsteroids) return;

        Vector2 l_spawnPos = GetRandomSpawnPoint();
        Asteroid l_asteroid = Instantiate(m_asteroidPrefab, l_spawnPos, Quaternion.identity);
        AsteroidInfo l_asteroidInfo = GetRandomAsteroidInfo(); //Set the info for newly spawned astroid

        if (!l_asteroidInfo.m_asteroidSprite) l_asteroidInfo.m_asteroidSprite = GetRandomAsteroidSprite();

        l_asteroid.SetAsteroidData(l_asteroidInfo.m_asteroidSprite, l_asteroidInfo.m_speed, l_asteroidInfo.m_splitCount, l_asteroidInfo.m_damagePower);

        l_asteroid.AddForce(Random.insideUnitCircle*5);
        m_activeAsteroidsCount++;
    }

    /// <summary>
    /// Split the asteroids into 2 parts
    /// </summary>
    /// <param name="a_asteroidInfo"></param>
    /// <param name="a_asteroidTransform"></param>
    /// <param name="a_astroidMass"></param>
    public void SplitAsteroid(AsteroidInfo a_asteroidInfo, Transform a_asteroidTransform , float a_astroidMass)
    {
        //Spawning 2 asteroids at current asteroid's position and reducing the size
        for (int i = 0; i < 2; i++)
        {
            Vector2 l_pos = a_asteroidTransform.position;
            l_pos += Random.insideUnitCircle * 0.5f;
            Asteroid l_asteroid = Instantiate(m_asteroidPrefab, l_pos , Quaternion.identity);
            l_asteroid.transform.localScale = a_asteroidTransform.localScale/1.5f;

            Sprite l_asteroidSprite = GetRandomAsteroidSprite();

            //Setting the data of the new splitted asteriods same as the parent asteroid but with a new sprite
            l_asteroid.SetAsteroidData(l_asteroidSprite, a_asteroidInfo.m_speed, a_asteroidInfo.m_splitCount - 1, a_asteroidInfo.m_damagePower, a_astroidMass / 2);
            l_asteroid.AddForce(Random.insideUnitCircle*5);
            m_activeAsteroidsCount++;
        }
    }


    /// <summary>
    /// Choose a random asteroid data from the AsteroidData Scriptable Object
    /// </summary>
    /// <returns></returns>
    public AsteroidInfo GetRandomAsteroidInfo()
    {
        int l_randomAsteroidDataIndex = Random.Range(0, m_asteroidDataSO.AsteroidList.Count);
        return m_asteroidDataSO.AsteroidList[l_randomAsteroidDataIndex];
    }


    /// <summary>
    /// Gets called when the last splitted asteroid gets destroyed
    /// </summary>
    private void OnAstroidDestroyedCompletely()
    {
        m_activeAsteroidsCount--;
    }

    private Sprite GetRandomAsteroidSprite()
    {
        int l_randomSpriteIndex = Random.Range(0, m_asteroidsSprite.Length);
        return m_asteroidsSprite[l_randomSpriteIndex];
    }

    private Vector2 GetRandomSpawnPoint()
    {
        int l_randomTransformIndex = Random.Range(0, m_asteroidsSpawnPoints.Length);
        return m_asteroidsSpawnPoints[l_randomTransformIndex].position;
    }
}
