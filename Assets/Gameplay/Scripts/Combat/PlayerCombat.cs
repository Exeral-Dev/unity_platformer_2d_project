using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
   
    public float attackRange = 0.5f;
    public int impactForce = 600;
    public int attackDamage = 10;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();          
        }
    }

    void Attack()
    {
        animator.SetTrigger("isAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
       
        foreach(Collider2D enemy  in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().EnemyHurt(attackDamage, transform, impactForce);
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
