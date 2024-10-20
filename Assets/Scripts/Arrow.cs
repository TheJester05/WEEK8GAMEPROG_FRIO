using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    public float maxRange = 50f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        CheckRange();
    }

    public void SetArrowProperties(float speed, float chargeLevel)
    {
        damage = Mathf.Lerp(10, 50, chargeLevel); // Damage scales with charge level
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy arrow on impact
        }
    }

    void CheckRange()
    {
        if (Vector3.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject);
        }
    }
}
