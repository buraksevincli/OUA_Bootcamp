using GameFolders.Scripts.Abstracts.States;
using GameFolders.Scripts.Concretes.Movements;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.States.EnemyStates
{
    public class ChaseState : IState
    {
        private EnemyMover _enemyMover;
        private Transform _player;
        
        public ChaseState(EnemyMover enemyMover, Transform player)
        {
            _enemyMover = enemyMover;
            _player = player;
        }
        
        public void Tick()
        {
            _enemyMover.MoveAction(_player.position);
        }
        
        public void FixedTick()
        {
            
        }

        public void LateTick()
        {
            //move animation yaz
        }

        public void OnEnter()
        {
            Debug.Log($"{nameof(ChaseState)} {nameof(OnEnter)}");
        }
        
        public void OnExit()
        {
            Debug.Log($"{nameof(ChaseState)} {nameof(OnExit)}");
        }
    }
}
