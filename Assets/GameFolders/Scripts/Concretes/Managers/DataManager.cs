using GameFolders.Scripts.Abstracts.Scriptables;
using GameFolders.Scripts.Abstracts.Utilities;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Managers
{
    public class DataManager : MonoSingleton<DataManager>
    {
        [SerializeField] private EventData eventData;
        
        public EventData EventData => eventData;
    }
}
