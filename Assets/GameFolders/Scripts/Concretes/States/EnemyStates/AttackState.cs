using GameFolders.Scripts.Abstracts.States;
using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.States.EnemyStates
{
    public class AttackState : IState
    {
        private EnemyController _enemyController;
        private Transform _player;
        private int _damage;
        
        public AttackState(EnemyController enemyController, Transform player, int damage)
        {
            _enemyController = enemyController;
            _player = player;
            _damage = damage;
        }
        
        public void Tick()
        {
            _enemyController.transform.LookAt(_player);
            _enemyController.transform.eulerAngles = new Vector3(0f, _enemyController.transform.eulerAngles.y, 0f);
        }
        
        public void FixedTick()
        {
            PlayerHealthController.Instance.TakeDamagePlayer(_damage);
        }

        public void LateTick()
        {
            //animasyon işlemini true yap
        }
        
        public void OnEnter()
        {
            Debug.Log($"{nameof(AttackState)} {nameof(OnEnter)}");
        }

        public void OnExit()
        {
            //animasyon işlemini false yap
            Debug.Log($"{nameof(AttackState)} {nameof(OnExit)}");
        }
    }
}
