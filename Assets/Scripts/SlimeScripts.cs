using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScripts : EnemyScript
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Vector3 homePosition;
    float moveX = 1;
    bool IsRun;

    protected override void Start()
    {
        base.Start();
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        homePosition = transform.position;
    }

    private void FixedUpdate()
    {
        IsRun = false;
        CheckDistance();
        animator.SetBool("IsRun", IsRun);
        animator.SetFloat("MoveX", moveX);
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                if (target.position.x < transform.position.x)
                {
                    moveX = -1;
                }
                else
                {
                    moveX = 1;
                }
                IsRun = true;
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                myRigidbody.MovePosition(temp);
                changeState(EnemyState.walk);
            }
        }
        else if (transform.position != homePosition && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (target.position.x < transform.position.x)
            {
                moveX = 1;
            }
            else
            {
                moveX = -1;
            }
            IsRun = true;
            Vector3 temp = Vector3.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
            myRigidbody.MovePosition(temp);
            changeState(EnemyState.walk);
        }

        
    }
    public override void RemoveEnemy()
    {
        gameObject.SetActive(false);
        Debug.Log(gameObject.name + "bien mat");
        //Destroy(gameObject);
        spawnSlimeEnemies.SpawnCo();
    }
}
