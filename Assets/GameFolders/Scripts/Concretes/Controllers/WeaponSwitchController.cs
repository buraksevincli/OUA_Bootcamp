using System;
using System.Collections;
using GameFolders.Scripts.Concretes.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class WeaponSwitchController : MonoBehaviour
    {
        [SerializeField] private GameObject heavyGun;
        [SerializeField] private GameObject rifle;
        [SerializeField] private GameObject miniGun;

        [SerializeField] private GameObject heavyGunImage;
        [SerializeField] private GameObject rifleImage;
        [SerializeField] private GameObject miniGunImage;

        private WaitForSeconds _wait;

        private void Awake()
        {
            _wait = new WaitForSeconds(2f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HeavyGunSelect();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                RifleSelect();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                MiniGunSelect();
            }
        }

        private void HeavyGunSelect()
        {
            StartCoroutine(HeavyGunSelected());

            heavyGun.TryGetComponent(out GunController gunController);
            
            gunController.CurrentClipAmmo += GameManager.Instance.HeavyGunAmmo;

            GameManager.Instance.HeavyGunAmmo = 0;

        }
        private IEnumerator HeavyGunSelected()
        {
            heavyGun.SetActive(true);
            rifle.SetActive(false);
            miniGun.SetActive(false);

            heavyGunImage.SetActive(true);

            yield return _wait;

            heavyGunImage.SetActive(false);
        }

        private void RifleSelect()
        {
            StartCoroutine(RifleSelected());
            
            rifle.TryGetComponent(out GunController gunController);
            
            gunController.CurrentClipAmmo += GameManager.Instance.RifleAmmo;

            GameManager.Instance.RifleAmmo = 0;
        }

        private IEnumerator RifleSelected()
        {
            heavyGun.SetActive(false);
            rifle.SetActive(true);
            miniGun.SetActive(false);
            
            rifleImage.SetActive(true);

            yield return _wait;

            rifleImage.SetActive(false);
        }

        private void MiniGunSelect()
        {
            StartCoroutine(MiniGunSelected());
            
            miniGun.TryGetComponent(out GunController gunController);
            
            gunController.CurrentClipAmmo += GameManager.Instance.MiniGunAmmo;

            GameManager.Instance.MiniGunAmmo = 0;
        }

        private IEnumerator MiniGunSelected()
        {
            heavyGun.SetActive(false);
            rifle.SetActive(false);
            miniGun.SetActive(true);
            
            miniGunImage.SetActive(true);

            yield return _wait;

            miniGunImage.SetActive(false);
        }
    }
}
