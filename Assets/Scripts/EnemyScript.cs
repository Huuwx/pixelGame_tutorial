using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    protected Animator animator;
    public string nameEnemy;
    public int baseAttack;
    public float moveSpeed;

    protected virtual void Start()
    {
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
}
