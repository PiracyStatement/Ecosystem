using UnityEngine;
using static Chicken;

public class Cockroach : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private CockroachState currentState = CockroachState.Chasing;
    public enum CockroachState
    {
        Chasing,
        Waiting,
        Staring,
        Spinning
    }

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }



    public void StartState(CockroachState newState)
    {
        EndState(currentState);

        switch (newState)
        {
            case CockroachState.Chasing:

                break;

            case CockroachState.Waiting:

                break;

            case CockroachState.Staring:
                rb.linearVelocity = Vector2.zero;
                break;

            case CockroachState.Spinning:
                animator.enabled = true;
                animator.SetBool("isSpinning", true);
                break;
        }

        currentState = newState;
    }

    public void UpdateState()
    {
        switch (currentState)
        {
            case CockroachState.Chasing:

                break;

            case CockroachState.Waiting:
                //run from pigeon
                break;

            case CockroachState.Spinning:
                //spin
                break;
        }
    }

    public void EndState(CockroachState oldState)
    {
        switch (oldState)
        {

        }
    }


    public void StartSpin()
    {
        StartState(CockroachState.Staring);
        Invoke("StartSpinForReal", 2.0f);
    }

    public void StartSpinForReal()
    {
        StartState(CockroachState.Spinning);
    }
}
