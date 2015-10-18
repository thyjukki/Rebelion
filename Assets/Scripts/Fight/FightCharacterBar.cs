using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightCharacterBar : MonoBehaviour
{


    public GameObject CharacterPrefab;

    private List<FightCharacterTab> CharacterTabs;
    // Use this for initialization
    void Start()
    {
        CharacterTabs = new List<FightCharacterTab>();
        if (!FightManager.InFight)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetCharacters(List<Fighter> fighters)
    {
        if (CharacterTabs == null)
        {
            CharacterTabs = new List<FightCharacterTab>();
        }
        else
        {
            foreach (var item in CharacterTabs)
            {
                Destroy(item);
            }
            CharacterTabs.Clear();
        }
        foreach (Fighter fighter in fighters)
        {
            GameObject cTabObject = GameObject.Instantiate(CharacterPrefab);
            cTabObject.transform.SetParent(this.transform);
            FightCharacterTab cTab = cTabObject.GetComponent<FightCharacterTab>();

            cTab.Fighter = fighter;
        }

    }
}
