using UnityEngine;

public class CheckGameTime : BehaviorTree
{
    private float threshold;

    public CheckGameTime(float threshold)
    {
        this.threshold = threshold;
    }

    public override Result Run()
    {
        if (GameManager.Instance.CurrentTime()
 >= threshold)
            return Result.SUCCESS;
        return Result.FAILURE;
    }

    public override BehaviorTree Copy()
    {
        return new CheckGameTime(threshold);
    }
}
