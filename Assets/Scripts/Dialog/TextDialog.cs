using UnityEngine;
using System.Collections;

public class TextDialog : Dialog
{
    public int NextID { get; set; }
    public string Text { get; set; }

    public TextDialog() : base()
    {
    }

    public TextDialog(int id, int nextID, int actorID, string name, string text)
        : base(id, actorID, name)
    {
        this.NextID = nextID;
        this.Text = text;
    }

    public override void Start()
    {
        DialogElement dialogElement = DialogManager.Instance.dialogElement;

        dialogElement.SetDialog(this);
    }
}