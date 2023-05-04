using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this as T;
    }
}