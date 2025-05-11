using UnityEngine;

public class Attack : BehaviorTree
{
    private float attackCooldown = 1f; // Cooldown between attacks in seconds
    private float lastAttackTime = 0f; // Keeps track of the last attack time

    public override Result Run()
    {
        // Ensure the agent is valid and has an action
        if (agent == null)
        {
            Debug.LogError("Agent is null in Attack behavior.");
            return Result.FAILURE;
        }

        EnemyAction act = agent.GetAction("attack");

        if (act == null)
        {
            Debug.LogError("Attack action is null. Check if 'attack' action is properly assigned.");
            return Result.FAILURE;
        }

        // Check if the target is still alive
        if (GameManager.Instance.player == null)
        {
            Debug.LogError("Player target is null.");
            return Result.FAILURE;
        }

        // Check if the cooldown time has passed (to prevent spamming)
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            bool success = act.Do(GameManager.Instance.player.transform);
            if (success)
            {
                lastAttackTime = Time.time; // Update last attack time
                return Result.IN_PROGRESS; // Keep attacking until the target is dead or out of range
            }
        }

        return Result.IN_PROGRESS; // Keep running this node until the attack is successful or fails
    }

    public Attack() : base()
    {
    }

    public override BehaviorTree Copy()
    {
        return new Attack();
    }
}