using UnityEngine;
namespace NewClass
{
    public class IdleState : BaseState
    {
        private BaseChampion owner;

        public IdleState( BaseChampion owner )
        {
            this.owner = owner;
        }
        public override void Enter()
        {
        }
        public override void FixedUpdate()
        {
        }
        public override void Update()
        {
        }
        public override void LateUpdate()
        {
        }
        public override void Exit()
        {
        }
    }
}
