using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float duration = 0.8f;

    void Start()
    {
        Destroy(gameObject, duration);
    }
}