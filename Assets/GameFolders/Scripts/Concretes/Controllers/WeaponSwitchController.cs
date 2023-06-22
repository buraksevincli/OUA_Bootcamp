using System.Collections;
using GameFolders.Scripts.Concretes.Managers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class WeaponSwitchController : MonoBehaviour
    {
        [SerializeField] private GameObject heavyGun;
        [SerializeField] private GameObject pistol;
        //[SerializeField] private GameObject sword;

        [SerializeField] private GameObject heavyGunImage;
        [SerializeField] private GameObject pistolImage;
        //[SerializeField] private GameObject swordImage;

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
                PistolSelect();
            }
            // else if (Input.GetKeyDown(KeyCode.Alpha3))
            // {
            //     SwordSelect();
            // }
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
            //sword.SetActive(false);
            pistol.SetActive(false);

            heavyGunImage.SetActive(true);

            yield return _wait;

            heavyGunImage.SetActive(false);
        }
        
        private void PistolSelect()
        {
            StartCoroutine(PistolSelected());
            
            pistol.TryGetComponent(out GunController gunController);
            
            gunController.CurrentClipAmmo += GameManager.Instance.PistolAmmo;

            GameManager.Instance.PistolAmmo = 0;
        }

        private IEnumerator PistolSelected()
        {
            heavyGun.SetActive(false);
            //sword.SetActive(false);
            pistol.SetActive(true);
            
            pistolImage.SetActive(true);

            yield return _wait;

            pistolImage.SetActive(false);
        }

        // private void SwordSelect()
        // {
        //     StartCoroutine(SwordSelected());
        // }
        //
        // private IEnumerator SwordSelected()
        // {
        //     heavyGun.SetActive(false);
        //     sword.SetActive(true);
        //     pistol.SetActive(false);
        //     
        //     swordImage.SetActive(true);
        //
        //     yield return _wait;
        //
        //     swordImage.SetActive(false);
        // }
    }
}
