using UnityEngine;

namespace AVG
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        [SerializeField] private bool dontDestroyOnLoad = true;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject($"{typeof(T).Name}");
                        instance = go.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = (T)this;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            Init();
        }

        protected virtual void Init() { }
    }
}
