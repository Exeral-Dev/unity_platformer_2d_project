 using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private int maxHealth = 100, maxShield = 50;
    public int currentHealth, currentShield;

    public HealthBar healthBar;
    public ShieldBar shieldBar;
    public DeathMenu deathMenu;
    public LevelManager levelManager;

    private Animator animator;
    private Rigidbody2D rb;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        healthBar.SetMaxHealth(maxHealth);

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    public int getCharacterMaxHealth()
    {
        return maxHealth;
    }

    public int getCharacterMaxShields()
    {
        return maxShield;
    }

    public void CharacterHeal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += levelManager.useHealSpell(currentHealth);
            healthBar.SetHealth(currentHealth);
        }     
    }

    public void CharacterDefend()
    {
        if (currentShield < maxShield)
        {
            currentShield += levelManager.useDefendSpell(currentShield);
            shieldBar.SetShield(currentShield);
        }    
    }

    // Update is called once per frame
    public void CharacterHurt(int damage, Transform enemy, int impactForce)
    {
        if (!isDead)
        {
            if (currentShield <= 0)
            {
                currentHealth -= damage;
                animator.SetTrigger("isHurt");
                healthBar.SetHealth(currentHealth);

                if (enemy.position.x < transform.position.x)
                {
                    rb.velocity = Vector2.right * impactForce * Time.deltaTime;
                }

                if (enemy.position.x > transform.position.x)
                {
                    rb.velocity = Vector2.left * impactForce * Time.deltaTime;
                }

                if (currentHealth <= 0)
                {
                    CharacterDie();
                }
            }
            else
            {
                currentShield -= damage;
                animator.SetTrigger("isHurt");
                shieldBar.SetShield(currentShield);

                if (enemy.position.x < transform.position.x)
                {
                    rb.velocity = Vector2.right * impactForce * Time.deltaTime;
                }

                if (enemy.position.x > transform.position.x)
                {
                    rb.velocity = Vector2.left * impactForce * Time.deltaTime;
                }
            }

        }
     
    }

    public void CharacterTrapped(int damage, int impactForce)
    {
        if (!isDead)
        {
            if (currentShield <= 0)
            {
                currentHealth -= damage;
                animator.SetTrigger("isHurt");
                healthBar.SetHealth(currentHealth);
                rb.velocity = Vector2.up * impactForce * Time.deltaTime;

                if (currentHealth <= 0)
                {
                    CharacterDie();
                }
            }
            else
            {
                currentShield -= damage;
                animator.SetTrigger("isHurt");
                shieldBar.SetShield(currentShield);
                rb.velocity = Vector2.up * impactForce * Time.deltaTime;
            }

        }

    }

    public void CharacterDie()
    {
        isDead = true;
        Debug.Log("Dead");
        animator.SetTrigger("isDead");
        deathMenu.DeathMenuActivate();
    }
}
