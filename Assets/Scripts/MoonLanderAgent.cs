using UnityEngine;
using MLAgents;

public class MoonLanderAgent : Agent
{
    private Lander nave;
    private Vector2 previousVelocity;
    private Vector2 previousDistanceToBase;

    public override void InitializeAgent()
    {
        nave = GetComponent<Lander>();
    }

    public override void CollectObservations()
    {
        AddVectorObs(nave.getVelocity());
        AddVectorObs(nave.getDistanceToBase());
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        int action = (int)vectorAction[0];

        nave.LeftThrust(action == 0);
        nave.CentralThrust(action == 1);
        nave.RightThrust(action == 2);

        Vector2 velocity = nave.getVelocity();
        Vector2 distanceToBase = nave.getDistanceToBase();

        if (previousVelocity != null && Mathf.Abs(velocity.y) < Mathf.Abs(previousVelocity.y)) {
            AddReward(0.05f);
        } else {
            AddReward(-0.05f);
        }

        if (previousDistanceToBase != null && distanceToBase.magnitude < previousDistanceToBase.magnitude) {
            AddReward(0.05f);
        } else {
            AddReward(-0.05f);
        }

        previousVelocity = velocity;
        previousDistanceToBase = distanceToBase;

        if (nave.hasLost()) {
            Done();
        }

        if (nave.hasWon()) {
            AddReward(100f);
            Done();
        }
    }

    public override void AgentReset()
    {
        nave.reset();
    }
}
