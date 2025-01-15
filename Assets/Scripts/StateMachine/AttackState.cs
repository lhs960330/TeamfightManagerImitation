using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    bool isKiting = false;
    public AttackState( BaseChampion owner ) : base(owner) { }
    Coroutine attackCoroutine;
    public override void Enter()
    {
        // 카이팅하는 캐릭터인지 확인
        if ( owner.Fsm.FindState("Kiting") != null )
        {
            isKiting = true;
        }
        // 공격 애니메이션의 딜레이를 관리하는 코루틴을 시작
        if ( attackCoroutine != null )
        {
            owner.StopCoroutine(attackCoroutine); // 이전 공격이 끝나지 않은 경우 멈추기
        }
        attackCoroutine = owner.StartCoroutine(AttackDelay());

    }
    public override void Update()
    {
    }
    private IEnumerator AttackDelay()
    {
        // 딜레이 후 공격 처리
        while ( owner.targetEnemy != null )
        {

            // 상대가 있고 사거리안에 있을때 발동
            if ( owner.targetEnemy != null && Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position) < owner.Data.attackRange )
            {
                owner.PlayAni("Attack");
                // 현재 적이 하나여서 BaseChanmpion에서 적을 가지고있는 상태이다. 나중에 매니저가 적과 아군에 데이터를 가지고 있어 그걸 이용하는게 맞는거 같다.
                owner.targetEnemy.TakeHit(owner.targetEnemy.Data.attackPower);  // 데미지 10을 줌
            }
            else
            {
                owner.ChangeState("Idle");
            }
            yield return new WaitForSeconds(owner.curAnimationTime);  // 애니메이션 딜레이
            yield return new WaitForSeconds(1f);  // 1초 대기 (공격 딜레이 시간)
            // 카이팅이 필요한 캐릭터이면
            if ( isKiting )
            {
                float distanceToEnemy = Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position);
                if ( distanceToEnemy < owner.Data.attackRange )
                {
                    owner.ChangeState("Kiting");
                }
            }

        }
    }

    public override void Exit()
    {
        // 공격이 끝나면 상태 종료
        if ( attackCoroutine != null )
        {
            owner.StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}
