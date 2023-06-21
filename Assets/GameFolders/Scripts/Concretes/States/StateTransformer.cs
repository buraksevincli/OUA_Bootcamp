using GameFolders.Scripts.Abstracts.States;

namespace GameFolders.Scripts.Concretes.States
{
    public class StateTransformer
    {
        public IState To { get; }
        public IState From { get; } 
        public System.Func<bool> Condition { get; }

        public StateTransformer(IState from, IState to, System.Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}
