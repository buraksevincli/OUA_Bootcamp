using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine;
using UnityEngine.AI;

namespace GameFolders.Scripts.Concretes.Movements
{
    public class EnemyMover
    {
        private NavMeshAgent _navMeshAgent;
        
        public EnemyMover(EnemyController enemyController)
        {
            _navMeshAgent = enemyController.GetComponent<NavMeshAgent>();
        }

        public void MoveAction(Vector3 direction)
        {
            _navMeshAgent.SetDestination(direction);
        }
    }
}
