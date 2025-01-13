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
            // 적에게 도달했으면 Attack 상태로 전환 3f는 사거리 정해지면 그때 변경
            if ( Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position) < owner.Data.attackRange )
            {
                owner.ChangeState("Attack");  // 공격 상태로 전환
            }
        }
        else
        {
            // 적이 없으면 Idle 상태로 돌아가기
            owner.ChangeState("Idle");
        }

    }
    private void MoveTowardsEnemy()
    {
        if ( owner.targetEnemy == null ) return;

        // 적의 위치로 이동
        FlipCharacter();
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.targetEnemy.transform.position, owner.Data.movementSpeed * Time.deltaTime);  // 5f는 이동 속도
    }

}
