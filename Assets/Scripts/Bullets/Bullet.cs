using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // bullet's properties
    private float speed;
    private Transform target;

    // bullet's effect
    public void SetProperties(float speed, Transform target)
    {
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
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // move bullet
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        // get components of target
        Enemy01_Base enemy = target.GetComponent<Enemy01_Base>();

        // if target is enemy, damage enemy
        if (enemy != null)
        {
            enemy.TakeDamage(10);
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
