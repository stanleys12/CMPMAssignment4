using UnityEngine;

public class MoveToWaypoint : BehaviorTree
{
    private Vector3 targetPosition;
    private float moveSpeed = 5f;
    public MoveToWaypoint(Vector3 target)
    {
        targetPosition = target;
    }

    public override Result Run()
    {
        if (Vector3.Distance(agent.transform.position, targetPosition) > 0.1f)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            return Result.IN_PROGRESS;
        }

        return Result.SUCCESS;
    }

    public override BehaviorTree Copy()
    {
        return new MoveToWaypoint(targetPosition);
    }
}