using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Utilities;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public int HeavyGunAmmo { get; set; }
        public int RifleAmmo { get; set; }
        public int MiniGunAmmo { get; set; }
    }
}
