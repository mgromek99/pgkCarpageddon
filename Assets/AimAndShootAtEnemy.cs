using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum Element { FIRE, WATER, EARTH }

public class AimAndShootAtEnemy : MonoBehaviour
{
    // Public variables for the damage amount, range, and inaccuracy
    public int damageAmount = 20;
    public float raycastRange = 100f;
    public float inaccuracyDegrees = 5f;
    public Element turretElement;

    // Trail visibility
    public float trailTime = 0.3f;

    // Shooting downtime control
    public float shootingDowntime = 2f; // Time between shots in seconds
    private float lastShotTime;          // Timer to track the last shot

    // Effects for shooting, hit, and death
    public GameObject hitEffectPrefab;     // Prefab for hit effects (damage taken)
    public GameObject deathEffectPrefab;   // Prefab for death effect (destroyed)
    public LineRenderer rayEffect;         // LineRenderer for the shooting ray

    private HealthSystem healthSystem;

    void Update()
    {
        GameObject closestEnemy = FindClosestEnemy();

        if (closestEnemy != null && Time.time >= lastShotTime + shootingDowntime)
        {
            // Point towards the closest enemy
            Vector3 directionToEnemy = closestEnemy.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(directionToEnemy);

            // Try shooting with inaccuracy
            TryShootAtEnemy(closestEnemy);

            // Update the last shot time
            lastShotTime = Time.time;
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    void TryShootAtEnemy(GameObject enemy)
    {
        Vector3 inaccurateDirection = AddInaccuracyToDirection(transform.forward, inaccuracyDegrees);

        // Show raycast effect
        StartCoroutine(ShowRayEffect(transform.position, inaccurateDirection));

        RaycastHit hit;
        if (Physics.Raycast(transform.position, inaccurateDirection, out hit, raycastRange))
        {
            if (hit.collider.gameObject == enemy)
            {
                Debug.Log("Hit the enemy!");

                // Show hit effect
                Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);

                // Deal damage if the enemy has a HealthSystem
                healthSystem = enemy.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.TakeDamage(damageAmount, turretElement);
                }
            }
            else
            {
                Debug.Log("Missed the enemy.");
            }
        }
    }

    Vector3 AddInaccuracyToDirection(Vector3 direction, float degrees)
    {
        float randomYaw = Random.Range(-degrees / 2f, degrees / 2f);
        float randomPitch = Random.Range(-degrees / 2f, degrees / 2f);
        Quaternion inaccuracyRotation = Quaternion.Euler(randomPitch, randomYaw, 0);
        return inaccuracyRotation * direction;
    }

    // Coroutine to show the ray effect for a brief moment
    IEnumerator ShowRayEffect(Vector3 startPoint, Vector3 direction)
    {
        RaycastHit hit;
        Vector3 endPoint;

        if (Physics.Raycast(startPoint, direction, out hit, raycastRange))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = startPoint + direction * raycastRange;
        }

        // Set up the LineRenderer for the ray effect
        rayEffect.SetPosition(0, startPoint);
        rayEffect.SetPosition(1, endPoint);
        rayEffect.enabled = true;

        // Wait for a short duration to show the effect
        yield return new WaitForSeconds(trailTime);

        // Disable the ray effect
        rayEffect.enabled = false;
    }
}
