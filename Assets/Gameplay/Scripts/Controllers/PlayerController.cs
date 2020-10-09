using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider2d;
    private Animator animator;
    private Rigidbody2D rb;
    private CharacterStats character;
    private Vector3 characterScale;
    public bool facingRight = true;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask trap;
    //private Vector3 previousPosition;
    //private bool flipCharacter;

    public float characterSpeed = 10f;
    public float jumpForce = 20f;
 

    private void Start()
    {   
        capsuleCollider2d = gameObject.GetComponentInChildren<CapsuleCollider2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        character = gameObject.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!character.isDead)
        {
            CharacterMove();
            CharacterJump();
            CharacterUseSpells();
            CharacterTrapped();
        } 
    }

    private void CharacterMove()
    {    
        characterScale = transform.localScale;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {

            if (Input.GetAxisRaw("Horizontal") > 0)
            {             
                transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * characterSpeed * Time.deltaTime);
                characterScale.x = 1;

                if (!facingRight)
                {
                    facingRight = true;
                }
            }

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * characterSpeed * Time.deltaTime);
                characterScale.x = -1;

                if (facingRight)
                {
                    facingRight = false;
                }
            }

            transform.localScale = characterScale;
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

    }

    private void CharacterJump()
    {
        if (isGrounded() && Input.GetButton("Jump"))
        {
            animator.SetTrigger("isJump");
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void CharacterTrapped()
    {
        if (isTrapped())
        {
            character.CharacterTrapped(10, 1000);
        }
    }

    private void CharacterUseSpells()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            character.CharacterDefend();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            character.CharacterHeal();
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(capsuleCollider2d.bounds.center, capsuleCollider2d.bounds.size, 0f, Vector2.down, .1f, ground);
        return raycastHit2d.collider != null;
    }

    private bool isTrapped()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(capsuleCollider2d.bounds.center, capsuleCollider2d.bounds.size, 0f, Vector2.down, .1f, trap);
        return raycastHit2d.collider != null;
    }
}