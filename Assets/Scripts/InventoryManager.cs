using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Xml.Serialization;
using System.IO;

public class InventoryManager : MonoBehaviour {

    private ItemContainer itemContainer = new ItemContainer();

    private static InventoryManager instance;

    public static InventoryManager Instance
    {
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
            }

            return InventoryManager.instance; }
    }

    /// <summary>
    /// A prefab of the slot
    /// </summary>
    public GameObject slotPrefab;

    /// <summary>
    /// A prefb used for instantiating the hoverObject
    /// </summary>
    public GameObject iconPrefab;

    private GameObject hoverObject;

    public GameObject HoverObject
    {
        get { return hoverObject; }
        set { hoverObject = value; }
    }

    public GameObject toolTipObject;

    public Text sizeTextObject;

    public Text visualTextObject;

    public Canvas canvas;

    public EventSystem eventSystem;

    private Slot from, to;

    public Slot To
    {
        get { return to; }
        set { to = value; }
    }

    public Slot From
    {
        get { return from; }
        set { from = value; }
    }

    public void Start()
    {
        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumable) };

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);
        TextReader textReader = new StreamReader(Application.streamingAssetsPath + "/Items.xml");

        itemContainer = (ItemContainer)serializer.Deserialize(textReader);
        textReader.Close();
    }
}
