using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    public void KnockB(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                //enemy.isKinematic = false;
                if (other.gameObject.CompareTag("Enemy"))
                {
                    hit.GetComponent<EnemyScript>().currentState = EnemyState.stagger;
                    other.GetComponent<EnemyScript>().Knock(hit, knockTime);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    hit.GetComponent<CharacterController>().currentState = PlayerState.stagger;
                    other.GetComponent<CharacterController>().Knock(hit);
                }
                
            }
        }
    }
}
