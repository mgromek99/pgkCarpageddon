using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerClickHandler
{
    public GameObject towerUpgradePanelPrefab; // Prefab panelu ulepszeñ
    private GameObject instantiatedUpgradePanel; // Instancja panelu
    private Transform canvasTransform; // Canvas, w którym wyœwietlasz panel ulepszeñ
    private bool isUpgradesPanelOpen = false;
    public Transform Canvas; // Canvas, w którym wyœwietlasz panel

    // Statystyki wie¿y
    public int levelPath1 = 0;
    public int levelPath2 = 0;
    public int levelPath3 = 0;

    private GameObject activeUpgradePanel;


    private void Start()
    {
        // Automatyczne przypisanie Canvas
        if (canvasTransform == null)
        {
            canvasTransform = FindObjectOfType<Canvas>()?.transform;

            if (canvasTransform == null)
            {
                Debug.LogError("Nie znaleziono Canvas w scenie!");
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Klikniêto wie¿ê: " + gameObject.name);

        if (activeUpgradePanel == null)
        {
            // Tworzenie instancji panelu ulepszeñ
            activeUpgradePanel = Instantiate(towerUpgradePanelPrefab, Canvas);
            activeUpgradePanel.SetActive(true); // Uaktywnij panel
            activeUpgradePanel.transform.position = Input.mousePosition; // Ustaw pozycjê panelu
        }
        else
        {
            // Zamknij panel, jeœli ju¿ istnieje
            Destroy(activeUpgradePanel);
        }
    }


    // Mo¿esz dodaæ dodatkowe metody do zarz¹dzania ulepszeniami tutaj.
}