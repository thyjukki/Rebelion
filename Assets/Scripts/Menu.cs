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
    public CanvasGroup TabsGroup;
    public CanvasGroup SettingsGroup;
    public CharacterInfo CharacterAtributes;

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
        currentMenu = MenuTab.Character;
        canvasGroup = GetComponent<CanvasGroup>();

        TabsGroup.alpha = 0;
        TabsGroup.interactable = false;

        hideGroups();
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
        TabsGroup.alpha = 1;
        TabsGroup.interactable = true;
        TabsGroup.blocksRaycasts = true;
        Time.timeScale = 0;


        hideGroups();

        switch (currentMenu)
        {
            case MenuTab.Character:
                CharacterPreview.GetComponent<CanvasGroup>().alpha = 1;
                CharacterPreview.GetComponent<CanvasGroup>().interactable = true;
                CharacterPreview.GetComponent<CanvasGroup>().blocksRaycasts = true;
                CharacterAtributes.GetComponent<CanvasGroup>().alpha = 1;
                CharacterAtributes.GetComponent<CanvasGroup>().interactable = true;
                CharacterAtributes.GetComponent<CanvasGroup>().blocksRaycasts = true;
                CharacterAtributes.SetCharacterInfo();
                MenuText.text = "Character";
                break;
            case MenuTab.Stats:
                MenuText.text = "Stats";
                break;
            case MenuTab.Feats:
                MenuText.text = "Feats";
                break;
            case MenuTab.Inventory:
                Inventory.GetComponent<CanvasGroup>().alpha = 1;
                Inventory.GetComponent<CanvasGroup>().interactable = true;
                Inventory.GetComponent<CanvasGroup>().blocksRaycasts = true;
                CharacterPanel.GetComponent<CanvasGroup>().alpha = 1;
                CharacterPanel.GetComponent<CanvasGroup>().interactable = true;
                CharacterPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                CharacterPreview.GetComponent<CanvasGroup>().alpha = 1;
                MenuText.text = "Inventory";
                break;
            case MenuTab.Settings:
                MenuText.text = "Settings";
                SettingsGroup.alpha = 1;
                SettingsGroup.interactable = true;
                SettingsGroup.blocksRaycasts = true;
                break;
            default:
                break;
        }
    }



    private void CloseMenu()
    {
        Time.timeScale = 1;
        canvasGroup.alpha = 0;
        TabsGroup.alpha = 0;
        TabsGroup.interactable = false;
        TabsGroup.blocksRaycasts = false;
        hideGroups();
    }

    private void hideGroups()
    {
        Inventory.GetComponent<CanvasGroup>().alpha = 0;
        Inventory.GetComponent<CanvasGroup>().interactable = false;
        Inventory.GetComponent<CanvasGroup>().blocksRaycasts = false;
        CharacterPanel.GetComponent<CanvasGroup>().alpha = 0;
        CharacterPanel.GetComponent<CanvasGroup>().interactable = false;
        CharacterPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        CharacterPreview.GetComponent<CanvasGroup>().alpha = 0;
        CharacterAtributes.GetComponent<CanvasGroup>().alpha = 0;
        CharacterAtributes.GetComponent<CanvasGroup>().interactable = false;
        CharacterAtributes.GetComponent<CanvasGroup>().blocksRaycasts = false;
        SettingsGroup.alpha = 0;
        SettingsGroup.interactable = false;
        SettingsGroup.blocksRaycasts = false;
    }

    public void SettingsButton(int button)
    {
        switch (button)
	    {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                Application.Quit();
                break;
		    default:
                break;
	    }
    }
}
