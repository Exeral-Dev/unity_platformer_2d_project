using UnityEditor;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private Animator animator;
    private Rigidbody2D rb;
    public LevelManager levelManager;

    private int currentHealth;
    public bool isDead;
 
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void EnemyHurt(int damage, Transform player, int impactForce)
    {
        if(!isDead)
        {         
            currentHealth -= levelManager.DamageLevel(damage);
          
            animator.SetTrigger("isHurt");

            if (player.position.x < transform.position.x)
            {
                Debug.Log("Left");
                rb.velocity = Vector2.right * impactForce * Time.deltaTime;
            }

            if (player.position.x > transform.position.x)
            {
                Debug.Log("Right");
                rb.velocity = Vector2.left * impactForce * Time.deltaTime;
            }

            if (currentHealth <= 0)
            {          
                EnemyDie();
            }

            Debug.Log("The health of enemy" + currentHealth);
        }
    }

    public void EnemyTrapped(int damage, int impactForce)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            animator.SetTrigger("isHurt");
            rb.velocity = Vector2.up * impactForce * Time.deltaTime;

            if (currentHealth <= 0)
            {
                EnemyDie();
            }
        }

    }

    public void EnemyDie()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        PersistentManager.Instance.currentGoldAmount += 50;
        Destroy(gameObject, 2f);
    }
}
