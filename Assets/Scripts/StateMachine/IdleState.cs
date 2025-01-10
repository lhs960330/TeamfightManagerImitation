using UnityEngine;

public class IdleState : BaseState
{


    public IdleState( BaseChampion owner ) : base(owner) { }
    public override void Enter()
    {
        owner.PlayAni("Idle");
    }

    public override void Update()
    {
        // 적이있으면 다음행동으로 보내줘야됨
        if ( owner.HasEnemyInRange() )
        {
            owner.ChangeState("Move");

        }
        // 적이 없으면 계속 탐색시켜야됨
        else
        {
            owner.FindEnemy();
        }
    }
}
