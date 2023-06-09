using System;
using System.Collections;
using UnityEditor.Search;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float impactForce;
        [SerializeField] private float fireRate;
        [SerializeField] private float maxAmmo;
        [SerializeField] private float currentAmmo;
        [SerializeField] private float availableAmmo;
        [SerializeField] private GameObject impactEffectWithHole;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private ParticleSystem muzzleEffect;

        [SerializeField] private Camera fpsCam;

        private float _nextTimeToFire;

        private void OnEnable()
        {
            if (currentAmmo == 0)
            {
                currentAmmo = maxAmmo; 
            }
            else
            {
                currentAmmo = availableAmmo;
            }
            
        }

        private void OnDisable()
        {
            availableAmmo = currentAmmo;
        }

        private void Update()
        {
            

            if (Input.GetButtonDown("Fire1") && Time.time >= _nextTimeToFire && currentAmmo > 0)
            {
                muzzleEffect.Play();
            }
            else if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire && currentAmmo > 0)
            {
                _nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                muzzleEffect.Stop();
            }

            if (currentAmmo <= 0)
            {
                muzzleEffect.Stop();
                StartCoroutine(Reload());
            }
        }

        private void Shoot()
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
            }
            else
            {
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                //TargetController target = hit.transform.GetComponent<TargetController>();
                
                if (hit.transform.TryGetComponent(out TargetController target))
                {
                    target.TakeDamage(damage);
                    Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
                else
                {
                    Instantiate(impactEffectWithHole, hit.point, Quaternion.LookRotation(hit.normal));
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(2f);
            
            if (currentAmmo <= 0)
            {
                currentAmmo = maxAmmo;
            }
            else if (currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;
            }
        }
    }
}
