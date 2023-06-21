using GameFolders.Scripts.Concretes.Managers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class AmmoPackController : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;

        [SerializeField] private GameObject heavyGun;
        [SerializeField] private GameObject pistol;

        [SerializeField] private int ammo;

        private GunController _gunController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                switch (weaponType)
                {
                    case WeaponType.HeavyGun:
                        if (heavyGun.activeSelf)
                        {
                            _gunController = heavyGun.GetComponent<GunController>();
                            _gunController.CurrentClipAmmo += ammo;
                        }
                        else
                        {
                            GameManager.Instance.HeavyGunAmmo += ammo;
                        }
                        break;
                    case WeaponType.Pistol:
                        if (pistol.activeSelf)
                        {
                            _gunController = pistol.GetComponent<GunController>();
                            _gunController.CurrentClipAmmo += ammo;
                        }
                        else
                        {
                            GameManager.Instance.PistolAmmo += ammo;
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
        Pistol
    }
}
