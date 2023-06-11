using System;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class AmmoPackController : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;

        [SerializeField] private GameObject heavyGun;
        [SerializeField] private GameObject rifle;
        [SerializeField] private GameObject miniGun;

        [SerializeField] private int ammo;

        private void OnTriggerEnter(Collider other)
        {
            switch (weaponType)
            {
                case WeaponType.HeavyGun:
                    if (heavyGun.activeSelf)
                    {
                        // işlemler yapılacak.
                    }
                    break;
                case WeaponType.Rifle:
                    if (rifle.activeSelf)
                    {
                        //
                    }
                    break;
                case WeaponType.MiniGun:
                    if (miniGun.activeSelf)
                    {
                        //
                    }
                    break;
            }
        }
    }

    public enum WeaponType
    {
        HeavyGun,
        Rifle,
        MiniGun
    }
}
