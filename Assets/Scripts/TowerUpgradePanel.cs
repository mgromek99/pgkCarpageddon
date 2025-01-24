using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradePanel : MonoBehaviour
{
    public Button path1Level1;
    public Button path1Level2;
    public Button path2Level1;
    public Button path2Level2;
    public Button path3Level1;
    public Button path3Level2;

    private Tower linkedTower; // Powi¹zana wie¿a

    void Start()
    {
        // Ukrycie panelu, jeœli zosta³ przypadkowo w³¹czony
        gameObject.SetActive(false);
    }

    public void Initialize(Tower tower)
    {
        linkedTower = tower;

        // Przypisz akcje dla przycisków
        path1Level1.onClick.AddListener(() => UpgradePath(1, 1));
        path1Level2.onClick.AddListener(() => UpgradePath(1, 2));
        path2Level1.onClick.AddListener(() => UpgradePath(2, 1));
        path2Level2.onClick.AddListener(() => UpgradePath(2, 2));
        path3Level1.onClick.AddListener(() => UpgradePath(3, 1));
        path3Level2.onClick.AddListener(() => UpgradePath(3, 2));
    }

    private void UpgradePath(int path, int level)
    {
        if (linkedTower == null)
        {
            Debug.LogError("Nie przypisano wie¿y do panelu ulepszeñ.");
            return;
        }

        switch (path)
        {
            case 1:
                if (linkedTower.levelPath1 < level)
                {
                    linkedTower.levelPath1 = level;
                    Debug.Log($"Wie¿a ulepszona: Œcie¿ka 1, Poziom {level}");
                }
                break;

            case 2:
                if (linkedTower.levelPath2 < level)
                {
                    linkedTower.levelPath2 = level;
                    Debug.Log($"Wie¿a ulepszona: Œcie¿ka 2, Poziom {level}");
                }
                break;

            case 3:
                if (linkedTower.levelPath3 < level)
                {
                    linkedTower.levelPath3 = level;
                    Debug.Log($"Wie¿a ulepszona: Œcie¿ka 3, Poziom {level}");
                }
                break;
        }
    }
}