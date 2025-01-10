using System;
using System.Collections.Generic;

    public class StateMachine
    {
        public string curState { get; private set; }
        private Dictionary<string, BaseState> stateDic;
        private List<Transition> anyStateTransition;

        public StateMachine()
        {
            stateDic = new Dictionary<string, BaseState>();
            anyStateTransition = new List<Transition>();
        }

        public void AddState( string key, BaseState value )
        {
            stateDic.Add(key, value);
        }

        public void AddAnyState( string state, Func<bool> condition )
        {
            anyStateTransition.Add(new Transition(state, condition));
        }

        public void AddTransition( string start, string end, Func<bool> condition )
        {
            stateDic [start].transitions.Add(new Transition(end, condition));
        }

        public void Init( string entry )
        {
            curState = entry;
            stateDic [entry].Enter();
        }
    public BaseState FindState(string key)
    {
        foreach( var stat in stateDic )
        {
            if ( key == stat.Key )
                return stat.Value;
        }
        return null;
    }
        public void Update()
        {
            stateDic [curState].Update();

            foreach ( var transition in anyStateTransition )
            {
                if ( transition.condition() )
                {
                    ChangeState(transition.end);
                    return;
                }
            }

            foreach ( var transition in stateDic [curState].transitions )
            {
                if ( transition.condition() )
                {
                    ChangeState(transition.end);
                    return;
                }
            }
        }

        public void LateUpdate()
        {
            stateDic [curState].LateUpdate();
        }

        public void FixedUpdate()
        {
            stateDic [curState].FixedUpdate();
        }

        public void ChangeState( string nextState )
        {
            stateDic [curState].Exit();
            curState = nextState;
            stateDic [curState].Enter();
        }
    }

    public struct Transition
    {
        public string end;
        public Func<bool> condition;

        public Transition( string end, Func<bool> condition )
        {
            this.end = end;
            this.condition = condition;
        }
    }