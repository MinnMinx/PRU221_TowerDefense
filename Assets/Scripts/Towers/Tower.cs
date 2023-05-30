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

    // list of enemies in range
    private List<GameObject> targetInRange;
    
    // support shooting
    private float nextFireTime;



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

    /// <summary>
    /// Run when enemy enter tower range.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Add enemy to list
            targetInRange.Add(collision.gameObject);
        }
    }

    private void FireAt(Vector2 targetPosition)
    {
        // Define direction
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Instantiate bullet follow direction
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Add force to bullet
        bullet.GetComponent<Rigidbody2D>().AddForce((targetPosition - (Vector2)transform.position).normalized * 1000);
    }

    /// <summary>
    /// Run when enemy exit tower range.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Remove enemy from list
            targetInRange.Remove(collision.gameObject);
        }
    }

    /// <summary>
    ///  
    /// </summary>
    virtual protected void Start()
    {
        // Initialize list of enemies in range
        targetInRange = new List<GameObject>();

        // Initialize next fire time
        nextFireTime = 0;

        // get components collider of game object
        CircleCollider2D circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        
        // set radius of collider
        circleCollider2D.radius = range;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (targetInRange.Count > 0 && Time.time >= nextFireTime)
        {
            GameObject target = targetInRange[0];
            if (target != null)
            {
                FireAt(target.transform.position);
            }
            nextFireTime = Time.time + fireRate;
        }
    }



    #endregion
}
