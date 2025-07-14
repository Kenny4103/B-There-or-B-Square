using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstructions : MonoBehaviour
{
    [SerializeField] private Text instructionsText;   // UI Text for instructions
    [SerializeField] private Text moneyText;          // UI Text for money counter
    [SerializeField] private GridMovement gridMovement;
    [SerializeField] private MoneyCollect moneyCollect;
    [SerializeField] private float instructionDuration = 3f; // Seconds to show instructions

    private void Start()
    {
        StartCoroutine(ShowInstructionsRoutine());
    }

    private IEnumerator ShowInstructionsRoutine()
    {
        // Show instructions, hide money, disable movement
        instructionsText.gameObject.SetActive(true);
        moneyText.gameObject.SetActive(false);

        if (gridMovement != null)
            gridMovement.enabled = false;

        if (moneyCollect != null)
        {
            // Ensure money starts at $0
            typeof(MoneyCollect)
                .GetField("totalMoney", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(moneyCollect, 0);

            // Force update the display
            moneyCollect.SendMessage("UpdateMoneyDisplay", SendMessageOptions.DontRequireReceiver);
        }

        yield return new WaitForSeconds(instructionDuration);

        // Hide instructions, show money, enable movement
        instructionsText.gameObject.SetActive(false);
        moneyText.gameObject.SetActive(true);

        if (gridMovement != null)
            gridMovement.enabled = true;
    }
}
