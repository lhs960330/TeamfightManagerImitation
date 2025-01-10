using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChampionState { Idle, Move, Attack, Kiting, Die }


public class BaseChampion : MonoBehaviour
{
    [SerializeField] protected int hp;  // 체력
    //[SerializeField] ChampionData data;
    [SerializeField] protected StateMachine fsm;         // 상태머신
    public StateMachine Fsm { get { return fsm; } }
    Animator animator;
    private void Update()
    {
        fsm.Update();
    }

    private void LateUpdate()
    {
        fsm.LateUpdate();
    }

    private void FixedUpdate()
    {
        fsm.FixedUpdate();
    }

    private void OnEnable()
    {
        // 비활성화 되었다가 활성화될떄 바꿔줘야되는거 예)HP, 스폰 장소, 등
        //  hp = data.hp;
    }
    private void Awake()
    {
        fsm = new StateMachine();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Init();
        fsm.Init("Idle");
    }
    protected virtual void Init()
    {
        // data = GetComponent<ChampionData>();
        // hp = data.maxHp;

        fsm.AddState("Idle", new IdleState(this));
        fsm.AddState("Move", new MoveState(this));
        fsm.AddState("Attack", new AttackState(this));

        fsm.AddState("Die", new DieState(this));
        fsm.AddAnyState("Die", () => hp <= 0);
    }
    public void TakeHit( int damage )
    {
        hp -= damage;
    }
    public void ChangeState( string state )
    {
        fsm.ChangeState(state); // fsm의 내부 로직을 직접 사용하지 않도록 캡슐화
    }
    [SerializeField] protected List<BaseChampion> enemys = new List<BaseChampion>();
    public virtual void FindEnemy()
    {
        // 게임 매니저에서 찾아와서 해야됨

    }
    public bool HasEnemyInRange()
    {
        // 척이 있는지 없는 확인
        if ( enemys.Count == 0 ) return false;

        return true;
    }
    public BaseChampion targetEnemy;
    public void FindClosestEnemy()
    {
        float closestDistance = float.MaxValue;
        foreach ( var enemy in enemys )
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if ( distance < closestDistance )
            {
                closestDistance = distance;
                targetEnemy = enemy;
            }
        }
    }
    public void PlayAni( string aniName )
    {
        animator.Play(aniName);
        curAnimationTime = animator.GetCurrentAnimatorStateInfo(0).length;
    }
    public float curAnimationTime;

}
