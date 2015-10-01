using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogElement : MonoBehaviour {

    public Text textField;


    private TextDialog dialog;
    public void SetDialog(TextDialog newDialog)
    {
        this.gameObject.SetActive(true);
        this.dialog = newDialog;
        textField.text = this.dialog.Text;
    }

    public void Advance()
    {
        this.gameObject.SetActive(false);
        DialogManager.Instance.Advance(this.dialog.NextID);
    }
}
