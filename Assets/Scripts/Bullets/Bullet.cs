using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // bullet's properties
    private float speed;
    private float atk;
    private Transform target;
    private const float HIT_DISTANCE = 0.1f;

    // bullet's effect
    public void SetProperties(float atk, float speed, Transform target)
    {
        this.atk = atk;
        this.speed = speed;
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        // if target is null, destroy bullet
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // move bullet to target
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // if bullet reach target, destroy bullet
        if (dir.magnitude <= HIT_DISTANCE)
        {
            HitTarget();
            return;
        }

        // move bullet
        transform.position = Vector3.MoveTowards(transform.position, target.position, distanceThisFrame);
        // rotate toward direction
        transform.rotation = Quaternion.Euler(0, 0, Quaternion.LookRotation(Vector3.forward, dir).eulerAngles.z + 90);
    }

    private void HitTarget()
    {
        // get components of target
        Enemy01_Base enemy = target.GetComponent<Enemy01_Base>();

        // if target is enemy, damage enemy
        if (enemy != null)
        {
            //enemy.AddModifier(new Modifier()
            //{
            //    type = ModifierType.Spd,
            //    timeLeft = 1f,
            //    multipler = 0.3f,
            //});
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
