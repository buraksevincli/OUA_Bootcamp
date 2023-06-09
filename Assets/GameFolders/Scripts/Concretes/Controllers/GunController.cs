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
        [SerializeField] private GameObject impactEffectWithHole;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private ParticleSystem muzzleEffect;

        [SerializeField] private Camera fpsCam;

        private float _nextTimeToFire;

        private void Update()
        {
            

            if (Input.GetButtonDown("Fire1") && Time.time >= _nextTimeToFire)
            {
                muzzleEffect.Play();
            }
            else if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                muzzleEffect.Stop();
            }
        }

        private void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                TargetController target = hit.transform.GetComponent<TargetController>();

                if (target != null)
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
    }
}
