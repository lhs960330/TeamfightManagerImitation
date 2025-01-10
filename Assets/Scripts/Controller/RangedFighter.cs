public class RangedFighter : BaseChampion
{
    protected override void Init()
    {
        fsm.AddState("Kiting", new KitingState(this));
        base.Init();
    }
}