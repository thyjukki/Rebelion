using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Action : Dialog
{

    public List<KeyValuePair<string, int>> Actions { set; get; }

    public Action() : base()
    {
    }

    public Action(int id, int actorID, string name, List<KeyValuePair<string, int>> actions)
        : base(id, actorID, name)
    {
        this.Actions = actions;
    }

    public override void Start()
    {
        ActionsElement actionsElement = DialogManager.Instance.actionsElement;

        actionsElement.SetDialog(this);
    }
}
