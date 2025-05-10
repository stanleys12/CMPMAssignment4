public class WaitForGameTime : BehaviorTree
{
    private float targetTime;

    // Constructor accepts the time you want to wait until (e.g., 200 seconds)
    public WaitForGameTime(float targetTime)
    {
        this.targetTime = targetTime;
    }

    public override Result Run()
    {
        if (GameManager.Instance.CurrentTime() >= targetTime)
        {
            return Result.SUCCESS;
        }

        return Result.IN_PROGRESS;
    }

    public override BehaviorTree Copy()
    {
        return new WaitForGameTime(targetTime);
    }
}