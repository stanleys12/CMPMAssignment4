using System.Collections.Generic;
using UnityEngine;

public class Selector : InteriorNode
{
    public override Result Run()
    {
        while (current_child < children.Count)
        {
            Result res = children[current_child].Run();
            Debug.Log($"Selector evaluating child {current_child}: {res}");

            if (res == Result.SUCCESS)
            {
                current_child = 0;
                Debug.Log("Selector returning SUCCESS.");
                return Result.SUCCESS;
            }
            if (res == Result.IN_PROGRESS)
            {
                // Don't increment current_child — we want to re-run this same child next frame
                Debug.Log("Selector returning IN_PROGRESS.");
                return Result.IN_PROGRESS;
            }

            current_child++; // Only increment if FAILURE
        }

        current_child = 0;
        Debug.Log("Selector returning FAILURE.");
        return Result.FAILURE;
    }

    public Selector(IEnumerable<BehaviorTree> children) : base(children) { }

    public override BehaviorTree Copy()
    {
        return new Selector(CopyChildren());
    }
}
