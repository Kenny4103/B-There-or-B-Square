using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalTile : MonoBehaviour
{
    [SerializeField] private Text goalText; // Assign this in the prefab (world space text)
    [SerializeField] private int minGoalAmount = 50;
    [SerializeField] private int maxGoalAmount = 150;
    [SerializeField] private float triggerRadius = 0.5f;
    [SerializeField] private string nextLevel = "Level2";

    private int goalAmount;
    private bool isComplete = false;

    private static int totalGoals = 0;
    private static int completedGoals = 0;

    private void Awake()
    {
        totalGoals++;

        // Estimate goal value based on reasonable dollar tile collection
        goalAmount = Random.Range(minGoalAmount, maxGoalAmount + 1);

        if (goalText != null)
        {
            goalText.text = $"Deposit\n${goalAmount}";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isComplete) return;

        MoneyCollect moneyCollector = other.GetComponent<MoneyCollect>();
        if (moneyCollector != null && moneyCollector.TryDeposit(goalAmount))
        {
            isComplete = true;
            completedGoals++;

            if (goalText != null)
            {
                goalText.text = "Goal\nComplete!";
                goalText.color = Color.green;
            }

            if (completedGoals >= totalGoals)
            {
                Debug.Log("All goals completed! Proceed to next level.");
                LevelComplete();
            }
        }
    }

    private void LevelComplete()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void OnDestroy()
    {
        totalGoals--;
        if (isComplete)
            completedGoals--;
    }
}
