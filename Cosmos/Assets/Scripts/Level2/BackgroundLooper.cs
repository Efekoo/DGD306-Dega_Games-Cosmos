using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public GameObject bg1;
    public GameObject bg2;
    public float scrollSpeed = 2f;

    private float width;

    void Start()
    {

        width = bg1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {

        bg1.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        bg2.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;


        if (bg1.transform.position.x <= -width)
        {
            bg1.transform.position += Vector3.right * width * 2;
        }


        if (bg2.transform.position.x <= -width)
        {
            bg2.transform.position += Vector3.right * width * 2;
        }
    }
}