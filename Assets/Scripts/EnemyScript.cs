using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class EnemyScript : MonoBehaviour
{
    public EnemyState currentState;
    protected Animator animator;
    public string nameEnemy;
    public int baseAttack;
    public float moveSpeed;
    public float health;
    public FloatValue maxHealth;

    protected virtual void Start()
    {
        currentState = EnemyState.idle;
        animator = GetComponent<Animator>();
        health = maxHealth.initialValue;
    }

    public float Health
    {
        set
        {
            animator.SetTrigger("IsTakeDamage");
            Debug.Log(value);
            health = value;
            if(health <= 0)
            {
                Defeated();
            }
        }
        get { return health; }
    }
    

    public void Defeated()
    {
        animator.SetTrigger("IsDead");
    }
    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    protected void changeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }

    public void Knock(Rigidbody2D myRigidboy, float knockTime)
    {
        StartCoroutine(knockCo(myRigidboy, knockTime));
    }

    public IEnumerator knockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
