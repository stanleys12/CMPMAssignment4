using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;

        Vector3 waypoint = new Vector3(49.6f+ Random.Range(-2f, 2f), -23.3f+ Random.Range(-2f, 2f), 0f);
        Vector3 alternate_waypoint = new Vector3(1.4f+ Random.Range(-2f, 2f), -19.2f+ Random.Range(-2f, 2f), 0f);

        float distanceToBottomLeft = Vector3.Distance(agent.transform.position, alternate_waypoint);
        float distanceToBottomRight = Vector3.Distance(agent.transform.position, waypoint);

        Vector3 chosenWaypoint = (distanceToBottomLeft < distanceToBottomRight) ? alternate_waypoint : waypoint;

        Vector3 warlock_waypoint1 = new Vector3(53.6f, -23.3f, 0f);
        Vector3 warlock_waypoint2 = new Vector3(-1.4f, -19.2f, 0f);
        float distanceToBottomLeft1 = Vector3.Distance(agent.transform.position, warlock_waypoint2);
        float distanceToBottomRight2 = Vector3.Distance(agent.transform.position, warlock_waypoint1);
        Vector3 chosenWaypoint_w = (distanceToBottomLeft1 < distanceToBottomRight2) ? warlock_waypoint2 : warlock_waypoint1;

        Vector3 groupup = new Vector3(20f+ Random.Range(-2f, 2f), 5f+ Random.Range(-2f, 2f), 0f);

        Vector3 zombie_waypoint = new Vector3(15.3f + Random.Range(-2f, 2f), 28f + Random.Range(-2f, 2f), 0f + Random.Range(-2f, 2f));
        Vector3 skeleton_waypoint = new Vector3(47.6f + Random.Range(-2f, 2f), -22.3f + Random.Range(-2f, 2f), 0f + Random.Range(-2f, 2f));

        BehaviorTree combatLoop = new Sequence(new BehaviorTree[] {
            new MoveToPlayer(agent.GetAction("attack").range),
            new Attack()
        });

        BehaviorTree buffBehavior = new Sequence(new BehaviorTree[] {
            new AbilityReadyQuery("buff"),
            new Buff() 
        });

        BehaviorTree healBehavior = new Sequence(new BehaviorTree[] {
            new StrengthFactorQuery(0.5f),
            new Heal()
        });
        if (agent.monster == "warlock")
        {
            result = new Sequence(new BehaviorTree[] {
                new MoveToWaypoint(chosenWaypoint_w),
                new WaitForGameTime(GameManager.Instance.WinTime() - 300f), 
                buffBehavior, 
                combatLoop,
                new PermaBuff(),
                healBehavior
            });
        }
        else if (agent.monster == "zombie")
        {
            result = new Sequence(new BehaviorTree[] {
                                        new MoveToWaypoint(chosenWaypoint),
                                        new WaitForGameTime(GameManager.Instance.WinTime() - 320f),
                                        new MoveToWaypoint(groupup),
                                        combatLoop,
                                        //new MoveToPlayer(agent.GetAction("attack").range),

                                        //new Attack()
                                     });
        }
        else
        {
            result = new Sequence(new BehaviorTree[] {
                                        new MoveToWaypoint(chosenWaypoint),
                                        new WaitForGameTime(GameManager.Instance.WinTime() - 310f),
                                        combatLoop,
                                        //new MoveToPlayer(agent.GetAction("attack").range),

                                        //new Attack()
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