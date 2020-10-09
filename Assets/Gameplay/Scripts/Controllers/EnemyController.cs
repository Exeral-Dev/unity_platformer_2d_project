using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool followPlayer;
    private bool rightSide;

    private Animator animator;
    private Rigidbody2D rb;
    private EnemyStats enemy;
    private GameObject character;
    private CapsuleCollider2D capsuleCollider2d;

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float enemyDirection = 1f;
    [SerializeField] private float rayDistance = 1f;

    [SerializeField] private float distanceRight = 1f;
    [SerializeField] private float distanceLeft = 1.7f;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask trap;
    [SerializeField] private Vector2 rayCastOffsetWall;
    [SerializeField] private Vector2 rayCastOffsetLedge;
    [SerializeField] private LevelManager levelManager;

    private RaycastHit2D ground;
    private RaycastHit2D rightWall;
    private RaycastHit2D leftWall;
    private RaycastHit2D rightLedge;
    private RaycastHit2D leftLedge;

   

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        enemy = gameObject.GetComponent<EnemyStats>();
        character = levelManager.GetCharacter();
        capsuleCollider2d = gameObject.GetComponentInChildren<CapsuleCollider2D>();

    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(character);
        if(character != null)
        {
            if (!enemy.isDead && !character.GetComponent<CharacterStats>().isDead)
            {
                AIDetecting();
                AISearching();
                EnemyTrapped();
            }
            else
            {
                transform.Translate(new Vector3(0, 0, 0) * enemySpeed * Time.deltaTime);
                animator.SetBool("isMove", false);
            }
        }
     
    }

    private void AIDetecting()
    {
        //Debug.Log("Player position: " + playerTransform.position);
        //Debug.Log("Enemy position: " + transform.position);
        //Debug.Log("Player detected: " + followPlayer);
        //Debug.Log("Distance: " + (transform.position.x - playerTransform.position.x));
        float distance = (transform.position.x - character.transform.position.x);

        if (Math.Abs(distance) > detectionRange)
        {
            followPlayer = false;
        }
        else 
        {
            followPlayer = true;
        }


        if (followPlayer)
        {
        
            if (character.transform.position.x < transform.position.x)
            {
                rightSide = false;
                transform.eulerAngles = new Vector3(0, -180, 0);
            }

            if(character.transform.position.x > transform.position.x)
            {
                rightSide = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            
            if (!rightSide && Math.Abs(distance) < distanceLeft)
            {
                transform.Translate(new Vector3(0, 0, 0) * enemySpeed * Time.deltaTime);
                GetComponent<EnemyCombat>().attackPlayer = true;
                animator.SetBool("isMove", false);
            }

            if (rightSide && Math.Abs(distance) < distanceRight)
            {
                transform.Translate(new Vector3(0, 0, 0) * enemySpeed * Time.deltaTime);
                GetComponent<EnemyCombat>().attackPlayer = true;
                animator.SetBool("isMove", false);
            }


            if (!rightSide && Math.Abs(distance) > distanceLeft)
            {
                transform.Translate(new Vector3(enemyDirection, 0, 0) * enemySpeed * Time.deltaTime);
                GetComponent<EnemyCombat>().attackPlayer = false;
                animator.SetBool("isMove", true);
            }


            if (rightSide && Math.Abs(distance) > distanceRight)
            {
                transform.Translate(new Vector3(enemyDirection, 0, 0) * enemySpeed * Time.deltaTime);
                GetComponent<EnemyCombat>().attackPlayer = false;
                animator.SetBool("isMove", true);
            }

        }
        
        if(!followPlayer && Math.Abs(distance) > 1.8f)
        {
            transform.Translate(new Vector3(enemyDirection, 0, 0) * enemySpeed * Time.deltaTime);
            animator.SetBool("isMove", true);
        }
    }

    private void AISearching()
    {

        rightWall = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffsetWall.x, transform.position.y + rayCastOffsetWall.y), Vector2.right, rayDistance, layerMask);
        Debug.DrawRay(new Vector2(transform.position.x + rayCastOffsetWall.x, transform.position.y + rayCastOffsetWall.y), new Vector2(rayDistance, 0), Color.red);

        if (rightWall.collider != null)
        {
            if (!followPlayer)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
 
            }
            else
            {
                AIJump();
            }
        }

        leftWall = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffsetWall.x, transform.position.y + rayCastOffsetWall.y), Vector2.left, rayDistance, layerMask);
        Debug.DrawRay(new Vector2(transform.position.x - rayCastOffsetWall.x, transform.position.y + rayCastOffsetWall.y), new Vector2(-rayDistance, 0), Color.blue);

        if (leftWall.collider != null)
        {
            if (!followPlayer)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                AIJump();
            }
        }

        //Ground ray
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + rayCastOffsetLedge.y), new Vector2(0, -rayDistance), Color.green);

        //if (!followPlayer)
        //{
            rightLedge = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffsetLedge.x, transform.position.y + rayCastOffsetLedge.y), Vector2.down, rayDistance, layerMask);
            Debug.DrawRay(new Vector2(transform.position.x + rayCastOffsetLedge.x, transform.position.y + rayCastOffsetLedge.y), new Vector2(0, -rayDistance), Color.blue);

            if (rightLedge.collider == null)
            {
                if (!followPlayer)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
                else
                {
                    AIJump();
                }           
            }

            leftLedge = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffsetLedge.x, transform.position.y + rayCastOffsetLedge.y), Vector2.down, rayDistance, layerMask);
            Debug.DrawRay(new Vector2(transform.position.x - rayCastOffsetLedge.x, transform.position.y + rayCastOffsetLedge.y), new Vector2(0, -rayDistance), Color.blue);

            if (leftLedge.collider == null)
            {
                if (!followPlayer)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    AIJump();
                }
            }
        //}
    }

    private void AIJump()
    {
        if (isGrounded())
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void EnemyTrapped()
    {
        if (isTrapped())
        {
            enemy.EnemyTrapped(10, 700);
        }
    }

    private bool isGrounded()
    {
        ground = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + rayCastOffsetLedge.y), Vector2.down, rayDistance, layerMask);
        return ground.collider != null;
    }

    private bool isTrapped()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(capsuleCollider2d.bounds.center, capsuleCollider2d.bounds.size, 0f, Vector2.down, .1f, trap);
        return raycastHit2d.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}