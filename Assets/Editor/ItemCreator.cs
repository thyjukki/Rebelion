using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class ItemCreator : EditorWindow
{

    string itemName;
    string description;
    ItemType itemType;

    Quality quality;
    Category category;
    string spritePath;
    string malePath;
    string femalePath;
    Object sprite;
    Object maleSprite;
    Object femaleSprite;
    int maxStackSize;
    int strenght;
    int dexterity;
    int stamina;
    int magic;
    int attack;
    int defence;
    int health;
    int mana;

    static int currentId = 0;

    [MenuItem("Window/Create an item")]
    static void Init()
    {
        GetIDCount();
        // Get existing open window or if none, make a new one:
        ItemCreator window = (ItemCreator)EditorWindow.GetWindow(typeof(ItemCreator));
        window.Show();
    }


    /// <summary>
    /// TODO(Jukki) Auto select id
    /// </summary>
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Item ID:", EditorStyles.boldLabel);
        EditorGUILayout.LabelField(currentId.ToString(), EditorStyles.boldLabel, GUILayout.Width(220));
        //currentId = EditorGUILayout.IntField(currentId, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Item Name:", EditorStyles.boldLabel);
        itemName = EditorGUILayout.TextField(itemName, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Item Description:", EditorStyles.boldLabel);
        description = EditorGUILayout.TextArea(description, GUILayout.Width(220), GUILayout.Height(60));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Item Type:", EditorStyles.boldLabel);
        itemType = (ItemType)EditorGUILayout.EnumPopup(itemType, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Item Category:", EditorStyles.boldLabel);
        category = (Category)EditorGUILayout.EnumPopup(category, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Item Quality:", EditorStyles.boldLabel);
        quality = (Quality)EditorGUILayout.EnumPopup(quality, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Sprite:", EditorStyles.boldLabel);
        sprite = EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(220));
        spritePath = AssetDatabase.GetAssetPath(sprite).Replace("Assets/Resources/", "").Split('.')[0];
        GUILayout.EndHorizontal();

        if (category != Category.Consumable)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Sprite:", EditorStyles.boldLabel);
            maleSprite = EditorGUILayout.ObjectField(maleSprite, typeof(Sprite), false, GUILayout.Width(220));
            malePath = AssetDatabase.GetAssetPath(maleSprite).Replace("Assets/Resources/", "").Split('.')[0];
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Sprite:", EditorStyles.boldLabel);
            femaleSprite = EditorGUILayout.ObjectField(femaleSprite, typeof(Sprite), false, GUILayout.Width(220));
            femalePath = AssetDatabase.GetAssetPath(femaleSprite).Replace("Assets/Resources/", "").Split('.')[0];
            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Max Size:", EditorStyles.boldLabel);
        maxStackSize = EditorGUILayout.IntField(maxStackSize, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        if (category != Category.Consumable)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Strenght:", EditorStyles.boldLabel);
            strenght = EditorGUILayout.IntField(strenght, GUILayout.Width(220));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Dexterity:", EditorStyles.boldLabel);
            dexterity = EditorGUILayout.IntField(dexterity, GUILayout.Width(220));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Stamina:", EditorStyles.boldLabel);
            stamina = EditorGUILayout.IntField(stamina, GUILayout.Width(220));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Magic:", EditorStyles.boldLabel);
            magic = EditorGUILayout.IntField(magic, GUILayout.Width(220));
            GUILayout.EndHorizontal();
        }

        if (category == Category.Weapon)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Attack:", EditorStyles.boldLabel);
            attack = EditorGUILayout.IntField(attack, GUILayout.Width(220));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Defence:", EditorStyles.boldLabel);
            defence = EditorGUILayout.IntField(defence, GUILayout.Width(220));
            GUILayout.EndHorizontal();
        }

        if (category == Category.Consumable)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Health:", EditorStyles.boldLabel);
            health = EditorGUILayout.IntField(health, GUILayout.Width(220));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Mana:", EditorStyles.boldLabel);
            mana = EditorGUILayout.IntField(mana, GUILayout.Width(220));
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(25f);
        if (GUILayout.Button("Create Item"))
        {
            CreateItem();
            GetIDCount();
        }
        if (GUILayout.Button("Refresh ID"))
        {
            GetIDCount();
        }
    }

    public void CreateItem()
    {
        GetIDCount();
        ItemContainer itemContainer = new ItemContainer();

        System.Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumable) };

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);

        FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Open);

        itemContainer = (ItemContainer)serializer.Deserialize(fs);

        fs.Close();

        switch (category)
        {
            case Category.Equipment:
                itemContainer.Equipments.Add(new Equipment(currentId, itemName, description, itemType, quality, spritePath, malePath, femalePath, maxStackSize, strenght, dexterity, stamina, magic));
                break;
            case Category.Weapon:
                itemContainer.Weapons.Add(new Weapon(currentId, itemName, description, itemType, quality, spritePath, malePath, femalePath, maxStackSize, strenght, dexterity, stamina, magic, attack, defence));
                break;
            case Category.Consumable:
                itemContainer.Consumables.Add(new Consumable(currentId, itemName, description, itemType, quality, spritePath, maxStackSize, health, mana));
                break;
            default:
                break;
        }

        fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Create);
        serializer.Serialize(fs, itemContainer);
        fs.Close();
    }

    static void GetIDCount()
    {
        ItemContainer itemContainer = new ItemContainer();

        System.Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumable) };

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);

        FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Open);

        itemContainer = (ItemContainer)serializer.Deserialize(fs);

        fs.Close();


        int minId = 0;
        foreach (Item item in itemContainer.Consumables)
        {
            if (item.Id == minId)
            {
                minId = item.Id + 1;
            }
        }
        foreach (Item item in itemContainer.Equipments)
        {
            if (item.Id == minId)
            {
                minId = item.Id + 1;
            }
        }
        foreach (Item item in itemContainer.Weapons)
        {
            if (item.Id == minId)
            {
                minId = item.Id + 1;
            }
        }

        currentId = minId;
    }
}
