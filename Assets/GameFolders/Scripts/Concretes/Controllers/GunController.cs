using System;
using System.Collections;
using UnityEngine;
using TMPro;

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

        [SerializeField] private TMP_Text text;

        [SerializeField] private Camera fpsCam;

        private Animator _animator;
        private AudioSource _audioSource;
        
        private bool _isShooting;
        private bool _isReloading;
        
        private float _nextTimeToFire;

        private void Awake()
        {
            _animator = transform.GetChild(0).GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (currentAmmo == 0)
            {
                currentAmmo = maxAmmo;
                text.text = currentAmmo.ToString();
            }
            else
            {
                currentAmmo = availableAmmo;
                text.text = currentAmmo.ToString();
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
                _isShooting = false;
                _audioSource.Stop();
                muzzleEffect.Stop();
                _animator.SetBool("isShooting", _isShooting);
            }

            if (currentAmmo <= 0)
            {
                _isShooting = false;
                _audioSource.Stop();
                muzzleEffect.Stop();
                _animator.SetBool("isShooting", _isShooting);
                StartCoroutine(Reload());
            }

            if (Input.GetButtonDown("Fire2"))
            {
                fpsCam.usePhysicalProperties = true;
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                fpsCam.usePhysicalProperties = false;
            }
        }

        private void Shoot()
        {
            if (currentAmmo > 0)
            {
                _isShooting = true;
                currentAmmo--;
                text.text = currentAmmo.ToString();
                _audioSource.Play();
                _animator.SetBool("isShooting", _isShooting);
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
            _isReloading = true;
            _animator.SetBool("isReloading", _isReloading);
            
            yield return new WaitForSeconds(3f);

            _isReloading = false;
            _animator.SetBool("isReloading", _isReloading);
            
            if (currentAmmo <= 0)
            {
                currentAmmo = maxAmmo;
                text.text = currentAmmo.ToString();
            }
            else if (currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;
                text.text = currentAmmo.ToString();
            }
        }
    }
}
