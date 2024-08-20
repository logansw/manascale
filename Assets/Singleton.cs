using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : Component
{
    public static T Instance { get; private set; }

    protected virtual void Awake() {
        if (Instance == null) {
            Instance = FindObjectOfType<T>();
        } else {
            Destroy(gameObject);
        }
    }

    public abstract void Initialize();
}