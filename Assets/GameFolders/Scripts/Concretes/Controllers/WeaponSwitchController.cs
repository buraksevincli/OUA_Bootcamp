using System;
using System.Collections;
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
                StartCoroutine(HeavyGunSelected());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(RifleSelected());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(MiniGunSelected());
            }
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

        private IEnumerator RifleSelected()
        {
            heavyGun.SetActive(false);
            rifle.SetActive(true);
            miniGun.SetActive(false);
            
            rifleImage.SetActive(true);

            yield return _wait;

            rifleImage.SetActive(false);
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
