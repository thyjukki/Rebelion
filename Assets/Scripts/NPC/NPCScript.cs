using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCScript : MonoBehaviour {

    private HumanoidBuilder humanoidBuilder;

    private Dictionary<string, EquipmentSlot> slotByNames;

    private NPC npc;

    public NPC Npc
    {
        get { return npc; }
        set {
            npc = value;
            if (humanoidBuilder == null)
            {
                humanoidBuilder = GetComponent<HumanoidBuilder>();
            }
            if (npc == null)
            {
                humanoidBuilder.Female = false;
                humanoidBuilder.SkinName = string.Empty;
                humanoidBuilder.EyeName = string.Empty;
                humanoidBuilder.EarType = string.Empty;
                humanoidBuilder.NoseType = string.Empty;
            }
            else
            {
                humanoidBuilder.Female = npc.Female;
                humanoidBuilder.SkinName = npc.Skin;
                humanoidBuilder.EyeName = npc.EyeColor;
                humanoidBuilder.EarType = npc.Ears;
                humanoidBuilder.NoseType = npc.Nose;

                SetEquipments();
            }
        }
    }

	// Use this for initialization
	void Awake () {
        slotByNames = new Dictionary<string, EquipmentSlot>();

        foreach (EquipmentSlot slot in transform.GetComponentsInChildren<EquipmentSlot>())
        {
            slotByNames.Add(slot.name, slot);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void SetEquipments()
    {
        foreach (var equipment in Npc.Equipments)
        {
            string slotName = equipment.Key;
            int itemID = equipment.Value;

            if (slotByNames.ContainsKey(slotName))
            {
                ItemScript item = ItemScript.CreateItem(itemID);

                slotByNames[slotName].SetItem(item);
            }
        }
    }

    public bool HasDialog()
    {
        return npc.DialogID > 0;
    }
}
