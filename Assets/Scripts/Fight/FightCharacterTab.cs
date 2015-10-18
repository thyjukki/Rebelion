using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightCharacterTab : MonoBehaviour {
    enum BarType
    {
        HP,
        Mana,
        Stamina
    }

    /// <summary>
    /// Text for name of the character
    /// </summary>
    public Text Name
    {
        get
        {
            return transform.GetChild(0).GetComponent<Text>();
        }
    }

    /// <summary>
    /// Text for level of the character
    /// </summary>
    public Text Level
    {
        get
        {
            return transform.GetChild(1).GetComponent<Text>();
        }
    }

    /// <summary>
    /// Gameobject that holds the health bar
    /// </summary>
    public GameObject HealthBar
    {
        get
        {
            return transform.GetChild(2).gameObject;
        }
    }

    /// <summary>
    /// Gameobject that holds the mana/stamina bar
    /// </summary>
    public GameObject ExtraBar
    {
        get
        {
            return transform.GetChild(3).gameObject;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (fighter != null)
        {
            Atributes attributes = fighter.GetComponent<Atributes>();
            CharacterClass charClass = attributes.CharClass;

            SetBar(BarType.HP, HealthBar);
            if (charClass == CharacterClass.Mage)
            {
                SetBar(BarType.Mana, ExtraBar);
            }
            else
            {
                SetBar(BarType.Stamina, ExtraBar);
            }
        }
	}

    private Fighter fighter;
    public Fighter Fighter
    {
        get
        {
            return fighter;
        }
        set
        {
            fighter = value;

            SetFighter();
        }
    }


    public void SelectFighter()
    {
        Fighter.Selected = fighter;
    }

    private void SetFighter()
    {
        Atributes attributes = fighter.GetComponent<Atributes>();
        CharacterClass charClass = attributes.CharClass;

        Name.text = fighter.Name;
        Level.text = "Lvl: " + attributes.Level.ToString();

        if (charClass == CharacterClass.Mage)
        {
            SetBar(BarType.Mana, ExtraBar);
        }
        else
        {
            SetBar(BarType.Stamina, ExtraBar);
        }
    }

    private void SetBar(BarType barType, GameObject ExtraBar)
    {
        Image bar = ExtraBar.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        Image frame = ExtraBar.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        Text name = ExtraBar.transform.GetChild(1).GetComponent<Text>();
        Text value = ExtraBar.transform.GetChild(2).GetComponent<Text>();

        int max = 1;
        int cur = 1;

        switch (barType)
        {
            case BarType.HP:
                name.text = "HP:";
                max = fighter.Health;
                cur = fighter.Health / 2;
                bar.color = Color.red;
                break;
            case BarType.Mana:
                max = fighter.Points;
                cur = fighter.Points / 2;
                name.text = "Mana:";
                bar.color = Color.blue;
                break;
            case BarType.Stamina:
                max = fighter.Points;
                cur = fighter.Points / 2;
                name.text = "Stmn:";
                bar.color = new Color(255,195,0);
                break;
            default:
                break;
        }

        value.text = string.Format("{0}/{1}",cur, max);

        if (max > 0)
        {
            float percent = (float)cur / (float)max;

            Vector2 rt = frame.rectTransform.sizeDelta;
            rt.x = rt.x * percent;
            bar.rectTransform.sizeDelta = rt;
        }
    }
}
