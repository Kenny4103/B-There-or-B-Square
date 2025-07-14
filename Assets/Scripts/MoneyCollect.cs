using UnityEngine;
using UnityEngine.UI;

public class MoneyCollect : MonoBehaviour
{
    [SerializeField] private Text moneyText; // Drag your top-left UI Text here in the Inspector

    private int totalMoney = 0;

    private void Start()
    {
        UpdateMoneyDisplay();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DollarTile dollarTile = other.GetComponent<DollarTile>();
        if (dollarTile != null)
        {
            totalMoney += dollarTile.DollarValue;
            UpdateMoneyDisplay();
            Destroy(other.gameObject);
        }
    }

    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Money Collected: ${totalMoney}";
        }
    }

    public bool TryDeposit(int amount)
    {
        if (totalMoney >= amount)
        {
            totalMoney -= amount;
            UpdateMoneyDisplay();
            return true;
        }
        return false;
    }
}
