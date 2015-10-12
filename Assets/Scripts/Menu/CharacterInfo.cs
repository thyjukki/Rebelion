using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour {

    public Text CharacterName;
    public Text CharacterAttributes;
    public Text AtributeValues;
    public Text CharacterClass;

    private GameObject currentCharacter;
    public void SetCharacterInfo()
    {
        currentCharacter = GameObject.Find("Player");

        Atributes atributes = GameObject.FindObjectOfType<Atributes>();//currentCharacter.GetComponent<Atributes>();
        //TODO(FIXME)


        string atrbiuteText = string.Empty;
        string valueText = string.Empty;
        if (atributes.Strenght > 0)
        {
            atrbiuteText = atrbiuteText + "Strenght:";
            valueText = valueText + atributes.Strenght.ToString() + "/";
            if (atributes.GetExtraStrenght() > 0)
            {
                valueText = valueText + "<color=green>+" + atributes.GetExtraStrenght().ToString() + "</color>";
            }
            else if (atributes.GetExtraStrenght() < 0)
            {
                valueText = valueText + "<color=red>" + atributes.GetExtraStrenght().ToString() + "</color>";
            }
            else
            {
                valueText = valueText + "0";
            }
            atrbiuteText = atrbiuteText + "\n";
            valueText = valueText + "\n";
        }
        if (atributes.Dexterity > 0)
        {
            atrbiuteText = atrbiuteText + "Dexterity:";
            valueText = valueText + atributes.Dexterity.ToString() + "/";
            if (atributes.GetExtraDexterity() > 0)
            {
                valueText = valueText + "<color=green>+" + atributes.GetExtraDexterity().ToString() + "</color>";
            }
            else if (atributes.GetExtraDexterity() < 0)
            {
                valueText = valueText + "<color=red>" + atributes.GetExtraDexterity().ToString() + "</color>";
            }
            else
            {
                valueText = valueText + "0";
            }
            atrbiuteText = atrbiuteText + "\n";
            valueText = valueText + "\n";
        }
        if (atributes.Intelligent > 0)
        {
            atrbiuteText = atrbiuteText + "Intelligent:";
            valueText = valueText + atributes.Intelligent.ToString() + "/";
            if (atributes.GetExtraIntelligent() > 0)
            {
                valueText = valueText + "<color=green>+" + atributes.GetExtraIntelligent().ToString() + "</color>";
            }
            else if (atributes.GetExtraIntelligent() < 0)
            {
                valueText = valueText + "<color=red>" + atributes.GetExtraIntelligent().ToString() + "</color>";
            }
            else
            {
                valueText = valueText + "0";
            }
            atrbiuteText = atrbiuteText + "\n";
            valueText = valueText + "\n";
        }
        if (atributes.Magic > 0)
        {
            atrbiuteText = atrbiuteText + "Magic:";
            valueText = valueText + atributes.Magic.ToString() + "/";
            if (atributes.GetExtraMagic() > 0)
            {
                valueText = valueText + "<color=green>+" + atributes.GetExtraMagic().ToString() + "</color>";
            }
            else if (atributes.GetExtraMagic() < 0)
            {
                valueText = valueText + "<color=red>" + atributes.GetExtraMagic().ToString() + "</color>";
            }
            else
            {
                valueText = valueText + "0";
            }
            atrbiuteText = atrbiuteText + "\n";
            valueText = valueText + "\n";
        }

        string className = "Lvl " + atributes.Level.ToString() + " " + atributes.CharClass.ToString();

        AtributeValues.text = valueText;
        CharacterAttributes.text = atrbiuteText;
        CharacterName.text = currentCharacter.name;
        CharacterClass.text = className;

    }
}
