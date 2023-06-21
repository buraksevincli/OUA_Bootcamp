using GameFolders.Scripts.Abstracts.States;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.States.EnemyStates
{
    public class DeadState : IState
    {
        public void Tick()
        {
            
        }
        
        public void FixedTick()
        {
            
        }

        public void LateTick()
        {
            
        }

        public void OnEnter()
        {
            Debug.Log($"{nameof(DeadState)} {nameof(OnEnter)}");
        }
        
        public void OnExit()
        {
            Debug.Log($"{nameof(DeadState)} {nameof(OnExit)}");
        }
    }
}
