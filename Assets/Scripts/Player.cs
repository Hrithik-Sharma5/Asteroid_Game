using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] float m_moveSpeed;
    [SerializeField] float m_turnSpeed;
    [SerializeField] int m_totalHealth;

    Rigidbody2D m_playerRB;
    private float m_playerCurrentHealth;

    public int TotalHealth { get { return m_totalHealth; } }

    private void Start()
    {
        m_playerCurrentHealth = m_totalHealth;
        m_playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ShootWeapon();

        CheckPlayerOutOfScreen();
    }

    private void FixedUpdate()
    {
        //Movement
        if (Input.GetKey(KeyCode.UpArrow)) MovePlayer(1);
        else if (Input.GetKey(KeyCode.DownArrow)) MovePlayer(-1);

        //Rotation
        if (Input.GetKey(KeyCode.LeftArrow)) RotatePlayer(1);
        else if (Input.GetKey(KeyCode.RightArrow)) RotatePlayer(-1);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable l_iCollectable= collision.transform.GetComponent<ICollectable>();
        if (l_iCollectable != null) l_iCollectable.OnObjectCollect();
    }

    private void MovePlayer(int a_movDir)
    {
        m_playerRB.AddForce(this.transform.up * m_moveSpeed * a_movDir);
    }

    private void RotatePlayer(int a_rotDir)
    {
        m_playerRB.AddTorque(m_turnSpeed * a_rotDir);
    }

    private void ShootWeapon()
    {
        WeaponManager.s_instance.ShootWeapon(this.transform.position, this.transform.eulerAngles, this.transform.up);
    }

    /// <summary>
    /// If player out of screen bring him back from opposite direction
    /// </summary>
    private void CheckPlayerOutOfScreen()
    {
        Vector3 l_playerpos = transform.position;

        //Horizontal check(x)
        if (l_playerpos.x < -9.23f) l_playerpos.x = 9.23f;
        else if (l_playerpos.x > 9.23f) l_playerpos.x = -9.23f;

        //Vertical check(y)
        if (l_playerpos.y < -5.33f) l_playerpos.y = 5.33f;
        else if (l_playerpos.y > 5.33f) l_playerpos.y = -5.33f;

        transform.position = l_playerpos;
    }

    public void TakeDamage(float a_damageAmount = 0)
    {
        m_playerCurrentHealth -= a_damageAmount;
        GameManager.OnPlayerGetDamage(m_playerCurrentHealth);
        if (m_playerCurrentHealth <= 0) GameManager.OnGameOver.Invoke();
    }
}
