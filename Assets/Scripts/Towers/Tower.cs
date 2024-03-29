﻿using Enemy;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a base class of all towers.
/// </summary>
public class Tower : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected GameObject bulletLevel1; // bullet prefab
    [SerializeField]
    protected GameObject bulletLevel2; // bullet prefab
    [SerializeField]
    protected GameObject bulletLevel3; // bullet prefab
    [SerializeField]
    private CircleCollider2D upgradeCollider;
    [SerializeField]
    protected bool lv3Slow = false;
    [SerializeField]
    protected bool lv2Slow = false;
    

    // list of enemies in range
    private List<GameObject> targetInRange;

    // support shooting
    private Timer cooldownTimer;
    private Animator animIdle;
    private bool canShoot = true;

    public GameObject effectLevel2;
    public GameObject effectLevel3;
    public AudioClip shootLv1;
    public AudioClip shootLv2;
    public AudioClip shootLv3;
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

    private TowerSavedData savedData = null;

    #endregion

    #region Methods

    /// <summary>
    /// Run when tower is spawned.
    /// </summary>
    private void OnSpawn()
    {

    }

    /// <summary>
    /// Run when TowerManager update, return false if tower is destroyed and true if tower is still alive.
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    private bool OnUpdate(float deltaTime)
    {
        return true;
    }

    /// <summary>
    /// Run when tower is destroyed.
    /// </summary>
    private void Despawn()
    {
        Destroy(gameObject);
    }

    public void UpgradeTower()
    {
        // take money from player
        GameManager.instance.TakeMoney(level * 100);
        // upgrade tower
        level++;
        //cost += 100;
        damage += 20;
        range += level > 3 ? 0 : 0.5f;
        GetComponent<CircleCollider2D>().radius = range;
        coolDownTime *= coolDownTime <= 0.8f ? 1f : 0.7f;

        // add effect to tower after upgrade
        if (level == 2)
        {
            // get position of spawn effect higher than tower 0.5f
            Vector3 position = (Vector3)transform.position + new Vector3(-0.04f, 0.4f, 0);

            // start effect level 2 at position of tower
            Destroy(Instantiate(effectLevel2.gameObject, position, Quaternion.identity), 0.68f);
        }
        else if (level >= 3)
        {
            // get position of tower
            Vector3 position = (Vector3)transform.position + new Vector3(-0.01f, 0.6f, 0);

            // instantiate effect at position of tower
            Destroy(Instantiate(effectLevel3.gameObject, position, Quaternion.identity), 0.82f);
        }
    }

    public void AssignOldData(int level, int damage, float range, float cooldownTime)
    {
        savedData = new TowerSavedData
        {
            level = level,
            damage = damage,
            range = range,
            cd = cooldownTime,
        };
    }

    /// <summary>
    /// Run when enemy enter tower range.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy01_Base>() != null)
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
        
        // Instantiate bullet base on each level and follow direction
        GameObject bullet;
        if (level == 2)
        {
            // Play audio
            AudioSource.PlayClipAtPoint(shootLv2, transform.position);
            bullet = Instantiate(bulletLevel2, transform.position, Quaternion.identity);
        }
        else if (level >= 3)
        {
            // Play audio
            AudioSource.PlayClipAtPoint(shootLv3, transform.position);
            bullet = Instantiate(bulletLevel3, transform.position, Quaternion.identity);
        }
        else
        {
            // Play audio
            AudioSource.PlayClipAtPoint(shootLv1, transform.position);
            bullet = Instantiate(bulletLevel1, transform.position, Quaternion.identity);
        }

        var bulletBehavior = bullet.GetComponent<Bullet>();
        if (bulletBehavior != null)
        {
            bulletBehavior.SetProperties(damage, muzzleSpeed, target, (lv2Slow && level >= 2 ? 0.75f : lv3Slow && level >= 3 ? 1.5f : 0));
        }

        // rotate the tower to the left if the enemy is on the left if there is an enemy, otherwise keep the tower facing the right
        if (target.position.x < transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    /// <summary>
    /// Run when enemy exit tower range.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
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

        // get components animation of game object
        animIdle = gameObject.GetComponent<Animator>();

        // get components collider of game object
        CircleCollider2D circleCollider2D = gameObject.GetComponent<CircleCollider2D>();

        if (savedData != null)
        {
            if (savedData.level > 0)
                level = savedData.level;
            if (savedData.damage > 0)
                damage = savedData.damage;
            if (savedData.range > 0)
                range = savedData.range;
            if (savedData.cd > 0)
                coolDownTime = savedData.cd;
        }

        // set radius of collider
        circleCollider2D.radius = range;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        // Print all of properties of tower
        if (!canShoot && cooldownTimer.Finished)
        {
            canShoot = true;
        }

        if (targetInRange.Count > 0 && canShoot)
        {
            // start cooldown timer
            canShoot = false;
            cooldownTimer.Run();

            Transform nearestTarget = null;
            for (int i = 0; i < targetInRange.Count; i++)
            {
                if (targetInRange[i] != null)
                {
                    if (nearestTarget == null ||
                        Vector3.Distance(nearestTarget.position, MapController.DestinationPoint) >=
                            Vector3.Distance(targetInRange[i].transform.position, MapController.DestinationPoint))
                        nearestTarget = targetInRange[i].transform;
                }
            }
            if (nearestTarget != null)
            {
                // stop animation
                animIdle.Play("Idle", 0, 0);
                FireAt(nearestTarget);
            }
        }
        else
        {
            // start animation 
            animIdle.enabled = true;
        }
    }

    public class TowerSavedData
    {
        public int level { get; set; }
        public int damage { get; set; }
        public float range { get; set; }
        public float cd { get; set; }
    }

    #endregion
}
