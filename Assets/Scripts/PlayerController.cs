using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float chargeTime = 3f; // Maximum charge time for the arrow
    private float currentCharge = 0f;
    private bool isCharging = false;

    void Update()
    {
        HandleCharging();
    }

    void HandleCharging()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isCharging = true;
            currentCharge = 0f;
        }

        if (Input.GetKey(KeyCode.Q) && isCharging)
        {
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0, chargeTime); // Clamp to max charge
        }

        if (Input.GetKeyUp(KeyCode.Q) && isCharging)
        {
            ShootArrow();
            isCharging = false;
        }
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        float arrowSpeed = Mathf.Lerp(10, 30, currentCharge / chargeTime); // Speed based on charge
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetArrowProperties(arrowSpeed, currentCharge / chargeTime); // Set arrow damage and speed
        rb.velocity = arrowSpawnPoint.forward * arrowSpeed;
    }
}
