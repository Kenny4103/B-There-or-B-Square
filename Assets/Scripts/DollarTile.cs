using UnityEngine;
using UnityEngine.UI;

public class DollarTile : MonoBehaviour
{
    [SerializeField] private Text valueText; // Assign via inspector
    public int DollarValue { get; private set; }

    private void Awake()
    {
        int[] dollarOptions = { 20, 40, 60, 80, 100, 110, 120 };
        DollarValue = dollarOptions[Random.Range(0, dollarOptions.Length)];

        if (valueText != null)
        {
            valueText.text = $"${DollarValue}";
        }
    }
}
