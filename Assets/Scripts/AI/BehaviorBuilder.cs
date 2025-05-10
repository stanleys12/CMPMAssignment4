using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;

        Vector3 waypoint = new Vector3(47.6f, -22.3f, 0f);
        Vector3 alternate_waypoint = new Vector3(-3.4f, -18.2f, 0f);
        float distanceToBottomLeft = Vector3.Distance(agent.transform.position, alternate_waypoint);
        float distanceToBottomRight = Vector3.Distance(agent.transform.position, waypoint);

        Vector3 chosenWaypoint = (distanceToBottomLeft < distanceToBottomRight) ? alternate_waypoint : waypoint;


        Vector3 zombie_waypoint = new Vector3(15.3f + Random.Range(-2f, 2f), 28f + Random.Range(-2f, 2f), 0f + Random.Range(-2f, 2f));
        Vector3 skeleton_waypoint = new Vector3(47.6f + Random.Range(-2f, 2f), -22.3f + Random.Range(-2f, 2f), 0f + Random.Range(-2f, 2f));

        if (agent.monster == "warlock")
        {
            result = new Sequence(new BehaviorTree[] {
                                        new MoveToWaypoint(chosenWaypoint),
                                        new WaitForGameTime(GameManager.Instance.WinTime() - 350f),
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
                                        new MoveToWaypoint(chosenWaypoint),
                                        new WaitForGameTime(GameManager.Instance.WinTime() - 350f),
                                        new MoveToPlayer(agent.GetAction("attack").range),

                                        new Attack()
                                     });
        }
        else
        {
            result = new Sequence(new BehaviorTree[] {
                                        new MoveToWaypoint(chosenWaypoint),
                                        new WaitForGameTime(GameManager.Instance.WinTime() - 350f),
                                        new MoveToPlayer(agent.GetAction("attack").range),

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
