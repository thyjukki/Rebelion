using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour {

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
}
