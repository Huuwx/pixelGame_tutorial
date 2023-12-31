using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    public void KnockB(Collider2D other)
    {
        Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
        if (enemy != null)
        {
            enemy.isKinematic = false;
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * thrust;
            enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(knockCo(enemy));
        }
    }

    public IEnumerator knockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
    }
}
