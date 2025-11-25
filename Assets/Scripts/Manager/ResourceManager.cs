using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [Header("Minion Storage")]
    public bool[] collectedMinions = new bool[5]; // minion IDs 0..4

    [Header("Runtime State")]
    public int totalMinionsToWin = 5;      // how many are needed to finish
    public int totalCollectedMinions = 0;  // total across all runs
    public int collectedThisRun = 0;       // how many collected in the current street visit

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // called when HubScene (street) is loaded
    public void ResetRunCounter()
    {
        collectedThisRun = 0;
    }

    // full restart after finishing level
    public void ResetAllMinions()
    {
        for (int i = 0; i < collectedMinions.Length; i++)
        {
            collectedMinions[i] = false;
        }

        totalCollectedMinions = 0;
        collectedThisRun = 0;
    }

    public bool HasCollectedAllMinions()
    {
        return totalCollectedMinions >= totalMinionsToWin;
    }

    // this is the key logic: one minion per run, no duplicates, max 5
    public bool TryCollectMinion(int minionId)
    {
        // already finished the level
        if (HasCollectedAllMinions())
            return false;

        // already took one minion in this street visit
        if (collectedThisRun >= 1)
            return false;

        // safety checks
        if (minionId < 0 || minionId >= collectedMinions.Length)
            return false;

        // minion already collected in a previous run
        if (collectedMinions[minionId])
            return false;

        // mark as collected
        collectedMinions[minionId] = true;
        totalCollectedMinions++;
        collectedThisRun++;

        return true;
    }
}