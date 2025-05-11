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
        Result result = child.Run();
        if (result != Result.IN_PROGRESS)
        {
            child = child.Copy(); 
            child.SetAgent(agent); 
        }
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
