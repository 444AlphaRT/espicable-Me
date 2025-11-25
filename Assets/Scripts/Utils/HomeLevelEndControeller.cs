using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeLevelEndController : MonoBehaviour
{
    public string hubSceneName = "HubScene";

    private bool canRestart = false;

    private void Start()
    {
        if (ResourceManager.Instance != null &&
            ResourceManager.Instance.HasCollectedAllMinions())
        {
            canRestart = true;
        }
    }

    private void Update()
    {
        if (!canRestart)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ResourceManager.Instance != null)
            {
                ResourceManager.Instance.ResetAllMinions();
            }

            SceneManager.LoadScene(hubSceneName);
        }
    }
}