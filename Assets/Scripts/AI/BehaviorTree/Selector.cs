using System.Collections.Generic;
using UnityEngine;

public class Selector : InteriorNode
{
    public override Result Run()
    {
        while (current_child < children.Count)
        {
            Result res = children[current_child].Run();
            //Debug.Log($"Selector evaluating curr child {current_child}: {res}");

            if (res == Result.SUCCESS)
            {
                current_child = 0;
                Debug.Log("Selector returning success");
                return Result.SUCCESS;
            }
            if (res == Result.IN_PROGRESS)
            {
                Debug.Log("Selector returning in progrress");
                return Result.IN_PROGRESS;
            }

            current_child++;
        }

        current_child = 0;
        Debug.Log("Selector returning failur");
        return Result.FAILURE;
    }

    public Selector(IEnumerable<BehaviorTree> children) : base(children) { }

    public override BehaviorTree Copy()
    {
        return new Selector(CopyChildren());
    }
}
