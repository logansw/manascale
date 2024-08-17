using UnityEngine;
using System;

public abstract class State : MonoBehaviour
{
    public Action e_OnEnter;
    public Action e_OnExit;
    public StateType StateType { get; protected set; }
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}