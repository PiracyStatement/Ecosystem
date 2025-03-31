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
    public int bouncedRequired;
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
        bouncedRequired = Random.Range(4, 8);

        transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), transform.position.z);

        SlideAndBounce();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
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
                    StartState(FishState.Staring);
                    Explode();
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

    public void Explode()
    {
        explosion.Play();
        animator.SetBool("isExploding", true);

        GameObject.Find("Chicken").GetComponent<Chicken>().Explode();
        GameObject.Find("Cockroach").GetComponent<Cockroach>().Explode();

        Invoke("Restart", 3f);
    }

    void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
