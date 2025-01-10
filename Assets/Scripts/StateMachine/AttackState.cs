using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public AttackState( BaseChampion owner ) : base(owner) { }
    Coroutine attackCoroutine;
    public override void Enter()
    {

        // ���� �ִϸ��̼��� �����̸� �����ϴ� �ڷ�ƾ�� ����
        if ( attackCoroutine != null )
        {
            owner.StopCoroutine(attackCoroutine); // ���� ������ ������ ���� ��� ���߱�
        }
        attackCoroutine = owner.StartCoroutine(AttackDelay());
    }
    private IEnumerator AttackDelay()
    {
        // ������ �� ���� ó��
        while ( owner.targetEnemy != null )
        {
            if ( owner.targetEnemy != null )
            {
                Debug.Log($"���ݽ��� : {owner.gameObject.name}");
                owner.PlayAni("Attack");
                // ���÷� ������ �ֱ�
                owner.targetEnemy.TakeHit(3);  // ������ 10�� ��
            }
            else
            {
                Debug.Log("���ư���");
                owner.ChangeState("Idle");
            }
            yield return new WaitForSeconds(owner.curAnimationTime);  // �ִϸ��̼� ������
            yield return new WaitForSeconds(1f);  // 1�� ��� (���� ������ �ð�)
        }
    }

    public override void Exit()
    {
        // ������ ������ ���� ����
        if ( attackCoroutine != null )
        {
            owner.StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}
