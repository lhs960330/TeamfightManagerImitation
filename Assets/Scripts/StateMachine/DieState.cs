using System.Collections;
using UnityEngine;
namespace NewClass
{
    public class DieState : BaseState
    {
        private BaseChampion owner;
        private Coroutine routine;

        public DieState( BaseChampion owner )
        {
            this.owner = owner;
        }

        public override void Enter()
        {
            routine = owner.StartCoroutine(DieRoutine());
        }

        IEnumerator DieRoutine()
        {
            yield return new WaitForSeconds(1);
            GameObject.Destroy(owner.gameObject);
        }
    }
}