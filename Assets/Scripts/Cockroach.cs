using UnityEngine;
using static Chicken;

public class Cockroach : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private CockroachState currentState = CockroachState.Waiting;
    public enum CockroachState
    {
        Waiting,
        FreakingOut,
        Staring,
        Spinning
    }

    private Rigidbody2D rb;
    private Transform chTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chTransform = GameObject.Find("Chicken").GetComponent<Transform>();
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
            case CockroachState.Waiting:

                break;

            case CockroachState.FreakingOut:

                break;

            case CockroachState.Staring:
                rb.linearVelocity = Vector2.zero;
                transform.rotation = Quaternion.identity;
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
        Vector3 chVector = new Vector3(chTransform.position.x, chTransform.position.y, -0.09f);
        //print(chVector.x);

        switch (currentState)
        {
            case CockroachState.Waiting:
                if ((Vector3.Distance(chVector, transform.position)) < 1.7)
                {
                    StartState(CockroachState.FreakingOut);
                }
                break;

            case CockroachState.FreakingOut:
                if ((Vector3.Distance(chVector, transform.position)) > 1.7)
                {
                    StartState(CockroachState.Waiting);
                }

                transform.Rotate(new Vector3(0, 0, -1.5f));
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
    public void Explode()
    {
        animator.SetBool("isExploding", true);
    }
}
