using UnityEngine;

public class KitingState : BaseState
{
    private float maxKitingDistance = 3f;  // 카이팅 종료를 위한 최대 거리
    private float kitingStartTime;
    private float kitingDuration = 1f;  // 카이팅 지속 시간

    public KitingState( BaseChampion owner ) : base(owner)
    {
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
        if ( Vector3.Distance(owner.transform.position, owner.targetEnemy.transform.position) > maxKitingDistance )
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
        // 벽을 만났을때는 따로 설정 
        owner.PlayAni("Move");
        // 적과 반대 방향으로 이동
        Vector3 directionAwayFromEnemy = ( owner.transform.position - owner.targetEnemy.transform.position ).normalized;
        owner.transform.position += directionAwayFromEnemy * owner.Data.movementSpeed * Time.deltaTime;
        FlipCharacter(true);
    }
}
