
public class RepeatUntilFailure : BehaviorTree
{
    private BehaviorTree child;

    public RepeatUntilFailure(BehaviorTree child)
    {
        this.child = child;
    }

    public override Result Run()
    {
        Result result = child.Run();

        if (result == Result.FAILURE)
        {
            return Result.FAILURE;
        }

        return Result.IN_PROGRESS;
    }

    public override BehaviorTree Copy()
    {
        return new RepeatUntilFailure(child.Copy());
    }
}