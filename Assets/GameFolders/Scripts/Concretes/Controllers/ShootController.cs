using System;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private Transform weapon;
        [SerializeField] private GameObject ammo;
        [SerializeField] private float ammoSpeed;

        private Rigidbody _ammoRigidbody;
        private Vector3 _direction;

        private void Awake()
        {
            _ammoRigidbody = ammo.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _direction = hit.point - weapon.position;

                Debug.DrawRay(weapon.position, hit.point, Color.red);

                Debug.Log(_direction);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(ammo, weapon.position, Quaternion.identity);

                _ammoRigidbody.velocity = _direction.normalized * ammoSpeed;
            }
        }

        private void Fire()
        {
            Instantiate(ammo, weapon.position, Quaternion.identity);

            _ammoRigidbody.velocity = _direction.normalized * ammoSpeed;
        }
    }
}
