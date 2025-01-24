using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public enum StrengthOrWeakness { WEAK, NORMAL, STRONG }

public class HealthSystem : MonoBehaviour
{
    public int coinAmount;
    public int maxHealth;
    private int currentHealth;
    public Element element;

    // Visual effects
    public GameObject deathEffectPrefab;   // Prefab for death effect
    public GameObject hitEffectPrefab;     // Prefab for damage taken effect

    public delegate void OnDeath();
    public event OnDeath onDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public StrengthOrWeakness CheckElement(Element turretElement)
    {
        switch (turretElement)
        {
            case Element.FIRE:
                {
                    if (element == Element.FIRE) return StrengthOrWeakness.NORMAL;
                    else if (element == Element.WATER) return StrengthOrWeakness.WEAK;
                    else if (element == Element.EARTH) return StrengthOrWeakness.STRONG;
                    else return StrengthOrWeakness.NORMAL;
                }
            case Element.WATER:
                {
                    if (element == Element.FIRE) return StrengthOrWeakness.STRONG;
                    else if (element == Element.WATER) return StrengthOrWeakness.NORMAL;
                    else if (element == Element.EARTH) return StrengthOrWeakness.WEAK;
                    else return StrengthOrWeakness.NORMAL;
                }
            case Element.EARTH:
                {
                    if (element == Element.FIRE) return StrengthOrWeakness.WEAK;
                    else if (element == Element.WATER) return StrengthOrWeakness.STRONG;
                    else if (element == Element.EARTH) return StrengthOrWeakness.NORMAL;
                    else return StrengthOrWeakness.NORMAL;
                }
            default:
                {
                    return StrengthOrWeakness.NORMAL;
                }
        }
    }

    public void TakeDamage(int damageAmount, Element turretElement)
    {
        StrengthOrWeakness strengthOrWeakness = CheckElement(turretElement);

        if (strengthOrWeakness == StrengthOrWeakness.STRONG)
        {
            currentHealth -= 2 * damageAmount;
            Debug.Log(gameObject.name + " took " + damageAmount * 2 + " damage.");
        }
        else if (strengthOrWeakness == StrengthOrWeakness.WEAK)
        {
            currentHealth -= damageAmount / 2;
            Debug.Log(gameObject.name + " took " + damageAmount / 2 + " damage.");
        }
        else //strengthOrWeakness == StrengthOrWeakness.NORMAL or not defined
        {
            currentHealth -= damageAmount;
            Debug.Log(gameObject.name + " took " + damageAmount + " damage.");
        }

        // Show hit effect
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log(gameObject.name + " healed " + healAmount + " health.");
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");

        // Show death effect
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (onDeath != null)
        {
            onDeath.Invoke();
        }
        CoinManager.Instance.AddCoins(coinAmount);
        // Destroy the game object
        Destroy(gameObject);
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
}
