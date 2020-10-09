using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public  bool activatedHero;
    public  GameObject knight, wizard, archer;
    public string selectedLevel;
    private PersistentManager persistentManager;
    

    private void Awake()
    {
        persistentManager = GameObject.Find("PersistentManager").GetComponent<PersistentManager>();
    }

    private void Update()
    {
  
        if (!activatedHero)
        {
            if (persistentManager.selectedHero == "knight")
            {
                activatedHero = true;
                selectedLevel = persistentManager.selectedOptions;
                knight.SetActive(true);
            }

            if (persistentManager.selectedHero == "archer")
            {
                activatedHero = true;
                selectedLevel = persistentManager.selectedOptions;
                archer.SetActive(true);
            }

            if (persistentManager.selectedHero == "wizard")
            {
                activatedHero = true;
                selectedLevel = persistentManager.selectedOptions;
                wizard.SetActive(true);
            }
          
        }
    }

    public GameObject GetCharacter()
    {
 
        if (!activatedHero)
        {
            if (persistentManager.selectedHero == "knight")
            {
                return knight;
            }

            if (persistentManager.selectedHero == "archer")
            {
                return archer;
            }

            if (persistentManager.selectedHero == "wizard")
            {
                return wizard;
            }
        }

        return null;
    }

    public int useHealSpell(int currentHealth)
    {
        if (persistentManager.amountHealthSpells > 0)
        {
            persistentManager.amountHealthSpells -= 1;

            if (currentHealth <= 70)
            {
                return 30;
            }
            else
            {
                int neededHeal = 100 - currentHealth;
                return neededHeal;
            }         
        }
      
        return 0;
    }

    public int useDefendSpell(int currentShields)
    {
        if (persistentManager.amountShieldSpells > 0)
        {
            persistentManager.amountShieldSpells -= 1;

            if (currentShields <= 30)
            {
                return 20;
            }
            else
            {
                int neededShield = 100 - currentShields;
                return neededShield;
            }
        }

        return 0;
    }

    public int DamageLevel(int damage)
    {
       
        if(persistentManager.selectedHero == "knight")
        {
            if (persistentManager.levelSword == 1)
            {
                damage += 10;
            }
            else if (persistentManager.levelSword == 2)
            {
                damage += 20;
            }
            else if (persistentManager.levelSword == 3)
            {
                damage += 30;
            }
        }

        if (persistentManager.selectedHero == "archer")
        {
            if (persistentManager.levelBow == 1)
            {
                damage += 10;
            }
            else if (persistentManager.levelBow == 2)
            {
                damage += 20;
            }
            else if (persistentManager.levelBow == 3)
            {
                damage += 30;
            }
        }

        if (persistentManager.selectedHero == "wizard")
        {
            if (persistentManager.levelMagic == 1)
            {
                damage += 10;
            }
            else if (persistentManager.levelMagic == 2)
            {
                damage += 20;
            }
            else if (persistentManager.levelMagic == 3)
            {
                damage += 30;
            }
        }

        return damage;
    }
 
}