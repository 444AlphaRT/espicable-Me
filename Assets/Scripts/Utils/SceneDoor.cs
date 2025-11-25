using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour
{
    [Tooltip("Name of the scene to load when the player uses this door.")]
    public string sceneToLoad = "HomeScene";

    [Tooltip("Key the player must press to use the door.")]
    public KeyCode interactKey = KeyCode.E;

    [Tooltip("Optional: tag used to identify the player object.")]
    public string playerTag = "Player";

    private bool playerIsInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerIsInside = true;
            Debug.Log("Player entered door trigger.");
            // Here you can show UI prompt like: 'Press E to enter'
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerIsInside = false;
            Debug.Log("Player left door trigger.");
            // Here you can hide the UI prompt
        }
    }

    private void Update()
    {
        if (!playerIsInside)
            return;

        if (Input.GetKeyDown(interactKey))
        {
            Debug.Log("Loading scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
