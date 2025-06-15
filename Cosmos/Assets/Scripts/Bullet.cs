using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private float timer = 0f;
    public Vector2 direction = Vector2.right;


    void Start()
    {

        direction = direction.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool destroyBullet = true;

        if (other.CompareTag("SmallMeteor") || other.CompareTag("BigMeteor"))
        {
            MeteorBase meteor = other.GetComponent<MeteorBase>();
            if (meteor != null)
            {
                meteor.TakeDamage(1);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyPlane enemy = other.GetComponent<EnemyPlane>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            AlienEnemyUFO ufo = other.GetComponent<AlienEnemyUFO>();
            if (ufo != null)
            {
                ufo.TakeDamage(1);
            }

            PaperEnemy paper = other.GetComponent<PaperEnemy>();
            if (paper != null)
            {
                paper.TakeDamage(1);
            }
        }
        else if (other.CompareTag("Boss"))
        {

            Level2Boss level2Boss = other.GetComponent<Level2Boss>();
            if (level2Boss != null)
            {
                Debug.Log("Level2Boss bulundu!");
                level2Boss.TakeDamage(1);
            }
            else
            {

                BossController boss = other.GetComponent<BossController>();
                if (boss != null)
                {
                    Debug.Log("Level1Boss/BossController bulundu!");
                    boss.TakeDamage(1);
                }
                else
                {
                    Debug.LogWarning("Hiçbir boss scripti bulunamadý!");
                }
            }
        }
        else
        {
            destroyBullet = false;
        }

        if (destroyBullet)
        {
            Destroy(gameObject);
        }
    }
}