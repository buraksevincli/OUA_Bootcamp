using System;
using GameFolders.Scripts.Concretes.Movements;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        private Transform _player;
        private EnemyMover _enemyMover;

        private void Awake()
        {
            _enemyMover = new EnemyMover(this);
            _player = Camera.main.transform;
        }

        private void Update()
        {
            _enemyMover.MoveAction(_player.transform.position);
        }
    }
}
