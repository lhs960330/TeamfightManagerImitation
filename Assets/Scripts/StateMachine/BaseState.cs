using System.Collections.Generic;
using UnityEngine;
public class BaseState
{
    public List<Transition> transitions;
    protected BaseChampion owner;
    protected SpriteRenderer renderer;
    public BaseState( BaseChampion owner )
    {
        this.owner = owner;
        transitions = new List<Transition>();
        renderer = owner.gameObject.GetComponent<SpriteRenderer>();
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
    protected void FlipCharacter( bool isReverse = false )
    {
        Vector3 direction = ( owner.targetEnemy.transform.position - owner.transform.position );
        bool shouldFlip = ( direction.x < 0 ) != isReverse;  // isReverse 여부에 따라 반전 여부 결정
        renderer.flipX = shouldFlip;
    }
}