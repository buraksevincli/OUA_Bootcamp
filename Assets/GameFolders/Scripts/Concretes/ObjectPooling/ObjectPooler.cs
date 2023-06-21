using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Utilities;
using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.ObjectPooling
{
    public class ObjectPooler : MonoSingleton<ObjectPooler>
    {
        [SerializeField] private EnemyController[] enemyPrefab;
        [SerializeField] private int poolSize;

        private readonly Queue<EnemyController> _interactPool = new Queue<EnemyController>();

        private void Start()
        {
            InitializePool(poolSize);
        }

        private void InitializePool(int poolLenght)
        {
            for (int j = 0; j < poolLenght; j++)
            {
                for (int i = 0; i < enemyPrefab.Length; i++)
                {
                    EnemyController enemyController = Instantiate(enemyPrefab[i]);
                    enemyController.gameObject.SetActive(false);
                    enemyController.transform.parent = transform;

                    _interactPool.Enqueue(enemyController);
                }
            }
        }

        public void SetPool(EnemyController enemyController)
        {
            enemyController.gameObject.SetActive(false);
            enemyController.transform.parent = this.transform;

            _interactPool.Enqueue(enemyController);
        }

        public EnemyController GetPool()
        {
            if (_interactPool.Count == 0)
            {
                InitializePool(10);
            }

            return _interactPool.Dequeue();
        }
    }
}