using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Xml.Serialization;
using System.IO;

public class InventoryManager : MonoBehaviour {

    private ItemContainer itemContainer = new ItemContainer();

    public ItemContainer ItemContainer
    {
        get { return itemContainer; }
        set { itemContainer = value; }
    }

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

    /// <summary>
    /// Object that is shows which item is being moved
    /// </summary>
    private GameObject hoverObject;

    public GameObject HoverObject
    {
        get { return hoverObject; }
        set { hoverObject = value; }
    }

    public GameObject toolTipObject;

    public Text sizeTextObject;

    public Text visualTextObject;

    public GameObject selectStackSize;

    public Text stackText;

    private int splitAmount;

    public int SplitAmount
    {
        get { return splitAmount; }
        set { splitAmount = value; }
    }
    private int maxStackCount;

    public int MaxStackCount
    {
        get { return maxStackCount; }
        set { maxStackCount = value; }
    }
    private Slot movingSlot;

    public Slot MovingSlot
    {
        get { return movingSlot; }
        set { movingSlot = value; }
    }

    public Canvas canvas;

    public EventSystem eventSystem;

    private GameObject clicked;

    public GameObject Clicked
    {
        get { return clicked; }
        set { clicked = value; }
    }

    public GameObject itemObject;

    public void Awake()
    {
        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumable) };

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);
        TextReader textReader = new StreamReader(Application.streamingAssetsPath + "/Items.xml");

        itemContainer = (ItemContainer)serializer.Deserialize(textReader);
        textReader.Close();
    }

    public void Start()
    {


        movingSlot = GameObject.Find("MovingSlot").GetComponent<Slot>();
    }

    public void SetStackInfo(int maxStackCount)
    {
        selectStackSize.SetActive(true);
        Instance.SplitAmount = 0;
        Instance.MaxStackCount = maxStackCount;
        Instance.stackText.text = InventoryManager.Instance.SplitAmount.ToString();
    }
}
