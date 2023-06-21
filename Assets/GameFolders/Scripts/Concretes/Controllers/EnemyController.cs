using System;
using GameFolders.Scripts.Concretes.Movements;
using GameFolders.Scripts.Concretes.States;
using GameFolders.Scripts.Concretes.States.EnemyStates;
using UnityEngine;
using UnityEngine.AI;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private int takeDamageToPlayer;
        
        private Transform _player;
        private EnemyMover _enemyMover;
        private StateMachine _stateMachine;
        private NavMeshAgent _navMeshAgent;
        private TargetController _targetController;

        public bool CanAttack => Vector3.Distance(_player.position,
            this.transform.position) <= _navMeshAgent.stoppingDistance && _navMeshAgent.velocity == Vector3.zero;

        private void Awake()
        {
            _enemyMover = new EnemyMover(this);
            _stateMachine = new StateMachine();

            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetController = GetComponent<TargetController>();
            
            _player = Camera.main.transform;
        }

        private void Start()
        {
            AttackState attackState = new AttackState(this, _player, takeDamageToPlayer);
            ChaseState chaseState = new ChaseState(_enemyMover, _player);
            DeadState deadState = new DeadState();
            
            _stateMachine.AddState(chaseState, attackState, () => CanAttack);
            _stateMachine.AddState(attackState, chaseState, () => !CanAttack);
            _stateMachine.AddAnyState(deadState, () => _targetController.IsDead);

            _stateMachine.SetState(chaseState);
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedTick();
        }

        private void Update()
        {
            Debug.Log(CanAttack);

            _stateMachine.Tick();
        }

        private void LateUpdate()
        {
            _stateMachine.LateTick();
        }
    }
}
