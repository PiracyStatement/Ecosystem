using Unity.VisualScripting;
using UnityEngine;
using static Fish;

public class Chicken : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private ChickenState currentState = ChickenState.Idling;
    public enum ChickenState
    {
        Idling,
        Fleeing,
        Staring,
        Spinning
    }

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Sprite leftChicken;
    public Sprite rightChicken;
    public Sprite forwardChicken;

    private bool isRoaming = false;
    private bool isRoamingLeft = false;
    private Vector2 dest = new Vector2 (0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator.enabled = false;

        dest.x = rb.position.x;
        dest.y = rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }
    public void StartState(ChickenState newState)
    {
        EndState(currentState);

        switch (newState)
        {
            case ChickenState.Idling:

                break;

            case ChickenState.Fleeing:
                //run from pigeon
                break;

            case ChickenState.Staring:
                rb.linearVelocity = Vector2.zero;
                break;

            case ChickenState.Spinning:
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
            case ChickenState.Idling:
                if (!isRoaming)
                {
                    int rnd = Random.Range(1, 1000);

                    if (rnd == 999)
                    {
                        dest.x = Random.Range(-3.3f, 3.3f);
                        print(dest.x);

                        if (dest.x < rb.position.x)
                        {
                            isRoamingLeft = true;
                        }
                        else
                        {
                            isRoamingLeft = false;
                        }

                        isRoaming = true;
                        print(isRoamingLeft);
                    }
                }

                if (isRoaming)
                {
                    if (isRoamingLeft)
                    {
                        if (dest.x < rb.position.x)
                        {
                            rb.AddForce(new Vector2(-1, 0));
                            //print(rb.position.x);
                        }
                        else if (dest.x > rb.position.x)
                        {
                            rb.linearVelocity = Vector2.zero;
                            isRoaming = false;
                        }
                    }
                    else
                    {
                        if (dest.x > rb.position.x)
                        {
                            rb.AddForce(new Vector2(1, 0));
                            //print(rb.position.x);
                        }
                        else if (dest.x < rb.position.x)
                        {
                            rb.linearVelocity = Vector2.zero;
                            isRoaming = false;
                        }
                    }


                    if (rb.linearVelocity.x > 0)
                    {
                        sr.sprite = rightChicken;
                    }
                    else if (rb.linearVelocity.x < 0)
                    {
                        sr.sprite = leftChicken;
                    }
                    else
                    {
                        sr.sprite = forwardChicken;
                    }
                }
            break;

            case ChickenState.Fleeing:
                //run from pigeon
            break;

            case ChickenState.Staring:
                sr.sprite = forwardChicken;
                break;

            case ChickenState.Spinning:
                //spin
            break;
        }
    }

    public void EndState(ChickenState oldState)
    {
        switch (oldState)
        {

        }
    }


    public void StartSpin()
    {
        StartState(ChickenState.Staring);
        Invoke("StartSpinForReal", 2.0f);
    }

    public void StartSpinForReal()
    {
        StartState(ChickenState.Spinning);
    }
}
