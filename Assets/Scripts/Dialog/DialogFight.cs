using UnityEngine;
using System.Collections;

public class DialogFight : Dialog
{
    public int FightID { get; set; }

    public DialogFight() : base()
    {
    }

    public DialogFight(int id, int fightID, int actorID, string name)
        : base(id, actorID, name)
    {
        this.FightID = fightID;
    }

    public override void Start()
    {
        DialogManager.Instance.CloseDialog();
    }
}
