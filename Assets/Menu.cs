using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum MenuTab
{
    Character,
    Stats,
    Feats,
    Inventory,
    Settings
}

public class Menu : MonoBehaviour {

    public Inventory Inventory;
    public CharacterPanel CharacterPanel;
    public CharacterPreview CharacterPreview;

    public Text MenuText;

    private CanvasGroup canvasGroup;

    private MenuTab currentMenu;

    private static Menu instance;

    public static Menu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Menu>();
            }
            return instance;
        }
    }

    public static bool IsOpen
    {
        get
        {
            return Instance.canvasGroup.alpha == 1;
        }
    }

	// Use this for initialization
	void Start ()
    {
        currentMenu = MenuTab.Inventory;
        canvasGroup = GetComponent<CanvasGroup>();
        Inventory.GetComponent<CanvasGroup>().alpha = 0;
        CharacterPanel.GetComponent<CanvasGroup>().alpha = 0;
        CharacterPreview.GetComponent<CanvasGroup>().alpha = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OpenMenu(MenuTab menuTab)
    {
        OpenTab(menuTab);
    }

    public void OpenMenu(int menuTab)
    {
        
        OpenTab((MenuTab)menuTab);
    }

    public void OpenMenu()
    {
        OpenTab(Instance.currentMenu);
    }

    public static void ToggleMenu()
    {
        if (IsOpen)
        {
            Instance.CloseMenu();
        }
        else
        {
            Instance.OpenMenu();
        }
    }

    private void OpenTab(MenuTab menuTab)
    {
        canvasGroup.alpha = 1;
        currentMenu = menuTab;
        Time.timeScale = 0;

        switch (currentMenu)
        {
            case MenuTab.Inventory:
                Inventory.GetComponent<CanvasGroup>().alpha = 1;
                CharacterPanel.GetComponent<CanvasGroup>().alpha = 1;
                CharacterPreview.GetComponent<CanvasGroup>().alpha = 1;
                MenuText.text = "Inventory";
                break;
            default:
                break;
        }
    }

    private void CloseMenu()
    {
        Time.timeScale = 1;
        canvasGroup.alpha = 0;
        Inventory.GetComponent<CanvasGroup>().alpha = 0;
        CharacterPanel.GetComponent<CanvasGroup>().alpha = 0;
        CharacterPreview.GetComponent<CanvasGroup>().alpha = 0;
    }
}
