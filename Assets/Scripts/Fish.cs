using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Chicken;

public class Fish : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private FishState currentState = FishState.Bouncing;
    public enum FishState
    {
        Bouncing,
        Staring,
        Spinning
    }

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Sprite leftFish;
    public Sprite rightFish;
    public Sprite forwardFish;

    public AudioSource vineBoom;
    public AudioSource funkyTown;
    public AudioSource explosion;

    public int bouncedCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void SlideAndBounce()
    {
        float rnd = Random.Range(0, 2);

        if (rnd < 1)
        {
            rb.AddForce(new Vector2(100, -150));
        }
        else
        {
            rb.AddForce(new Vector2(-100, 150));
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator.enabled = false;
        SlideAndBounce();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();

        if (Input.GetKey(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }


    public void StartState(FishState newState)
    {
        EndState(currentState);

        switch (newState)
        {
            case FishState.Bouncing:

                break;

            case FishState.Staring:
                rb.linearVelocity = Vector2.zero;
                vineBoom.Play();
                break;

            case FishState.Spinning:
                animator.enabled = true;
                animator.SetBool("isSpinning", true);
                funkyTown.Play();
                break;
        }

        currentState = newState;
    }

    public void UpdateState()
    {
        switch (currentState)
        {
            case FishState.Bouncing:
                if (rb.linearVelocity.x > 0)
                {
                    sr.sprite = rightFish;
                }
                else if (rb.linearVelocity.x < 0)
                {
                    sr.sprite = leftFish;
                }
                break;

            case FishState.Staring:
                sr.sprite = forwardFish;
                break;

            case FishState.Spinning:
                if (!funkyTown.isPlaying)
                {
                    explosion.Play();
                }
                break;
        }
    }

    public void EndState(FishState oldState)
    {
        switch (oldState)
        {

        }
    }



    public void StartSpin()
    {
        StartState(FishState.Staring);

        GameObject.Find("Chicken").GetComponent<Chicken>().StartSpin();
        GameObject.Find("Cockroach").GetComponent<Cockroach>().StartSpin();

        Invoke("StartSpinForReal", 2.0f);
    }

    public void StartSpinForReal()
    {
        StartState(FishState.Spinning);
    }
}
