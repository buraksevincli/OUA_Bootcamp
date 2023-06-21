using System;
using GameFolders.Scripts.Concretes.Managers;
using GameFolders.Scripts.Concretes.ObjectPooling;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class TargetController : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private GameObject explodeEffect;

        private EnemyController _enemyController;

        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
        }

        public void TakeDamage(float amount)
        {
            health -= amount;

            if (health <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            Instantiate(explodeEffect, transform.position, transform.rotation);

            GameManager.Instance.Score++;

            ObjectPooler.Instance.SetPool(_enemyController);
        }
    }
}
