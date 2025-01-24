using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;  // Singleton, aby ³atwo uzyskaæ dostêp do CoinManager

    public int coins = 1000;             // Domyœlna liczba monet na start
    public TextMeshProUGUI coinsText;    // Referencja do wyœwietlacza liczby monet (CoinsNum)

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
        UpdateCoinText();  // Ustawienie pocz¹tkowego wyœwietlania liczby monet
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