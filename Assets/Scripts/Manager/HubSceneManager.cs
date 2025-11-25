using UnityEngine;

public class HubSceneManager : MonoBehaviour
{
    private void Start()
    {
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.ResetRunCounter();
        }
    }
}