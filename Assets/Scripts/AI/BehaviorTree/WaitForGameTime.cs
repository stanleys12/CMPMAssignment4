public class WaitForGameTime : BehaviorTree
{
    private float targetTime;

    public WaitForGameTime(float timeToWait)
    {
        targetTime = timeToWait;
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
