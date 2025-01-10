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
        // ���������� �����ൿ���� ������ߵ�
        if ( owner.HasEnemyInRange() )
        {
            owner.ChangeState("Move");

        }
        // ���� ������ ��� Ž�����Ѿߵ�
        else
        {
            owner.FindEnemy();
        }
    }
}
