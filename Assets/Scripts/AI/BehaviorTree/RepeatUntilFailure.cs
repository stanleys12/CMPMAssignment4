
public class RepeatUntilFailure : BehaviorTree
{
    private BehaviorTree child;

    public RepeatUntilFailure(BehaviorTree child)
    {
        this.child = child;
    }

    public override Result Run()
    {
        // Continuously run the child node until it fails
        Result result = child.Run();

        if (result == Result.FAILURE)
        {
            return Result.FAILURE;  // Stop repeating if failure occurs
        }

        return Result.IN_PROGRESS;  // Keep running the child node as long as it doesn't fail
    }

    public override BehaviorTree Copy()
    {
        return new RepeatUntilFailure(child.Copy());
    }
}