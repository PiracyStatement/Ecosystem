using UnityEngine;
using static Chicken;

public class Wall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<Fish>())
        {
            print("No fish");
            return;
        }

        int bCount = collision.gameObject.GetComponent<Fish>().bouncedCount;
        bCount++;

        if (bCount > 5)
        {
            collision.gameObject.GetComponent<Fish>().bouncedCount = 0;
            collision.gameObject.GetComponent<Fish>().StartSpin();
        }

        collision.gameObject.GetComponent<Fish>().bouncedCount = bCount;
    }
}
