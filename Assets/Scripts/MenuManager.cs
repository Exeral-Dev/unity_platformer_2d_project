using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu, heroMenu, shopMenu;
    public GameObject spellsMenu, weaponMenu, levelMenu;
    public GameObject spellsHealth, spellsShield;
    public GameObject objectSword, objectBow, objectMagic;
    public GameObject amountGoldSpells, amountGoldWeapons;
    public GameObject[] levelManager; 

    private void Start()
    {
        amountGoldSpells.GetComponent<TextMeshProUGUI>().text = PersistentManager.Instance.currentGoldAmount.ToString();
        amountGoldWeapons.GetComponent<TextMeshProUGUI>().text = PersistentManager.Instance.currentGoldAmount.ToString();
    }

    private void Update()
    {
        if (PersistentManager.Instance.previousGoldAmount != PersistentManager.Instance.currentGoldAmount)
        {
            amountGoldSpells.GetComponent<TextMeshProUGUI>().text = PersistentManager.Instance.currentGoldAmount.ToString();
            amountGoldWeapons.GetComponent<TextMeshProUGUI>().text = PersistentManager.Instance.currentGoldAmount.ToString();
            PersistentManager.Instance.previousGoldAmount = PersistentManager.Instance.currentGoldAmount;
        }   
    }

    //Buttons the main menu
    public void PlayGame()
    {
        mainMenu.SetActive(false);
        heroMenu.SetActive(true);
    }
    
    public void ShopGame()
    {
        mainMenu.SetActive(false);
        shopMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    
    //Buttons back
    public void BackButton()
    {
        if (heroMenu.gameObject.activeSelf)
        {           
            heroMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        if (shopMenu.gameObject.activeSelf)
        {
            shopMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        if (spellsMenu.gameObject.activeSelf)
        {
            spellsMenu.SetActive(false);
            shopMenu.SetActive(true);
        }

        if (levelMenu.gameObject.activeSelf)
        {
            PersistentManager.Instance.selectedHero = "";
            levelMenu.SetActive(false);
            heroMenu.SetActive(true);
        }

        if (weaponMenu.gameObject.activeSelf)
        {
            weaponMenu.SetActive(false);
            shopMenu.SetActive(true);
        }
        PersistentManager.Instance.selectedOptions = "";
    }

    //Buttons next
    public void NextButton()
    {
        
        if(PersistentManager.Instance.selectedOptions == "spells")
        {
            shopMenu.SetActive(false);
            spellsMenu.SetActive(true);
        }

        if (PersistentManager.Instance.selectedOptions == "weapons")
        {
            weaponMenu.SetActive(true);
            shopMenu.SetActive(false);
        }
      
        if (PersistentManager.Instance.selectedOptions == "archer" || PersistentManager.Instance.selectedOptions == "knight" || PersistentManager.Instance.selectedOptions == "wizard")
        {
            PersistentManager.Instance.selectedHero = PersistentManager.Instance.selectedOptions;
            heroMenu.SetActive(false);
            levelMenu.SetActive(true);
        }
       

        PersistentManager.Instance.selectedOptions = "";
    }

    public void PlayButton()
    {

        if (PersistentManager.Instance.selectedOptions == "level 1")
        {
            SceneManager.LoadScene("FirstLevel");
        }

        if (PersistentManager.Instance.selectedOptions == "level 2")
        {
            SceneManager.LoadScene("SecondLevel");
        }

        if (PersistentManager.Instance.selectedOptions == "level 3")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
    }

    //Buy buttons
    public void BuyHealthSpell(GameObject price)
    {
        string spellsHealthUI = spellsHealth.GetComponent<TextMeshProUGUI>().text;

        if (PersistentManager.Instance.currentGoldAmount > 50 && int.Parse(spellsHealthUI) < 3)
        {
            PersistentManager.Instance.currentGoldAmount = PersistentManager.Instance.currentGoldAmount - 50;
            PersistentManager.Instance.amountHealthSpells++;

            spellsHealth.GetComponent<TextMeshProUGUI>().text = PersistentManager.Instance.amountHealthSpells.ToString();
        }

        if (PersistentManager.Instance.amountHealthSpells == 3)
        {
            price.GetComponent<TextMeshProUGUI>().text = "Max";
        }
    }

    public void BuyShieldSpell(GameObject price)
    {
        string spellsShieldUI = spellsShield.GetComponent<TextMeshProUGUI>().text;

        if (PersistentManager.Instance.currentGoldAmount > 50 && int.Parse(spellsShieldUI) < 3)
        {
            PersistentManager.Instance.currentGoldAmount = PersistentManager.Instance.currentGoldAmount - 50;
            PersistentManager.Instance.amountShieldSpells++;

            spellsShield.GetComponent<TextMeshProUGUI>().text = PersistentManager.Instance.amountShieldSpells.ToString();
        }

        if (PersistentManager.Instance.amountShieldSpells == 3)
        {
            price.GetComponent<TextMeshProUGUI>().text = "Max";
        }

    }

    public void ImproveSword(GameObject price)
    {
        string swordLevelUI = objectSword.GetComponent<TextMeshProUGUI>().text;

        if (PersistentManager.Instance.currentGoldAmount >= 300 && int.Parse(swordLevelUI) < 3)
        {
            PersistentManager.Instance.currentGoldAmount = PersistentManager.Instance.currentGoldAmount - 300;
            PersistentManager.Instance.levelSword++;

            objectSword.GetComponent<TextMeshProUGUI>().text = PersistentManager.Instance.levelSword.ToString();
        }

        if (int.Parse(swordLevelUI) == 2)
        {
            price.GetComponent<TextMeshProUGUI>().text = "Max level";
        }
    }

    public void ImproveBow(GameObject price)
    {    
        string bowLevelUI = objectBow.GetComponent<TextMeshProUGUI>().text;
    
        if (PersistentManager.Instance.currentGoldAmount >= 300 && int.Parse(bowLevelUI) < 3)
        {
            PersistentManager.Instance.currentGoldAmount = PersistentManager.Instance.currentGoldAmount - 300;
            PersistentManager.Instance.levelBow++;

            objectBow.GetComponent<TextMeshProUGUI>().text = PersistentManager.Instance.levelBow.ToString();
        }

        if (int.Parse(bowLevelUI) == 2)
        {
            price.GetComponent<TextMeshProUGUI>().text = "Max level";
        }
    }

    public void ImproveMagic(GameObject price)
    {
        string magicLevelUI = objectMagic.GetComponent<TextMeshProUGUI>().text;

        if (PersistentManager.Instance.currentGoldAmount >= 300 && int.Parse(magicLevelUI) < 3)
        {
            PersistentManager.Instance.currentGoldAmount = PersistentManager.Instance.currentGoldAmount - 300;
            PersistentManager.Instance.levelMagic++;
   
            objectMagic.GetComponent<TextMeshProUGUI>().text = PersistentManager.Instance.levelMagic.ToString();
        }

        if (int.Parse(magicLevelUI) == 2)
        {
            price.GetComponent<TextMeshProUGUI>().text = "Max level";
        }
    }

    //Button select
    public void SelectButton(GameObject obj)
    {
        PersistentManager.Instance.selectedOptions = obj.GetComponent<TextMeshProUGUI>().text.ToLower();
    }
   
}