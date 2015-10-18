using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public enum OptionState
{
    None,
    Main,
    Attacks,
    Powers,
    Spells,
    Items
}

public class FightOptions : MonoBehaviour {


    private OptionState optionState;

    public OptionState OptionState
    {
        get
        {
            return optionState;
        }
        set
        {
            optionState = value;
        }
    }

    public Button AttackButton
    {
        get
        {
            return transform.GetChild(0).GetComponent<Button>();
        }
    }
    public Button SpellButton
    {
        get
        {
            return transform.GetChild(1).GetComponent<Button>();
        }
    }
    public Button ItemButton
    {
        get
        {
            return transform.GetChild(2).GetComponent<Button>();
        }
    }
    public Button WaitButton
    {
        get
        {
            return transform.GetChild(3).GetComponent<Button>();
        }
    }
    public GridLayoutGroup GLG
    {
        get
        {
            return GetComponent<GridLayoutGroup>();
        }
    }

    private List<Button> ChoiseButtons;

    public GameObject ButtonPrefab;


    void Start()
    {
        ChoiseButtons = new List<Button>();
        gameObject.SetActive(false);
    }

    public void Attack()
    {
        SetState(OptionState.Attacks);
    }

    public void Magic()
    {

    }

    public void Power()
    {

    }

    public void Item()
    {

    }

    public void Wait()
    {

    }

    private void SetAttacks()
    {
        foreach (var button in ChoiseButtons)
        {
            Destroy(button.gameObject);
        }
        ChoiseButtons.Clear();

        if (Fighter.Selected != null)
        {
            Atributes atributes = Fighter.Selected.Atributes;

            foreach(Attack attacks in atributes.Feats)
            {
                GameObject newButton = GameObject.Instantiate(ButtonPrefab);
                newButton.transform.SetParent(transform);
                Button button = newButton.GetComponent<Button>();
                button.onClick.AddListener(delegate { SelectedAttack(Fighter.Selected, attacks); });


                EventTrigger eventTrigger = newButton.GetComponent<EventTrigger>();
                EventTrigger.TriggerEvent enterTrigger = new EventTrigger.TriggerEvent();
                EventTrigger.TriggerEvent exitTrigger = new EventTrigger.TriggerEvent();
                enterTrigger.AddListener(delegate { PointerEnter(Fighter.Selected, attacks, button.gameObject); });
                exitTrigger.AddListener(delegate { PointerExit(); });

                EventTrigger.Entry enterEntry = new EventTrigger.Entry() {
                    callback = enterTrigger,
                    eventID = EventTriggerType.PointerEnter };
                EventTrigger.Entry exitEntry = new EventTrigger.Entry() {
                    callback = exitTrigger,
                    eventID = EventTriggerType.PointerExit
                };

                eventTrigger.triggers.Add(enterEntry);
                eventTrigger.triggers.Add(exitEntry);
            }
        }
    }

    private void PointerEnter(Fighter attacker, Attack attack, GameObject slot)
    {
        print("PointerEnter");
        InventoryManager.Instance.toolTipObject.SetActive(true);

        float xPos = slot.transform.position.x;
        float yPos = slot.transform.position.y;

        InventoryManager.Instance.visualTextObject.text = InventoryManager.Instance.sizeTextObject.text = attack.GetToolTip(true, true);
        InventoryManager.Instance.toolTipObject.transform.position = new Vector2(xPos, yPos);

    }

    private void PointerExit()
    {
        print("PointerExit");
        InventoryManager.Instance.toolTipObject.SetActive(false);
    }
    private void SelectedAttack(Fighter attacker, Attack attack)
    {

    }


    public void SetState(OptionState state)
    {
        gameObject.SetActive(true);
        if (state != OptionState.Main)
        {
            AttackButton.gameObject.SetActive(false);
            SpellButton.gameObject.SetActive(false);
            ItemButton.gameObject.SetActive(false);
            WaitButton.gameObject.SetActive(false);
        }

        switch (state)
        {
            case OptionState.None:
                ChoiseButtons.Clear();
                gameObject.SetActive(false);
                break;
            case OptionState.Main:

                AttackButton.gameObject.SetActive(true);
                SpellButton.gameObject.SetActive(true);
                ItemButton.gameObject.SetActive(true);
                WaitButton.gameObject.SetActive(true);
                GLG.cellSize = new Vector2(100, 40);
	            if (Fighter.Selected != null
                    && Fighter.Selected.GetComponent<Atributes>().CharClass == CharacterClass.Mage)
                {
                    SpellButton.GetComponentInChildren<Text>().text = "Spell";
                    SpellButton.onClick.RemoveAllListeners();
                    SpellButton.onClick.AddListener(delegate { Magic(); });
                }
                else
                {
                    SpellButton.GetComponentInChildren<Text>().text = "Power";
                    SpellButton.onClick.AddListener(delegate { Power(); });
                }
                break;
            case OptionState.Attacks:
                GLG.cellSize = new Vector2(40, 40);
                SetAttacks();
                break;
            case OptionState.Powers:
                break;
            case OptionState.Spells:
                break;
            case OptionState.Items:
                break;
            default:
                break;
        }
    }
}
