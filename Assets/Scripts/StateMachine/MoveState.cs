using UnityEngine;

public class MoveState : BaseState
{
    public MoveState( BaseChampion owner ) : base(owner) { }
    public override void Enter()
    {
        owner.FindClosestEnemy();
        if ( Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position) > 1f )
        {
            owner.PlayAni("Move");
        }
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        MoveTowardsEnemy();
        if ( owner.targetEnemy != null )
        {
            // ������ ���������� Attack ���·� ��ȯ 3f�� ��Ÿ� �������� �׶� ����
            if ( Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position) < owner.Data.attackRange )
            {
                owner.ChangeState("Attack");  // ���� ���·� ��ȯ
            }
        }
        else
        {
            // ���� ������ Idle ���·� ���ư���
            owner.ChangeState("Idle");
        }

    }
    private void MoveTowardsEnemy()
    {
        if ( owner.targetEnemy == null ) return;

        // ���� ��ġ�� �̵�
        FlipCharacter();
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.targetEnemy.transform.position, owner.Data.movementSpeed * Time.deltaTime);  // 5f�� �̵� �ӵ�
    }

}
