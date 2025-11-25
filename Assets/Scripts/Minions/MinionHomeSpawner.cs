using UnityEngine;

public class MinionHomeSpawner : MonoBehaviour
{
    [Header("Home Minion Settings")]
    public GameObject homeMinionPrefab;
    public float margin = 0.5f;

    private void Start()
    {
        SpawnHomeMinions();
    }

    private void SpawnHomeMinions()
    {
        if (ResourceManager.Instance == null)
        {
            Debug.LogWarning("MinionHomeSpawner: ResourceManager.Instance is null.");
            return;
        }

        if (homeMinionPrefab == null)
        {
            Debug.LogWarning("MinionHomeSpawner: homeMinionPrefab is not assigned.");
            return;
        }

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("MinionHomeSpawner: no Main Camera found.");
            return;
        }

        // Camera bounding box in world units
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0f, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1f, 1f, cam.nearClipPlane));

        float minX = bottomLeft.x + margin;
        float maxX = topRight.x - margin;
        float minY = bottomLeft.y + margin;
        float maxY = topRight.y - margin;

        // Spawn only minions that were actually collected
        bool[] collected = ResourceManager.Instance.collectedMinions;

        for (int i = 0; i < collected.Length; i++)
        {
            if (!collected[i])
                continue;

            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Vector2 spawnPos = new Vector2(x, y);

            Instantiate(homeMinionPrefab, spawnPos, Quaternion.identity);
        }
    }
}