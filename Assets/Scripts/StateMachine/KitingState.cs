using UnityEngine;

public class KitingState : BaseState
{
    private float maxKitingDistance = 3f;  // ī���� ���Ḧ ���� �ִ� �Ÿ�
    private float kitingStartTime;
    private float kitingDuration = 1f;  // ī���� ���� �ð�
    public KitingState( BaseChampion owner ) : base(owner)
    {
        Mask = LayerMask.GetMask("Wall");
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
        if ( Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position) > owner.Data.attackRange )
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
        owner.PlayAni("Move");
        // ���� �ݴ� �������� �̵�
        Vector3 directionAwayFromEnemy = ( owner.transform.position - owner.targetEnemy.transform.position ).normalized;

        // ���� ���������� ���� ���� 
        owner.transform.position += CheckWall(directionAwayFromEnemy) * owner.Data.movementSpeed * Time.deltaTime;
        FlipCharacter(true);
    }
    LayerMask Mask;
    private Vector3 CheckWall( Vector3 dir )
    {
        // 2D ����ĳ��Ʈ�� ���� Vector2�� ��ȯ
        Vector2 origin = owner.transform.position;
        Vector2 direction = new Vector2(dir.x, dir.y).normalized;

        // 2D ����ĳ��Ʈ ����
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 1f, Mask);

        // ����׿� ����
        Debug.DrawRay(origin, direction * 1f, hit.collider ? Color.red : Color.green);

        if ( hit.collider != null )
        {
            // ���� �������� ���ߵ� ����
            Vector2 hitDir = ( hit.point - origin ).normalized;
            Debug.Log($"�� ������: {hit.collider.gameObject.name} | �浹 ����: {hit.point}");
            Vector3 perpendicular = new Vector3(hitDir.y, -hitDir.x, 0);
            return perpendicular.normalized;
        }

        Debug.Log("�� ���� ����");
        return dir; // �� �̰���
    }
}