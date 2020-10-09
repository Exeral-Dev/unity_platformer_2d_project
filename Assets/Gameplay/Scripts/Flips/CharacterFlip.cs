using UnityEngine;

public class CharacterFlip : MonoBehaviour
{
    private CharacterController character;

    // Start is called before the first frame update
    private void Start()
    {
        character = gameObject.GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
       
    }
}