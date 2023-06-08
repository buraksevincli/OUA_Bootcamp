using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFolders.Scripts.Abstracts.Utilities
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static volatile T _instance = null;

        public static T Instance => _instance;
        
        [SerializeField] private bool dontDestroyOnLoad = false;

        protected virtual void Awake()
        {
            Singleton();
        }

        private void Singleton()
        {
            if (dontDestroyOnLoad)
            {
                if (_instance == null)
                {
                    _instance = this as T;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (_instance == null)
                {
                    _instance = this as T;
                }
            }
        }
    }
}

