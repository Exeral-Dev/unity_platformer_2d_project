using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrow;

    private PlayerController character;

    private void Start()
    {
        character = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {

        if (character.facingRight)
        {
            Instantiate(arrow, shootPoint.position, Quaternion.Euler(0, 0, -90));
        }

        if (!character.facingRight)
        {
            Instantiate(arrow, shootPoint.position, Quaternion.Euler(0, 0, 90));
        }

        //Debug.Log("Character position: " + transform.position.x);
        //Debug.Log("Bullet position: " + shootPoint.position.x);
        
    }
}
