using System.Collections;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class SwordController : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float impactForce;
        [SerializeField] private float fireRate;
        
        [SerializeField] private GameObject impactEffect;

        [SerializeField] private Camera fpsCam;
        [SerializeField] private AudioClip[] audioClips;

        private Animator _animator;
        private WaitForSeconds _swordShot;
        //private AudioSource _audioSource;
        
        private bool _isShooting;
        private bool _isReloading;
        
        private float _nextTimeToFire;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _swordShot = new WaitForSeconds(.5f);
            //_audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            _animator.SetBool("isShooting", _isShooting);
            
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextTimeToFire)
            {
                _isShooting = true;
                _nextTimeToFire = Time.time + 1f / fireRate;
                StartCoroutine(SwordShot());
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                _isShooting = false;
            }
        }
        
        #region ShootControllers

        private void Shoot()
        {
            RaycastHit hit;
            
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                if (hit.transform.TryGetComponent(out TargetController target))
                {
                    target.TakeDamage(damage);
                    Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
        }

        private IEnumerator SwordShot()
        {
            yield return _swordShot;
            Shoot();
        }
        #endregion
    }
}
