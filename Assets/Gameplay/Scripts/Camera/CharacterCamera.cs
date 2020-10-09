using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public Canvas canvas;
    public LevelManager levelManager;

    private Camera playerCamera;

    private void Awake()
    {
        if (levelManager.activatedHero == true)
        {
            playerCamera = levelManager.GetCharacter().GetComponentInChildren<Camera>();
            canvas.worldCamera = playerCamera;
        }
    }

}
