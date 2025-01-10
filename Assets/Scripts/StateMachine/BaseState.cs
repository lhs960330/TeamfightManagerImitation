using System.Collections.Generic;
public class BaseState
{
    public List<Transition> transitions;
    protected BaseChampion owner;

    public BaseState( BaseChampion owner )
    {
        this.owner = owner;
        transitions = new List<Transition>();
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}