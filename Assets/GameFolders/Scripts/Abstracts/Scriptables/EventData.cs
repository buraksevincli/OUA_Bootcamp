using System;
using UnityEngine;

namespace GameFolders.Scripts.Abstracts.Scriptables
{
    [CreateAssetMenu(fileName = "Event Data", menuName = "Data/Event Data")]
    public class EventData : ScriptableObject
    {
        public Action GameOver { get; set; }
    }
}
