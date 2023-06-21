using GameFolders.Scripts.Abstracts.States;

namespace GameFolders.Scripts.Concretes.States
{
    public class StateTransformer
    {
        public IState To { get; }
        public IState From { get; } 
        public System.Func<bool> Condition { get; }

        public StateTransformer(IState to, IState from, System.Func<bool> condition)
        {
            To = to;
            From = from;
            Condition = condition;
        }
    }
}
