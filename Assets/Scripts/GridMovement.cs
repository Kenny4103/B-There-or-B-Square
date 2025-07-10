using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GridMovement : MonoBehaviour
{
    [SerializeField] private bool isRepeatedMovement = false;
    [SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private float gridSize = 1f;

    private bool isMoving = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Optional for top-down movement
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (!isMoving)
        {
            System.Func<KeyCode, bool> inputFunction = isRepeatedMovement ? Input.GetKey : Input.GetKeyDown;

            if (inputFunction(KeyCode.UpArrow))
                StartCoroutine(Move(Vector2.up));
            else if (inputFunction(KeyCode.DownArrow))
                StartCoroutine(Move(Vector2.down));
            else if (inputFunction(KeyCode.LeftArrow))
                StartCoroutine(Move(Vector2.left));
            else if (inputFunction(KeyCode.RightArrow))
                StartCoroutine(Move(Vector2.right));
        }
    }

    private IEnumerator Move(Vector2 direction)
    {
        isMoving = true;

        Vector2 startPosition = rb.position;
        Vector2 targetPosition = startPosition + (direction * gridSize);

        // Check for obstacle using Rigidbody2D.Cast
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitCount = rb.Cast(direction, hits, gridSize - 0.01f); // Small epsilon to prevent false positives
        if (hitCount > 0 && !hits[0].collider.isTrigger)
        {
            isMoving = false;
            yield break; // Cancel move if collision ahead
        }

        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, percent);
            rb.MovePosition(newPosition);
            yield return null;
        }

        rb.MovePosition(targetPosition);
        isMoving = false;
    }
}
