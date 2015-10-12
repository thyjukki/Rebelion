using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CharacterClass
{
    Soldier,
    Mage,
    Rogue,
    Any
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

    public List<int> UnlockedFeatsID;

    public List<Attack> Feats
    {
        get
        {
            List<Attack> collector = new List<Attack>();
            foreach (int featID in UnlockedFeatsID)
            {
                Attack feat = FightManager.Instance.Feats.FindAttack(featID);
                if (feat != null)
                {
                    collector.Add(feat);
                }
            }

            return collector;
        }
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
