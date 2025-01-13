using UnityEngine;

public class KitingState : BaseState
{
    private float maxKitingDistance = 3f;  // ī���� ���Ḧ ���� �ִ� �Ÿ�
    private float kitingStartTime;
    private float kitingDuration = 1f;  // ī���� ���� �ð�

    public KitingState( BaseChampion owner ) : base(owner)
    {
    }

    public override void Enter()
    {
        Debug.Log("ī���� ����");
        kitingStartTime = Time.time;  // ī���� ���� �ð��� ���
    }

    public override void Update()
    {
        // ī���� ���� ���� üũ
        if ( ShouldExitKiting() )
        {
            owner.ChangeState("Move");  // ī���� ���� �� �̵� ���·� ����
            return;
        }

        MoveAwayFromEnemy();
    }

    private bool ShouldExitKiting()
    {
        // ������ �Ÿ��� �ʹ� �־����� ��
        if ( Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position) > maxKitingDistance )
        {
            return true;
        }

        // ī���� �ð��� ������ �ð� �̻��� �Ǹ� ����
        if ( Time.time - kitingStartTime > kitingDuration )
        {
            return true;
        }

        // ���� �� �̻� ���� ��
        if ( owner.targetEnemy == null )
        {
            return true;
        }

        return false;
    }

    private void MoveAwayFromEnemy()
    {
        // ���� ���������� ���� ���� 
        owner.PlayAni("Move");
        // ���� �ݴ� �������� �̵�
        Vector3 directionAwayFromEnemy = ( owner.transform.position - owner.targetEnemy.transform.position ).normalized;
        owner.transform.position += directionAwayFromEnemy * owner.Data.movementSpeed * Time.deltaTime;
        FlipCharacter(true);
    }
}
