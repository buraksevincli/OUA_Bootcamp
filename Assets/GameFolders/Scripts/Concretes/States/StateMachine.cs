using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.States;

namespace GameFolders.Scripts.Concretes.States
{
    public class StateMachine
    {
        private List<StateTransformer> _stateTransformers = new List<StateTransformer>();
        private List<StateTransformer> _anyStateTransformers = new List<StateTransformer>();

        private IState _currentState;

        public void SetState(IState state)
        {
            if (_currentState == state) return;

            _currentState?.OnExit();

            _currentState = state;

            _currentState.OnEnter();
        }

        public void Tick()
        {
            StateTransformer stateTransformer = CheckForTransformer();

            if (stateTransformer != null)
            {
                SetState(stateTransformer.To);
            }
            
            _currentState.Tick();
        }
        
        public void FixedTick()
        {
            _currentState.FixedTick();
        }

        public void LateTick()
        {
            _currentState.LateTick();
        }

        private StateTransformer CheckForTransformer()
        {
            foreach (StateTransformer stateTransformer in _anyStateTransformers)
            {
                if (stateTransformer.Condition.Invoke()) return stateTransformer;
            }

            foreach (StateTransformer stateTransformer in _stateTransformers)
            {
                if (stateTransformer.Condition.Invoke() && _currentState == stateTransformer.From)
                    return stateTransformer;
            }

            return null;
        }

        public void AddState(IState from, IState to, System.Func<bool> condition)
        {
            StateTransformer stateTransformer = new StateTransformer(from, to, condition);
            _stateTransformers.Add(stateTransformer);
        }

        public void AddAnyState(IState to, System.Func<bool> condition)
        {
            StateTransformer stateTransformer = new StateTransformer(null, to, condition);
            _anyStateTransformers.Add(stateTransformer);
        }
    }
}
