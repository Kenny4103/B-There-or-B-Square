using System.Collections;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    // Allows you to hold down a key for movement.
    [SerializeField] private bool isRepeatedMovement = false;
    // Time in seconds to move between one grid position and the next.
    [SerializeField] private float moveDuration = 0.1f;
    // The size of the grid
    [SerializeField] private float gridSize = 1f;
    private Rigidbody2D _rb;
    private bool isMoving = false;

    // Update is called once per frame
    private void Update()
    {
        // Only process on move at a time.
        if (!isMoving)
        {
            // Accomodate two different types of moving.
            System.Func<KeyCode, bool> inputFunction;
            if (isRepeatedMovement)
            {
                // GetKey repeatedly fires.
                inputFunction = Input.GetKey;
            }
            else
            {
                // GetKeyDown fires once per keypress
                inputFunction = Input.GetKeyDown;
            }

            // If the input function is active, move in the appropriate direction.
            if (inputFunction(KeyCode.UpArrow))
            {
                StartCoroutine(Move(Vector2.up));
            }
            else if (inputFunction(KeyCode.DownArrow))
            {
                StartCoroutine(Move(Vector2.down));
            }
            else if (inputFunction(KeyCode.LeftArrow))
            {
                StartCoroutine(Move(Vector2.left));
            }
            else if (inputFunction(KeyCode.RightArrow))
            {
                StartCoroutine(Move(Vector2.right));
            }
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Smooth movement between grid positions.
    private IEnumerator Move(Vector2 direction)
    {
        isMoving = true;

        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize);

        // Cast a box to check for collisions
        RaycastHit2D hit = Physics2D.BoxCast(startPosition, _rb.GetComponent<BoxCollider2D>().size, 0f, direction, gridSize, LayerMask.GetMask("Obstacle"));

        if (hit.collider != null)
        {
            // Hit a wall or obstacle, cancel move
            isMoving = false;
            yield break;
        }

        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return null;
        }

        transform.position = endPosition;
        isMoving = false;
    }
}
