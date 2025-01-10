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
        // ī�����ϴ� ĳ�������� Ȯ��
        if ( owner.Fsm.FindState("Kiting") != null )
        {
            Debug.Log(owner.gameObject.name);
            isKiting = true;
        }
        // ���� �ִϸ��̼��� �����̸� �����ϴ� �ڷ�ƾ�� ����
        if ( attackCoroutine != null )
        {
            owner.StopCoroutine(attackCoroutine); // ���� ������ ������ ���� ��� ���߱�
        }
        attackCoroutine = owner.StartCoroutine(AttackDelay());

    }
    public override void Update()
    {
    }
    private IEnumerator AttackDelay()
    {
        // ������ �� ���� ó��
        while ( owner.targetEnemy != null )
        {

            // ��밡 �ְ� ��Ÿ��ȿ� ������ �ߵ�
            if ( owner.targetEnemy != null && Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position) < 1f )
            {
                owner.PlayAni("Attack");
                // ���÷� ������ �ֱ�
                owner.targetEnemy.TakeHit(3);  // ������ 10�� ��
            }
            else
            {
                owner.ChangeState("Idle");
            }
            yield return new WaitForSeconds(owner.curAnimationTime);  // �ִϸ��̼� ������
            yield return new WaitForSeconds(1f);  // 1�� ��� (���� ������ �ð�)
            // ī������ �ʿ��� ĳ�����̸�
            if ( isKiting )
            {
                float distanceToEnemy = Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position);
                if ( distanceToEnemy < 1.5f )
                {
                    owner.ChangeState("Kiting");
                }
            }

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
