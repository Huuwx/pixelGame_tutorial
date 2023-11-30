using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;

    public float moveSpeed;

    private bool isMoving;

    SpriteRenderer spriteRenderer;

    public SwordAttack swordAttack;

    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public LayerMask battleZoneLayer;

    float x;
    float y;

    private Vector2 input;

    bool canMove = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    public void HandleUpdate()
    {
        //Movement();

        if (canMove)
        {
            if (!isMoving)
            {
                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");


                if (input.x != 0) input.y = 0;

                if (input != Vector2.zero)
                {
                    animator.SetFloat("MoveX", input.x);
                    animator.SetFloat("MoveY", input.y);
                    x = input.x;
                    y = input.y;
                    //Debug.Log(x);
                    //Debug.Log(y);

                    var targetPos = transform.position;
                    targetPos.x += input.x;
                    targetPos.y += input.y;

                    if (isWalkable(targetPos))
                        StartCoroutine(Move(targetPos));
                }
            }

            animator.SetBool("isWalk", isMoving);

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Interact();
            }
        }

        if(input.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(input.x > 0)
        {
            spriteRenderer.flipX = false;
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

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
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
    }

    public void SwordAttack()
    {
        LockMovement();
        if(spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        if(spriteRenderer.flipX == false)
        {
            swordAttack.AttackRight();
        }
        //if(y == 1 && x == 0)
        //{
        //    swordAttack.AttackUp();
        //}
        //if(y == -1 && x == 0)
        //{
        //    swordAttack.AttackDown();
        //}
    }

    public void EndSwordAttack()
    {
        UnlocKMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlocKMovement()
    {
        canMove = true;
    }

    //void Movement()
    //{
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");
    //    Vector2 position = transform.position;

    //    animator.SetFloat("MoveX", horizontal);
    //    animator.SetFloat("MoveY", vertical);

    //    position.x = position.x + 3.0f * horizontal * Time.deltaTime;
    //    position.y = position.y + 3.0f * vertical * Time.deltaTime;
    //    transform.position = position;
    //}
}
