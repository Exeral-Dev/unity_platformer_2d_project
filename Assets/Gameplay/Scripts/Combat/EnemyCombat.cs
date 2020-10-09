using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    
    private float elapsedTime = 0f;
    [SerializeField] private float AttackInterval = 0.5f;
    public float attackRange = 0.5f;
    public int impactForce = 600;
    public int attackDamage = 10;
    public bool attackPlayer;


    // Update is called once per frame
    void Update()
    {
        if (attackPlayer)
        {
            if (Time.time > elapsedTime)
            {
                Attack();
                elapsedTime = Time.time + AttackInterval;
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("isAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<CharacterStats>().CharacterHurt(attackDamage, transform, impactForce);

            if (enemy.GetComponent<CharacterStats>().isDead)
            {
                attackPlayer = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
