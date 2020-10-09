 using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 20f;
    [SerializeField] private int arrowDamage = 10;
    [SerializeField] private int impactForce = 300;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * arrowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyStats enemy = hitInfo.GetComponentInParent<EnemyStats>();

  
        if (enemy != null)
        {
            enemy.EnemyHurt(arrowDamage, transform, impactForce);
        }
 

        Destroy(gameObject);
    }
}
