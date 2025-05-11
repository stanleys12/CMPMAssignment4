using System.Collections.Generic;
using UnityEngine;

public class Repeater : BehaviorTree
{
    BehaviorTree child;

    public Repeater(BehaviorTree child)
    {
        this.child = child;
    }

    public override Result Run()
    {
        child.Run();  // Continuously run the child node
        return Result.IN_PROGRESS;
    }

    public override BehaviorTree Copy()
    {
        return new Repeater(child.Copy());
    }

    public override IEnumerable<BehaviorTree> AllNodes()
    {
        yield return this;
        foreach (var n in child.AllNodes())
        {
            yield return n;
        }
    }
}
