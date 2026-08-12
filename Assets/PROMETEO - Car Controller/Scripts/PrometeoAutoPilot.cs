using UnityEngine;

public class PrometeoAutoPilot : MonoBehaviour
{
    [SerializeField] private PrometeoCarController car;

    [Header("Random Input")]
    [SerializeField] private float minActionTime = 0.5f;
    [SerializeField] private float maxActionTime = 2f;

    [Header("Probabilities")]
    [Range(0f, 1f)]
    [SerializeField] private float forwardChance = 0.55f;

    [Range(0f, 1f)]
    [SerializeField] private float reverseChance = 0.1f;

    [Range(0f, 1f)]
    [SerializeField] private float leftChance = 0.15f;

    [Range(0f, 1f)]
    [SerializeField] private float rightChance = 0.15f;

    [Range(0f, 1f)]
    [SerializeField] private float handbrakeChance = 0.05f;

    private Action currentAction;
    private float actionTimer;

    private enum Action
    {
        None,
        Forward,
        Reverse,
        Left,
        Right,
        Handbrake
    }

    private void Start()
    {
        if (car == null)
            car = GetComponent<PrometeoCarController>();

        car.autoPilot = false;

        ChooseRandomAction();
    }

    private void Update()
    {
        if (car == null)
            return;

        actionTimer -= Time.deltaTime;

        if (actionTimer <= 0f)
        {
            ChooseRandomAction();
        }

        ExecuteAction();
    }

    private void ChooseRandomAction()
    {
        actionTimer = Random.Range(minActionTime, maxActionTime);

        float random = Random.value;

        if (random < forwardChance)
        {
            currentAction = Action.Forward;
        }
        else if (random < forwardChance + reverseChance)
        {
            currentAction = Action.Reverse;
        }
        else if (random < forwardChance + reverseChance + leftChance)
        {
            currentAction = Action.Left;
        }
        else if (random < forwardChance + reverseChance + leftChance + rightChance)
        {
            currentAction = Action.Right;
        }
        else
        {
            currentAction = Action.Handbrake;
        }
    }

    private void ExecuteAction()
    {
        switch (currentAction)
        {
            case Action.Forward:
                car.GoForward();
                break;

            case Action.Reverse:
                car.GoReverse();
                break;

            case Action.Left:
                car.TurnLeft();
                break;

            case Action.Right:
                car.TurnRight();
                break;

            case Action.Handbrake:
                car.Handbrake();
                break;

            case Action.None:
                car.ThrottleOff();
                car.ResetSteeringAngle();
                break;
        }
    }
}