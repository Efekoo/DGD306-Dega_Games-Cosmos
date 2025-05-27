using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public GameObject bg1;
    public GameObject bg2;
    public float scrollSpeed = 2f;

    private float width;

    void Start()
    {
        // Sprite Renderer'dan sprite geni�li�ini hesapla
        width = bg1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Her iki sprite'� da sola kayd�r
        bg1.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        bg2.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // BG1 tamamen ekran d���na ��karsa onu sa�a ta��
        if (bg1.transform.position.x <= -width)
        {
            bg1.transform.position += Vector3.right * width * 2;
        }

        // BG2 tamamen ekran d���na ��karsa onu sa�a ta��
        if (bg2.transform.position.x <= -width)
        {
            bg2.transform.position += Vector3.right * width * 2;
        }
    }
}