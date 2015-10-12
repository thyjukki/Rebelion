using UnityEngine;
using System.Collections;

public interface IState
{
    IState NextGameState { get; }

    IEnumerator DoWork();

    void Start();

    void Cleanup();
}
