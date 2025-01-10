using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChampionState { Idle, Move, Attack, Die }


public class BaseChampion : MonoBehaviour
{
    [SerializeField] protected int hp;  // 체력
    //[SerializeField] ChampionData data;
    [SerializeField] protected NewClass.StateMachine fsm = new NewClass.StateMachine();         // 상태머신
    
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
        // 한번만 실행하면되는것들 (컴포넌트를 찾아서 넣거나, 상태들을 넣어줄때
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
