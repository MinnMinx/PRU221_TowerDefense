using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01_Base : MonoBehaviour
{
    /// <summary>
    /// Health of enemy.
    /// </summary>
    private decimal hp;
    public decimal Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    /// <summary>
    /// Atk of enemy.
    /// </summary>
    private decimal atk;
    public decimal Atk
    {
        get { return atk; }
        set { atk = value; }
    }

    /// <summary>
    /// Speed of enemy.
    /// </summary>
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    /// <summary>
    /// Money of enemy.
    /// </summary>
    private decimal money;
    public decimal Money
    {
        get { return money; }
        set { money = value; }
    }

    public Enemy01_Base(decimal hp, decimal atk, float speed, decimal money)
    {
        this.hp = hp;
        this.atk = atk;
        this.speed = speed;
        this.money = money;
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(atk);
    }

    // Update is called once per frame
    void Update()
    {
        //if (hp <= 0)
        //{
        //    OnDespawn();
        //}
    }

    /// <summary>
    /// Destroy enemy.
    /// </summary>
    private void OnDespawn()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// check hit the bullet.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //var a = collision.gameObject.GetComponent<Bullet>();
        //if (a != null)
        //{
        //    TakeDamage(a.Atk);
        //}

        //var b = collision.gameObject.GetComponent<Base>();
        //if (b != null)
        //{
        //    DealDamage();
        //}
    }

    /// <summary>
    /// deal damage to enemy.
    /// </summary>
    /// <param name="damage"></param>
    private void TakeDamage(decimal damage)
    {
        hp = hp - damage;
    }

    private void DealDamage()
    {

    }
}
