using UnityEngine;
using System.Collections;


public enum ItemType {Mana, Health};

public class Item : MonoBehaviour {

    public ItemType type;

    public Sprite sprite;

    public int maxSize;



    public void Use()
    {
        switch (type)
        {
            case ItemType.Mana:
                Debug.Log("I just used a mana potion");
                break;
            case ItemType.Health:
                Debug.Log("I just used a health potion");
                break;
        }
    }
}
