using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChampionState { Idle, Move, Attack, Die }


public class BaseChampion : MonoBehaviour
{
    [SerializeField] protected int hp;  // ü��
    //[SerializeField] ChampionData data;
    [SerializeField] protected NewClass.StateMachine fsm = new NewClass.StateMachine();         // ���¸ӽ�
    
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
        // �ѹ��� �����ϸ�Ǵ°͵� (������Ʈ�� ã�Ƽ� �ְų�, ���µ��� �־��ٶ�
        Init();
    }
    public virtual void FindEnemy()
    {
        
    }
    protected virtual void Init()
    {
        animator = GetComponent<Animator>();
        // data = GetComponent<ChampionData>();
        // hp = data.maxHp;

        fsm.AddState("Idle", new NewClass.IdleState(this));

        fsm.AddState("Die", new NewClass.DieState(this));
        fsm.Init("Idle");
        fsm.AddAnyState("Die", () => hp <= 0);

    }
    public void TakeHit( int damage )
    {
        hp -= damage;
    }

}
