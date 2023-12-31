using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float damage = 2;
    public float Range = 0.3f;
    private Vector3 interactPos;
    private Animator animator;
    public float thrust;
    public float knockTime;


    public float moveSpeed;

    private bool isMoving;


    public LayerMask enemiesLayer;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public LayerMask battleZoneLayer;
    public LayerMask ObjectBreakableLayer;

    private Vector3 input;

    bool canMove = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX", -1);
    }
    // Update is called once per frame
    public void HandleUpdate()
    {
        //Movement();

        if (canMove)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //if (input.x != 0) input.y = 0;

            if (input != Vector3.zero)
            {
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                var pos = transform.position;


                if (isWalkable(targetPos))
                {
                    input.Normalize();
                    Movement(pos, input.x, input.y);
                    isMoving = true;
                }
                
            }

            animator.SetBool("isWalk", isMoving);

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Interact();
            }
            isMoving = false;
        }
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.01f, solidObjectsLayer | interactableLayer | ObjectBreakableLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void CheckForEncounters()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.01f, battleZoneLayer) != null)
        {
            Debug.Log("You're in a battle zone!");
        }

    }

    void OnFire()
    {
        animator.SetTrigger("Sword_Attack");
        var facingDir = new Vector3(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, Range, enemiesLayer | ObjectBreakableLayer);
        if (collider != null)
        {

            OrcScripts Oenemy = collider.GetComponent<OrcScripts>();
            SlimeScripts Senemy = collider.GetComponent<SlimeScripts>();
            SkeletonScripts Skenemy = collider.GetComponent<SkeletonScripts>();
            ObjectScrips oj = collider.GetComponent<ObjectScrips>();
            if (Oenemy != null)
            {
                Oenemy.Health -= damage;
                if(Oenemy.health > 0)
                {
                    KnockBack(collider);
                }
                Debug.Log("Orc");
            }
            else if (Senemy != null)
            {
                Senemy.Health -= damage;
                if (Senemy.health > 0)
                {
                    KnockBack(collider);
                }
                Debug.Log("Slime");
            }
            else if(Skenemy != null)
            {
                Skenemy.Health -= damage;
                if (Skenemy.health > 0)
                {
                    KnockBack(collider);
                }
                Debug.Log("Skeleton");
            }
            if (oj != null)
            {
                oj.destroy();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactPos, Range);
    }


    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlocKMovement()
    {
        canMove = true;
    }

    void Movement(Vector2 pos, float horizontal, float vertical)
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //Vector2 position = transform.position;

        //animator.SetFloat("MoveX", horizontal);
        //animator.SetFloat("MoveY", vertical);

        pos.x = pos.x + moveSpeed * horizontal * Time.deltaTime;
        pos.y = pos.y + moveSpeed * vertical * Time.deltaTime;
        transform.position = pos;

        CheckForEncounters();
    }

    
    private void KnockBack(Collider2D other)
    {
        Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
        if (enemy != null)
        {
            //enemy.isKinematic = false;
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * thrust;
            enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(knockCo(enemy));
        }
    }

    private IEnumerator knockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
        }
    }
}
