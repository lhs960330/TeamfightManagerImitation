using System.Collections;
using UnityEngine;
public class DieState : BaseState
{
    protected Coroutine routine;

    public DieState( BaseChampion owner ) : base(owner) { }

    public override void Enter()
    {
        owner.PlayAni("Die");
        routine = owner.StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(owner.curAnimationTime);
        GameObject.Destroy(owner.gameObject);
    }
}