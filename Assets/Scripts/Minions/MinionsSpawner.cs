using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [Header("Minion Settings")]
    public GameObject minionPrefab;
    public int minionsToSpawn = 5;

    [Header("Screen Margin")]
    public float margin = 0.5f;

    private void Start()
    {
        SpawnMinions();
    }

    private void SpawnMinions()
    {
        if (minionPrefab == null)
        {
            Debug.LogWarning("MinionSpawner: minionPrefab is not assigned.");
            return;
        }

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("MinionSpawner: no Main Camera found.");
            return;
        }

        // Get world-space corners of the camera view
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0f, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1f, 1f, cam.nearClipPlane));

        float minX = bottomLeft.x + margin;
        float maxX = topRight.x - margin;
        float minY = bottomLeft.y + margin;
        float maxY = topRight.y - margin;

        // Spawn at most as many minions as the array can track
        int count = minionsToSpawn;
        if (ResourceManager.Instance != null)
        {
            count = Mathf.Min(count, ResourceManager.Instance.collectedMinions.Length);
        }

        for (int i = 0; i < count; i++)
        {
            // If this minion was already collected in a previous run, skip spawning it
            if (ResourceManager.Instance != null &&
                ResourceManager.Instance.collectedMinions[i])
            {
                continue;
            }

            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Vector2 spawnPos = new Vector2(x, y);

            GameObject newMinion = Instantiate(minionPrefab, spawnPos, Quaternion.identity);

            MinionStreetCollectible streetComponent = newMinion.GetComponent<MinionStreetCollectible>();
            if (streetComponent != null)
            {
                streetComponent.minionId = i;
            }
        }
    }
}