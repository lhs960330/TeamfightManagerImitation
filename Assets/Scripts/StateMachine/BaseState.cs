using System.Collections.Generic;
namespace NewClass
{
    public class BaseState
    {
        public List<Transition> transitions;

        public BaseState()
        {
            transitions = new List<Transition>();
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
    }
}