using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a base class of all towers.
/// </summary>
public class Tower : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected GameObject bulletPrefab; // bullet prefab

    // list of enemies in range
    private List<GameObject> targetInRange;

    // support shooting
    private Timer cooldownTimer;
    private Animator animIdle;
    private bool canShoot = true;

    public GameObject effectLevel2;
    public GameObject effectLevel3;
    #endregion

    #region Properties
    protected int id;
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    protected int level; // current level of tower
    public int Level
    {
        get { return level; }
        set { level = value; }
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

    protected float muzzleSpeed; // muzzle speed of tower
    public float MuzzleSpeed
    {
        get { return muzzleSpeed; }
        set { muzzleSpeed = value; }
    }

    protected float coolDownTime; // cool down time of tower

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
        // check if game object was clicked, then upgrade tower
        if (Input.GetMouseButtonDown(0))
        {
            // get mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // get collider of game object
            CircleCollider2D circleCollider2D = gameObject.GetComponent<CircleCollider2D>();

            // check if mouse position is in range of tower
            if (circleCollider2D.OverlapPoint(mousePosition) && Vector2.Distance(mousePosition, transform.position) < 0.5f)
            {
                // check if tower is not max level
                if (level < 3)
                {
                    // upgrade tower
                    level++;
                    cost += 100;
                    damage += 10;
                    range += 0.5f;
                    muzzleSpeed += 500f;
                    coolDownTime += 10f;
                    // add effect to tower after upgrade
                    if (level == 2)
                    {
                        // get position of spawn effect higher than tower 0.5f
                        Vector3 position = (Vector3)transform.position + new Vector3(0,0.1f,0);
                        
                        // start effect level 2 at position of tower
                        Destroy(Instantiate(effectLevel2.gameObject, position, Quaternion.identity),5);
                    }
                    else if (level == 3)
                    {
                        // get position of tower
                        Vector3 position = (Vector3)transform.position;

                        // instantiate effect at position of tower
                        Destroy(Instantiate(effectLevel3.gameObject, position, Quaternion.identity),5);
                    }
                }
            }
        }
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

    /// <summary>
    /// Fire at enemy.
    /// </summary>
    /// <param name="target"></param>
    private void FireAt(Transform target)
    {
        // Define direction
        // Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Instantiate bullet follow direction
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var bulletBehavior = bullet.GetComponent<Bullet>();
        if (bulletBehavior != null)
        {
            bulletBehavior.SetProperties(damage, muzzleSpeed, target);
        }

        // Bullet tự tính dir nên không cần gán trước
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // rotate the tower to the left if the enemy is on the left if there is an enemy, otherwise keep the tower facing the right
        if (target.position.x < transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        // Bullet tự di chuyển nên ko cần add force
            //// Add force to bullet
            //bullet.GetComponent<Rigidbody2D>().AddForce((targetPosition - (Vector2)transform.position).normalized * muzzleSpeed);
    }

    /// <summary>
    /// Run when enemy exit tower range.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Có thể check luôn nếu List contain gameObject
            // if (collision.gameObject.tag == "Enemy")
        if (targetInRange.Contains(collision.gameObject))
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

        // Initialize cooldown Timer
        cooldownTimer = gameObject.AddComponent<Timer>();
        cooldownTimer.Duration = coolDownTime;
        //cooldownTimer.Duration = 1f;

        // get components animation of game object
        animIdle = gameObject.GetComponent<Animator>();

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
        // Print all of properties of tower
        Debug.Log(gameObject.name+ "Level: " + level + " Cost: " + cost + " Damage: " + damage + " Range: " + range + " MuzzleSpeed: " + muzzleSpeed + " CoolDownTime: " + coolDownTime);
        if (!canShoot && cooldownTimer.Finished)
        {
            canShoot = true;
        }
        
        if (targetInRange.Count > 0 && canShoot)
        {
            // start cooldown timer
            canShoot = false;
            cooldownTimer.Run();

            // get first enemy in range
            GameObject target = targetInRange[0];
            if (target != null)
            {
                // stop animation
                animIdle.Play("Idle", 0, 0);
                FireAt(target.transform);
            }
        }
        else
        {
            // start animation 
            animIdle.enabled = true;
        }

        // check if game object was clicked, then upgrade tower
        OnLevelUp();
    }

    #endregion
}
