using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    // Reference to enemy prefabs
    [SerializeField]
    private GameObject[] enemyReference;

    private GameObject spawnedEnemy;

    // Positions for spawning enemies on the left and right
    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomIndex;  // Random enemy type index
    private int altSide = -1;   // Alternates side (-1 for left, 1 for right), first will be right
    [SerializeField]
    private GameObject ammoReplenishPrefab;

    void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnedEnemy());
    }

    IEnumerator SpawnedEnemy()
    {
        // Infinite loop to spawn enemies continuously
        while (true)
        {
            // Wait for a random amount of time between spawns
            yield return new WaitForSeconds(Random.Range(3, 6));

            // Choose a random enemy and a random side
            randomIndex = Random.Range(0, enemyReference.Length);
            altSide *= -1;

            // Instantiate the enemy
            spawnedEnemy = Instantiate(enemyReference[randomIndex]);

            // Set size and direction
            Vector3 enemyScale = new Vector3(0.6f, 0.6f, 1f); // Uniform scale for all enemies

            // Get the SpriteRenderer component and set sorting order
            SpriteRenderer enemyRenderer = spawnedEnemy.GetComponent<SpriteRenderer>();

            if (enemyRenderer != null)
            {
                enemyRenderer.sortingOrder = 1; // Ensure it's rendered in Layer 1
            }

            // Spawn on the left side
            if (altSide == -1)
            {
                spawnedEnemy.transform.position = leftPos.position;
                spawnedEnemy.transform.localScale = enemyScale; // Normal direction
                spawnedEnemy.GetComponent<Enemy>().speed = Random.Range(2, 4); // Positive speed
            }
            // Spawn on the right side
            else
            {
                spawnedEnemy.transform.position = rightPos.position;
                spawnedEnemy.transform.localScale = new Vector3(-enemyScale.x, enemyScale.y, enemyScale.z); // Flipped horizontally
                spawnedEnemy.GetComponent<Enemy>().speed = -Random.Range(2, 4); // Negative speed
            }
            
            // === SPAWN AMMO REPLENISH ===
            // 30% chance to spawn one each cycle
            if (Random.value < 0.2f)
            {
                float randomX = Random.Range(-40, 70);
                Vector3 spawnPos = new Vector3(randomX, 0, 0f);
                Instantiate(ammoReplenishPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
