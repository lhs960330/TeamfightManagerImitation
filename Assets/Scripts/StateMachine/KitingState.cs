using UnityEngine;

public class KitingState : BaseState
{
    private float maxKitingDistance = 3f;  // 카이팅 종료를 위한 최대 거리
    private float kitingStartTime;
    private float kitingDuration = 1f;  // 카이팅 지속 시간
    public KitingState( BaseChampion owner ) : base(owner)
    {
        Mask = LayerMask.GetMask("Wall");
    }

    public override void Enter()
    {
        Debug.Log("카이팅 시작");
        kitingStartTime = Time.time;  // 카이팅 시작 시간을 기록
    }

    public override void Update()
    {
        // 카이팅 종료 조건 체크
        if ( ShouldExitKiting() )
        {
            owner.ChangeState("Move");  // 카이팅 종료 후 이동 상태로 변경
            return;
        }
        MoveAwayFromEnemy();

    }

    private bool ShouldExitKiting()
    {
        // 적과의 거리가 너무 멀어졌을 때
        if ( Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position) > owner.Data.attackRange )
        {
            return true;
        }

        // 카이팅 시간이 지정된 시간 이상이 되면 종료
        if ( Time.time - kitingStartTime > kitingDuration )
        {
            return true;
        }

        // 적이 더 이상 없을 때
        if ( owner.targetEnemy == null )
        {
            return true;
        }
        return false;
    }

    private void MoveAwayFromEnemy()
    {
        owner.PlayAni("Move");
        // 적과 반대 방향으로 이동
        Vector3 directionAwayFromEnemy = ( owner.transform.position - owner.targetEnemy.transform.position ).normalized;

        // 벽을 만났을때는 따로 설정 
        owner.transform.position += CheckWall(directionAwayFromEnemy) * owner.Data.movementSpeed * Time.deltaTime;
        FlipCharacter(true);
    }
    LayerMask Mask;
    private Vector3 CheckWall( Vector3 dir )
    {
        // 2D 레이캐스트를 위해 Vector2로 변환
        Vector2 origin = owner.transform.position;
        Vector2 direction = new Vector2(dir.x, dir.y).normalized;

        // 2D 레이캐스트 수행
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 1f, Mask);

        // 디버그용 레이
        Debug.DrawRay(origin, direction * 1f, hit.collider ? Color.red : Color.green);

        if ( hit.collider != null )
        {
            // 벽을 만났을때 가야될 방향
            Vector2 hitDir = ( hit.point - origin ).normalized;
            Debug.Log($"벽 감지됨: {hit.collider.gameObject.name} | 충돌 지점: {hit.point}");
            Vector3 perpendicular = new Vector3(hitDir.y, -hitDir.x, 0);
            return perpendicular.normalized;
        }

        Debug.Log("벽 감지 실패");
        return dir; // 벽 미감지
    }
}