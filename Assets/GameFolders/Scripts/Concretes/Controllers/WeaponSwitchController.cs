using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class WeaponSwitchController : MonoBehaviour
    {
        [SerializeField] private GameObject heavyGun;
        [SerializeField] private GameObject rifle;
        [SerializeField] private GameObject miniGun;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                heavyGun.SetActive(true);
                rifle.SetActive(false);
                miniGun.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                heavyGun.SetActive(false);
                rifle.SetActive(true);
                miniGun.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                heavyGun.SetActive(false);
                rifle.SetActive(false);
                miniGun.SetActive(true);
            }
        }
    }
}
