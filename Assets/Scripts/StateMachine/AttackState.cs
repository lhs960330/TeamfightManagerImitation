using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public AttackState( BaseChampion owner ) : base(owner) { }
    Coroutine attackCoroutine;
    public override void Enter()
    {

        // 공격 애니메이션의 딜레이를 관리하는 코루틴을 시작
        if ( attackCoroutine != null )
        {
            owner.StopCoroutine(attackCoroutine); // 이전 공격이 끝나지 않은 경우 멈추기
        }
        attackCoroutine = owner.StartCoroutine(AttackDelay());
    }
    private IEnumerator AttackDelay()
    {
        // 딜레이 후 공격 처리
        while ( owner.targetEnemy != null )
        {
            if ( owner.targetEnemy != null )
            {
                Debug.Log($"공격시작 : {owner.gameObject.name}");
                owner.PlayAni("Attack");
                // 예시로 데미지 주기
                owner.targetEnemy.TakeHit(3);  // 데미지 10을 줌
            }
            else
            {
                Debug.Log("돌아가기");
                owner.ChangeState("Idle");
            }
            yield return new WaitForSeconds(owner.curAnimationTime);  // 애니메이션 딜레이
            yield return new WaitForSeconds(1f);  // 1초 대기 (공격 딜레이 시간)
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
