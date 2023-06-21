using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.ObjectPooling
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float maxSpawnTime;
        
        private float _maxSpawnTime;
        private float _currentSpawnTime = 0f;

        private void Start()
        {
            GetRandomMaxTime();
        }

        private void Update()
        {
            _currentSpawnTime += Time.deltaTime;

            if (_currentSpawnTime > _maxSpawnTime)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            EnemyController interactObject = ObjectPooler.Instance.GetPool();

            interactObject.transform.parent = this.transform;
            interactObject.transform.position = this.transform.position;
            interactObject.gameObject.SetActive(true);

            _currentSpawnTime = 0f;
            GetRandomMaxTime();
        }

        private void GetRandomMaxTime()
        {
            _maxSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
