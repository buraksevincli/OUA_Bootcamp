using System;
using GameFolders.Scripts.Concretes.Managers;
using Unity.VisualScripting;
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

        private GunController _gunController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (heavyGun.activeSelf)
                {
                    _gunController = heavyGun.GetComponent<GunController>();
                }
                else if (rifle.activeSelf)
                {
                    _gunController = rifle.GetComponent<GunController>();
                }
                else if (miniGun.activeSelf)
                {
                    _gunController = miniGun.GetComponent<GunController>();
                }
                
                switch (weaponType)
                {
                    case WeaponType.HeavyGun:
                        if (heavyGun.activeSelf)
                        {
                            _gunController.CurrentClipAmmo += ammo;
                        }
                        else
                        {
                            GameManager.Instance.HeavyGunAmmo += ammo;
                        }
                        break;
                    case WeaponType.Rifle:
                        if (rifle.activeSelf)
                        {
                            _gunController.CurrentClipAmmo += ammo;
                        }
                        else
                        {
                            GameManager.Instance.RifleAmmo += ammo;
                        }
                        break;
                    case WeaponType.MiniGun:
                        if (miniGun.activeSelf)
                        {
                            _gunController.CurrentClipAmmo += ammo;
                        }
                        else
                        {
                            GameManager.Instance.MiniGunAmmo += ammo;
                        }
                        break;
                }
                Destroy(this.gameObject);
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
