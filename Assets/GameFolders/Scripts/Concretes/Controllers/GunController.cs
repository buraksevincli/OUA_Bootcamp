using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float impactForce;
        [SerializeField] private float fireRate;
        
        
        [SerializeField] private int startAmmo;
        [SerializeField] private int currentAmmo;
        [SerializeField] private int availableAmmo;
        
        [SerializeField] private int startClipAmmo;
        [SerializeField] private int availableClipAmmo;
        
        private int currentClipAmmo = 1;
        public int CurrentClipAmmo
        {
            get => currentClipAmmo;
            set => currentClipAmmo = value;
        }


        [SerializeField] private GameObject impactEffectWithHole;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private ParticleSystem muzzleEffect;

        [SerializeField] private TMP_Text currentAmmoText;
        [SerializeField] private TMP_Text clipAmmoText;

        [SerializeField] private Camera fpsCam;

        private Animator _animator;
        private AudioSource _audioSource;
        private WaitForSeconds _reloadTime;
        private Coroutine _reloadCoroutine;
        
        private bool _isShooting;
        private bool _isReloading;
        
        private float _nextTimeToFire;

        private void Awake()
        {
            _animator = transform.GetChild(0).GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();

            _reloadTime = new WaitForSeconds(3f);
        }

        private void OnEnable()
        {
            SetCurrentAmmo();
            SetCurrentClipAmmo();

            if (currentAmmo == 0 && currentClipAmmo > 0)
            {
                StartCoroutine(Reload());
            }
        }

        private void OnDisable()
        {
            availableAmmo = currentAmmo;

            availableClipAmmo = currentClipAmmo;
        }

        private void Update()
        {
            _animator.SetBool("isShooting", _isShooting);
            
            SetTextBullets();
            
            if (currentClipAmmo == 0 && currentAmmo == 0)
            {
                _isShooting = false;
                _audioSource.Stop();
                muzzleEffect.Stop();
            }

            if (currentAmmo <= 0 && currentClipAmmo > 0)
            {
                _isShooting = false;

                _audioSource.Stop();
                muzzleEffect.Stop();

                StartReloading();
            }

            if (Input.GetKeyDown(KeyCode.R) && currentClipAmmo > 0)
            {
                _isShooting = false;

                _audioSource.Stop();
                muzzleEffect.Stop();

                StartReloading();
            }
                
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextTimeToFire && currentAmmo > 0)
            {
                muzzleEffect.Play();
            }
            else if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire && currentAmmo > 0)
            {
                _isShooting = true;
                _nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                _isShooting = false;
                _audioSource.Stop();
                muzzleEffect.Stop();
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
            if (currentAmmo > 0 && _isShooting)
            {
                currentAmmo--;
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
            
            yield return _reloadTime;

            _isReloading = false;
            _animator.SetBool("isReloading", _isReloading);
            
            AmmoLimitationAndSet();
        }
        
        private void StartReloading()
        {
            if (!_isReloading)
            {
                StartCoroutine(Reload());
            }
        }

        private void SetCurrentAmmo()
        {
            if (currentAmmo == 1)
            {
                currentAmmo = startAmmo;
                currentAmmoText.text = currentAmmo.ToString();
            }
            else
            {
                currentAmmo = availableAmmo;
                currentAmmoText.text = currentAmmo.ToString();
            }
        }

        private void SetCurrentClipAmmo()
        {
            if (currentClipAmmo == 1)
            {
                currentClipAmmo = startClipAmmo;
                clipAmmoText.text = currentClipAmmo.ToString();
            }
            else
            {
                currentClipAmmo = availableClipAmmo;
                clipAmmoText.text = currentClipAmmo.ToString();
            }
        }

        private void AmmoLimitationAndSet()
        {
            if (currentClipAmmo >= startAmmo)
            {
                if (currentAmmo <= 0 && currentClipAmmo > 0)
                {
                    currentAmmo = startAmmo;
                    currentClipAmmo -= startAmmo;
                }
                else if (currentAmmo > startAmmo && currentClipAmmo > 0)
                {
                    currentAmmo = startAmmo;
                    currentClipAmmo -= startAmmo;
                }
                else if (currentAmmo > 0 && currentClipAmmo > 0)
                {
                    currentClipAmmo -= (startAmmo - currentAmmo);
                    currentAmmo = startAmmo;
                }
            }
            else
            {
                if (currentAmmo <= 0 && currentClipAmmo > 0)
                {
                    currentAmmo = currentClipAmmo;
                    currentClipAmmo = 0;
                }
                else if (currentAmmo > startAmmo && currentClipAmmo > 0)
                {
                    currentAmmo = currentClipAmmo;
                    currentClipAmmo = 0;
                }
                else if (currentAmmo > 0 && currentClipAmmo > 0)
                {
                    if (currentAmmo + currentClipAmmo > startAmmo)
                    {
                        currentClipAmmo = (currentAmmo + currentClipAmmo) - startAmmo;
                        currentAmmo = startAmmo;
                    }
                    else if (currentAmmo + currentClipAmmo <= startAmmo)
                    {
                        currentAmmo = currentAmmo + currentClipAmmo;
                        currentClipAmmo = 0;
                    }
                }
            }
        }

        private void SetTextBullets()
        {
            currentAmmoText.text = currentAmmo.ToString();
            clipAmmoText.text = currentClipAmmo.ToString();
        }
    }
}
