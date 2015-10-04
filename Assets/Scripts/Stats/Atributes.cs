using UnityEngine;
using System.Collections;

public enum CharacterClass
{
    Soldier,
    Mage,
    Rogue
}
public class Atributes : MonoBehaviour
{
    public int Strenght = 8;

    public int Dexterity = 8;

    public int Intelligent = 8;

    public int Magic = 8;


    public int Experience
    {
        get;
        private set;
    }

    public int Level
    {
        get;
        private set;
    }

    public CharacterClass CharClass;

    public bool CanLevelUp
    {
        get
        {
            if (expToLevel > Level)
                return true;
            else
                return false;
        }
    }

    private int expToLevel
    {
        get { return Experience / 750; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GiveExperience(int exp)
    {
        Experience += exp;
    }


    public void ClearExperience()
    {
        Experience = 0;
        Level = 0;
    }

    public int GetExtraStrenght()
    {
        int collector = 0;

        foreach (EquipmentSlot equipmentSlot in GetComponentsInChildren<EquipmentSlot>())
        {
            ItemScript itemScript = equipmentSlot.Item;

            if (itemScript != null)
            {
                Equipment item = (Equipment)itemScript.Item;

                collector += item.Strenght;
            }
        }
        return collector;
    }

    public int GetExtraDexterity()
    {
        int collector = 0;

        foreach (EquipmentSlot equipmentSlot in GetComponentsInChildren<EquipmentSlot>())
        {
            ItemScript itemScript = equipmentSlot.Item;

            if (itemScript != null)
            {
                Equipment item = (Equipment)itemScript.Item;

                collector += item.Dexterity;
            }
        }
        return collector;
    }

    public int GetExtraIntelligent()
    {
        int collector = 0;

        foreach (EquipmentSlot equipmentSlot in GetComponentsInChildren<EquipmentSlot>())
        {
            ItemScript itemScript = equipmentSlot.Item;

            if (itemScript != null)
            {
                Equipment item = (Equipment)itemScript.Item;

                collector += item.Stamina; //TODO(Jukki) FIXME
            }
        }
        return collector;
    }

    public int GetExtraMagic()
    {
        int collector = 0;

        foreach (EquipmentSlot equipmentSlot in GetComponentsInChildren<EquipmentSlot>())
        {
            ItemScript itemScript = equipmentSlot.Item;

            if (itemScript != null)
            {
                Equipment item = (Equipment)itemScript.Item;

                collector += item.Magic;
            }
        }
        return collector;
    }
}
