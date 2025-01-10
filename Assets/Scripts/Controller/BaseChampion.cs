using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChampionState { Idle, Move, Attack, Kiting, Die }


public class BaseChampion : MonoBehaviour
{
    [SerializeField] protected int hp;  // ü��
    //[SerializeField] ChampionData data;
    [SerializeField] protected StateMachine fsm;         // ���¸ӽ�
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
        // ��Ȱ��ȭ �Ǿ��ٰ� Ȱ��ȭ�ɋ� �ٲ���ߵǴ°� ��)HP, ���� ���, ��
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
        fsm.ChangeState(state); // fsm�� ���� ������ ���� ������� �ʵ��� ĸ��ȭ
    }
    [SerializeField] protected List<BaseChampion> enemys = new List<BaseChampion>();
    public virtual void FindEnemy()
    {
        // ���� �Ŵ������� ã�ƿͼ� �ؾߵ�

    }
    public bool HasEnemyInRange()
    {
        // ô�� �ִ��� ���� Ȯ��
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
