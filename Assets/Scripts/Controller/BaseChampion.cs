using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChampionState { Idle, Move, Attack, Kiting, Wall, Die }


public class BaseChampion : MonoBehaviour
{
    [SerializeField] protected int hp;  // 체력
    [SerializeField] protected StateMachine fsm;         // 상태머신
    [SerializeField] ChampionData data;        // 내 스탯
    // 스탯을 바꿀수 없지만 읽을수 있게
    public ChampionData Data { get { return data; } }
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

        // 원본을 사용하지 않기위해 원본을 복사해서 사용
        data = Instantiate(Manager.Resource.Load<ChampionData>($"Champions/Data/{gameObject.name}"));
        // 나무위키에 나온 스탯으로하면 이동속도가 너무 빠르고 사거리도 너무 길어져 그 수치대로 기준을 잡고 조금 낮추었다.
        data.Init();
        hp = data.maxHp;
    }
    public void TakeHit( int damage )
    {
        // 방어력에 따른 피해 감소 계산
        float damageReduction = damage * ( data.armor / ( 100f + data.armor ) );
        // 실제 피해 계산
        float finalDamage = damage - damageReduction;

        // 체력 감소
        hp -= ( int )finalDamage;
        
        Debug.Log($"{gameObject.name} : {finalDamage}");
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
