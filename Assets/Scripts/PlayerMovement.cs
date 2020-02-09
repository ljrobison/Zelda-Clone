using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public float attackDelay;


    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        attackDelay = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
    }

    // Updates only during physics frames.
    void FixedUpdate() 
    {
        if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
    }
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("isMoving", true);
        }
        else
            animator.SetBool("isMoving", false);
    }
    void MoveCharacter()
    {
        change.Normalize();
        myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("isAttacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackDelay);
        currentState = PlayerState.walk;
    }
}
