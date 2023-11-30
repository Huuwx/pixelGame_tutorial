using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D SwordCollider;
    public float damage = 2;

    public void AttackRight()
    {
        //Debug.Log("Attack Right");
        SwordCollider.enabled = true;
        SwordCollider.offset = new Vector2(0, 0);
    }

    public void AttackLeft() {
        //Debug.Log("Attack Left");
        SwordCollider.enabled = true;
        SwordCollider.offset = new Vector2(-1, 0);
    }

    //public void AttackUp()
    //{
    //    Debug.Log("Attack Up");
    //SwordCollider.offset = new Vector2(-0.5, 1);
    //}

    //public void AttackDown()
    //{
    //    Debug.Log("Attack Down");
    //    SwordCollider.enabled = true;
    //SwordCollider.offset = new Vector2(-0.5, -1);
    //}

    public void StopAttack()
    {
        SwordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            //deal damage to the enemy
            EnemyScript enemy = collision.GetComponent<EnemyScript>();

            if(enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }
}
