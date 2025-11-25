using UnityEngine;

public class MinionStreetCollectible : MonoBehaviour
{
    public int minionId = 0;
    public float collectDistance = 1.2f;

    private Transform player;

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    private void Update()
    {
        if (player == null || ResourceManager.Instance == null)
            return;

        // check distance
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > collectDistance)
            return;

        // press F to collect
        if (Input.GetKeyDown(KeyCode.F))
        {
            bool collected = ResourceManager.Instance.TryCollectMinion(minionId);
            if (collected)
            {
                Destroy(gameObject);
            }
        }
    }
}