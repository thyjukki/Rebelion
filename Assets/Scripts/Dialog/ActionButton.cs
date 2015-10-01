using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    private int ID;

    public void SetButton(int id, string text)
    {
        this.ID = id;

        GetComponent<Text>().text = text;
    }


    public void Advance()
    {
        this.transform.parent.gameObject.SetActive(false);
        DialogManager.Instance.Advance(ID);
    }
}
