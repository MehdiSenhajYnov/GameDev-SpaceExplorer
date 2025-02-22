using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }



    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Second instance of " + typeof(T) + " created. Automatic self-destruct triggered.");
            Destroy(this.gameObject);
        }
        _instance = this as T;

        Init();
    }


    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }


    public virtual void Init() { }
}