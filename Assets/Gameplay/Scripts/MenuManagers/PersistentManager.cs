using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }
    public int currentGoldAmount, previousGoldAmount;
    public int amountHealthSpells, amountShieldSpells;
    public int levelSword, levelBow, levelMagic;
    public string selectedHero, selectedLevel, selectedOptions;

    // Start is called before the first frame update
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            currentGoldAmount = 3000;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
