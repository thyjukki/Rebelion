using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Mono.Cecil;

public class FeatsMenu : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public GameObject IconPrefab;
    public GridLayoutGroup attacksGlg;

    void Start()
    {
        SetAttacks();
    }

    List<Attack> ignore;
    private bool foundStage2;
    private bool foundStage3;
    public void SetAttacks()
    {
        var children = new List<GameObject>();
        foreach (Transform child in attacksGlg.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        FeatContainer feats = FightManager.Instance.Feats;
        ignore = new List<Attack>();
        foreach (var attack in feats.Attacks)
        {
            if (!ignore.Contains(attack))
            {
                foundStage2 = false;
                foundStage3 = false;
                AddAttack(attack);
                if (!foundStage2)
                {
                    AddSpace();
                    AddSpace();
                }
                if (!foundStage3)
                {
                    AddSpace();
                    AddSpace();
                }
            }
        }
    }

    private void AddAttack(Attack attack)
    {
        GameObject character = Menu.Instance.Character;
        Atributes attribute = character.GetComponent<Atributes>();
        CharacterClass charClass = attribute.CharClass;

        FeatContainer feats = FightManager.Instance.Feats;

        if (attack.Class == charClass || attack.Class == CharacterClass.Any)
        {
            Sprite iconSprite = Resources.Load<Sprite>(attack.Icon);

            GameObject attackButton = GameObject.Instantiate(IconPrefab);

            attackButton.transform.SetParent(attacksGlg.transform);
            attackButton.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite;
            attackButton.GetComponent<FeatSlot>().Feat = attack;
            attackButton.name = attack.Name;
            attackButton.transform.localScale = new Vector3(1f, 1f, 1f);


            if (Menu.Instance.Character.GetComponent<Atributes>().Feats.Contains(attackButton.GetComponent<FeatSlot>().Feat))
                attackButton.GetComponent<FeatSlot>().Unlocked = true;
            else
                attackButton.GetComponent<FeatSlot>().Unlocked = false;

            if (attack.MinLevel > attribute.Level)
            {
                attackButton.GetComponent<FeatSlot>().EnoughLevel = false;
            }
            else
            {
                attackButton.GetComponent<FeatSlot>().EnoughLevel = true;
            }


            ignore.Add(attack);

            foreach (var other in feats.Attacks)
            {
                if (other.Need == attack.ID && other != attack)
                {
                    AddArrow();
                    AddAttack(other);
                    if (!foundStage2)
                        foundStage2 = true;
                    else
                        foundStage3 = true;
                    break;
                }
            }
        }
    }

    private void AddArrow()
    {
        GameObject arrow = GameObject.Instantiate(ArrowPrefab);

        arrow.transform.SetParent(attacksGlg.transform);
        arrow.name = "Arrow";
    }

    private void AddSpace()
    {
        GameObject spacer = new GameObject();

        spacer.AddComponent<RectTransform>();
        spacer.transform.SetParent(attacksGlg.transform);
        spacer.name = "Spacer";
    }

    /// <summary>
    /// Shows the tooltip of the given slot
    /// </summary>
    /// <param name="slot"></param>
    public void ShowToolTip(GameObject slot)
    {
        FeatSlot tmpSlot = slot.GetComponent<FeatSlot>();

        if (tmpSlot != null)
        {
            InventoryManager.Instance.toolTipObject.SetActive(true);

            float xPos = slot.transform.position.x;
            float yPos = slot.transform.position.y;

            InventoryManager.Instance.visualTextObject.text = InventoryManager.Instance.sizeTextObject.text = tmpSlot.Feat.GetToolTip(tmpSlot.Unlocked, tmpSlot.EnoughLevel);
            InventoryManager.Instance.toolTipObject.transform.position = new Vector2(xPos, yPos);
        }

    }

    /// <summary>
    /// Hides the tool tip
    /// </summary>
    public void HideToolTip()
    {
        InventoryManager.Instance.toolTipObject.SetActive(false);
    }
}
