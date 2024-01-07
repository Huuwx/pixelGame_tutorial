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
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public LayerMask ObjectBreakableLayer;

    protected virtual void Start()
    {
        currentState = EnemyState.idle;
        animator = GetComponent<Animator>();
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

    public float health = 10;

    public void Defeated()
    {
        animator.SetTrigger("IsDead");
    }
    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    protected bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.01f, solidObjectsLayer | interactableLayer | ObjectBreakableLayer) != null)
        {
            return false;
        }

        return true;
    }

    protected void changeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
