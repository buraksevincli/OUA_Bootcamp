using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFolders.Scripts.Abstracts.Utilities
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static volatile T instance = null;

        public static T Instance => instance;
        
        [SerializeField] private bool dontDestroyOnLoad = false;

        protected virtual void Awake()
        {
            Singleton();
        }

        private void Singleton()
        {
            if (dontDestroyOnLoad)
            {
                if (instance == null)
                {
                    instance = this as T;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (instance == null)
                {
                    instance = this as T;
                }
            }
        }
    }
}

