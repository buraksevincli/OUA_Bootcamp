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
        
        
        [SerializeField] private int startAmmo;
        [SerializeField] private int startClipAmmo;
        
        private int _availableAmmo;
        private int _availableClipAmmo;
        
        private int _currentAmmo = 1;
        private int _currentClipAmmo = 1;
        public int CurrentClipAmmo
        {
            get => _currentClipAmmo;
            set => _currentClipAmmo = value;
        }


        [SerializeField] private GameObject impactEffectWithHole;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private ParticleSystem muzzleEffect;

        [SerializeField] private TMP_Text currentAmmoText;
        [SerializeField] private TMP_Text clipAmmoText;

        [SerializeField] private Camera fpsCam;
        [SerializeField] private AudioClip[] audioClips;

        private Animator _animator;
        //private AudioSource _audioSource;
        private WaitForSeconds _reloadTime;
        
        private bool _isShooting;
        private bool _isReloading;
        
        private float _nextTimeToFire;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            //_audioSource = GetComponent<AudioSource>();

            _reloadTime = new WaitForSeconds(3f);
        }

        private void OnEnable()
        {
            SetCurrentAmmo();
            SetCurrentClipAmmo();

            if (_currentAmmo == 0 && _currentClipAmmo > 0)
            {
                StartCoroutine(Reload());
            }
        }

        private void OnDisable()
        {
            _availableAmmo = _currentAmmo;

            _availableClipAmmo = _currentClipAmmo;
        }

        private void Update()
        {
            _animator.SetBool("isShooting", _isShooting);
            
            SetTextBullets();
            
            if (_currentClipAmmo == 0 && _currentAmmo == 0)
            {
                _isShooting = false;
                //_audioSource.Stop();
                muzzleEffect.Stop();
            }

            if (_currentAmmo <= 0 && _currentClipAmmo > 0)
            {
                _isShooting = false;

                //_audioSource.Stop();
                muzzleEffect.Stop();

                StartReloading();
            }

            if (Input.GetKeyDown(KeyCode.R) && _currentClipAmmo > 0 && _currentAmmo < startAmmo)
            {
                _isShooting = false;

                //_audioSource.Stop();
                muzzleEffect.Stop();

                StartReloading();
            }
                
            if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire && _currentAmmo > 0)
            {
                _isShooting = true;
                _nextTimeToFire = Time.time + 1f / fireRate;

                Shoot();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                _isShooting = false;
                //_audioSource.Stop();
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

        

        #region ShootControllers

        private void Shoot()
        {
            muzzleEffect.Play();

            if (_currentAmmo > 0 && _isShooting)
            {
                _currentAmmo--;
                //_audioSource.clip = audioClips[0];
                //_audioSource.Play();
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

        #endregion

        #region ReloadControllers

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

        #endregion

        #region AmmoControllers

        private void SetCurrentAmmo()
        {
            if (_currentAmmo == 1)
            {
                _currentAmmo = startAmmo;
                currentAmmoText.text = _currentAmmo.ToString();
            }
            else
            {
                _currentAmmo = _availableAmmo;
                currentAmmoText.text = _currentAmmo.ToString();
            }
        }

        private void SetCurrentClipAmmo()
        {
            if (_currentClipAmmo == 1)
            {
                _currentClipAmmo = startClipAmmo;
                clipAmmoText.text = _currentClipAmmo.ToString();
            }
            else
            {
                _currentClipAmmo = _availableClipAmmo;
                clipAmmoText.text = _currentClipAmmo.ToString();
            }
        }

        private void AmmoLimitationAndSet()
        {
            if (_currentClipAmmo >= startAmmo)
            {
                if (_currentAmmo <= 0 && _currentClipAmmo > 0)
                {
                    _currentAmmo = startAmmo;
                    _currentClipAmmo -= startAmmo;
                }
                else if (_currentAmmo > startAmmo && _currentClipAmmo > 0)
                {
                    _currentAmmo = startAmmo;
                    _currentClipAmmo -= startAmmo;
                }
                else if (_currentAmmo > 0 && _currentClipAmmo > 0)
                {
                    _currentClipAmmo -= (startAmmo - _currentAmmo);
                    _currentAmmo = startAmmo;
                }
            }
            else
            {
                if (_currentAmmo <= 0 && _currentClipAmmo > 0)
                {
                    _currentAmmo = _currentClipAmmo;
                    _currentClipAmmo = 0;
                }
                else if (_currentAmmo > startAmmo && _currentClipAmmo > 0)
                {
                    _currentAmmo = _currentClipAmmo;
                    _currentClipAmmo = 0;
                }
                else if (_currentAmmo > 0 && _currentClipAmmo > 0)
                {
                    if (_currentAmmo + _currentClipAmmo > startAmmo)
                    {
                        _currentClipAmmo = (_currentAmmo + _currentClipAmmo) - startAmmo;
                        _currentAmmo = startAmmo;
                    }
                    else if (_currentAmmo + _currentClipAmmo <= startAmmo)
                    {
                        _currentAmmo = _currentAmmo + _currentClipAmmo;
                        _currentClipAmmo = 0;
                    }
                }
            }
        }

        private void SetTextBullets()
        {
            currentAmmoText.text = _currentAmmo.ToString();
            clipAmmoText.text = _currentClipAmmo.ToString();
        }

        #endregion
        
    }
}
