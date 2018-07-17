using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                    Debug.LogError("No instance of type " + typeof(T).ToString() + " exists!");

                else if (!IsInitialized)
                {
                    instance.Initialize();
                    IsInitialized = true;
                }
            }
            return instance;
        }
    }

    public static bool isTemporaryInstance { private set; get; }

    public static bool IsInitialized
    {
        get;
        private set;
    }

    public bool dontDestroyOnLoad;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Debug.LogError("Another instance of " + GetType() + " already exists! Destroying self...");
            DestroyImmediate(this);
            return;
        }
        if (!IsInitialized)
        {
#if !UNITY_EDITOR
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
#endif

            instance.Initialize();

            IsInitialized = true;
        }
    }

    protected virtual void Initialize() { }

    private void OnApplicationQuit()
    {
        instance = null;
    }

	private void OnDestroy ()
	{
		instance = null;
		IsInitialized = false;
	}
}
