using UnityEngine;
using System.Collections;

abstract public class Dialog {

    public int ID { get; set; }
    public int ActorID { get; set; }
    public string Name { get; set; }

    public Dialog()
    {
    }

    public Dialog(int id, int actorID, string name)
    {
        this.ID = id;
        this.ActorID = actorID;
        this.Name = name;
    }

    public abstract void Start();
}
