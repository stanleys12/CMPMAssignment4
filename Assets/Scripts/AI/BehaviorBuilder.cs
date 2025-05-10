using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;

        Vector3 waypoint = new Vector3(47.6f, -22.3f, 0f);

        if (agent.monster == "warlock")
        {
            result = new Sequence(new BehaviorTree[] {
                                        new MoveToWaypoint(waypoint),
                                        new WaitForGameTime(GameManager.Instance.CurrentTime() + 200f), // Wait until 200 seconds have passed
                                        new MoveToPlayer(agent.GetAction("attack").range),

                                        new Attack(),
                                        new PermaBuff(),
                                        new Heal(),
                                        new Buff()
                                     });
        }
        else if (agent.monster == "zombie")
        {
            result = new Sequence(new BehaviorTree[] {
                                        new MoveToWaypoint(waypoint),
                                        new WaitForGameTime(GameManager.Instance.CurrentTime() + 200f),                                        new MoveToPlayer(agent.GetAction("attack").range),
                                        new Attack()
                                     });
        }
        else
        {
            result = new Sequence(new BehaviorTree[] {
                                        new MoveToWaypoint(waypoint),
                                        new WaitForGameTime(GameManager.Instance.CurrentTime() + 200f),                                        new MoveToPlayer(agent.GetAction("attack").range),
                                        new Attack()
                                     });
        }

        // do not change/remove: each node should be given a reference to the agent
        foreach (var n in result.AllNodes())
        {
            n.SetAgent(agent);
        }
        return result;
    }
}
