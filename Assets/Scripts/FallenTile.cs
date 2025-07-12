using UnityEngine;
using UnityEngine.SceneManagement;

public class FallenTile : MonoBehaviour
{
    [SerializeField] private float restartDelay = 1f;
    [SerializeField] private float activationDelay = 0.2f; // Delay before trigger becomes active

    private bool isActive = false;

    private void Start()
    {
        // Wait before enabling the trigger effect
        Invoke(nameof(ActivateTile), activationDelay);
    }

    private void ActivateTile()
    {
        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(RestartLevelAfterDelay());
        }
    }

    private System.Collections.IEnumerator RestartLevelAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
