using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // bullet's properties
    private float atk;
    private Vector3 velocity;
    private float slowDur = 0;
    private static Collider2D[] cacheCollider = new Collider2D[1];
    private Collider2D selfCollider;
    private static ContactFilter2D enemyColliderFilter;

    private void Awake()
    {
        selfCollider = GetComponent<Collider2D>();
        enemyColliderFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
        enemyColliderFilter.SetDepth(float.NegativeInfinity, float.PositiveInfinity);
    }

    // bullet's effect
    public void SetProperties(float atk, float speed, Transform target, float slowDur = 0)
    {
        this.atk = atk;
        //this.speed = speed;
        var distance = target.position - transform.position;
        distance.z = 0;
        this.velocity = distance.normalized * speed * 2;
        this.slowDur = slowDur;
        // rotate toward direction
        transform.rotation = Quaternion.Euler(0, 0, Quaternion.LookRotation(Vector3.forward, velocity).eulerAngles.z + 90);
    }

    // Update is called once per frame
    void Update()
    {
        // move bullet
        transform.position += velocity * Time.deltaTime;
        if (Physics2D.OverlapCollider(selfCollider, enemyColliderFilter, cacheCollider) > 0)
            HitTarget();
    }

    private void HitTarget()
    {
        if (cacheCollider[0] == null || cacheCollider[0].gameObject == null)
        {
            cacheCollider[0] = null;
            return;
        }
        // get components of target
        Enemy01_Base enemy = cacheCollider[0].GetComponent<Enemy01_Base>();

        // if target is enemy, damage enemy
        if (enemy != null)
        {
            if (slowDur > 0)
            {
                enemy.AddModifier(new Modifier()
                {
                    type = Modifier.ModifierType.Spd,
                    timeLeft = slowDur,
                    multipler = 0.3f,
                });
            }
            enemy.TakeDamage((decimal)atk);
        }

        // destroy bullet
        Destroy(gameObject);
    }

    /// <summary>
    /// Destroy bullet when it is out of camera
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
