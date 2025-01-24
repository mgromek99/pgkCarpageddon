using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;  // Singleton, aby �atwo uzyska� dost�p do CoinManager

    public int coins = 1000;             // Domy�lna liczba monet na start
    public TextMeshProUGUI coinsText;    // Referencja do wy�wietlacza liczby monet (CoinsNum)

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinText();  // Ustawienie pocz�tkowego wy�wietlania liczby monet
    }

    public bool CanAffordTower(int towerCost)
    {
        return coins >= towerCost;
    }

    public void DeductCoinsForTower(int towerCost)
    {
        if (CanAffordTower(towerCost))
        {
            coins -= towerCost;
            UpdateCoinText();
        }
    }

    public void AddCoins(int reward)
    {
        coins += reward;
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinsText.text = "$" + coins;
    }
}