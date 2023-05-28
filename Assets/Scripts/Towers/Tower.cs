using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a base class of all towers.
/// </summary>
public class Tower : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletPrefab; // bullet prefab


    #region Properties

    protected int level; // current level of tower
    public int Level
    {
        get { return level; }
    }

    protected int cost; // cost of tower
    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    protected int damage; // damage of tower
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    protected float range; // shooting range of tower
    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    protected float fireRate; // fire rate of tower
    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }

    protected float coolDownTime; // time to allow choose another tower
    public float CoolDownTime
    {
        get { return coolDownTime; }
        set { coolDownTime = value; }
    }


    #endregion

    #region Methods

    virtual protected void Start()
    {
       
    }
    // function sum two number


    /// <summary>
    /// Run when tower is spawned.
    /// </summary>
    public void OnSpawn()
    {

    }

    /// <summary>
    /// Run when TowerManager update, return false if tower is destroyed and true if tower is still alive.
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public bool OnUpdate(float deltaTime)
    {
        return true;
    }

    /// <summary>
    /// Run when tower is destroyed.
    /// </summary>
    public void Despawn()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Run when tower is upgraded.
    /// </summary>
    public void OnLevelUp()
    {
        level++;
        damage += 10;
        range += 0.5f;
        fireRate += 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Define direction
            Vector2 direction = collision.transform.position - transform.position;

            // Instantiate bullet follow direction
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            // Add force to bullet
            bullet.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * 1000);
        }
    }

    #endregion
}
