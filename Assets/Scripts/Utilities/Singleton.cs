using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {   
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    GameObject obj = new GameObject($"{typeof(T).Name}");
                    obj.AddComponent<Singleton<T>>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    
    protected virtual void OnEnable() { }
    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
    protected virtual void LateUpdate() { }
    protected virtual void OnDisable() { }
}
