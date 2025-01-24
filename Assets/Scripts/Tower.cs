using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerClickHandler
{
    public GameObject towerUpgradePanelPrefab; // Prefab panelu ulepsze�
    private GameObject instantiatedUpgradePanel; // Instancja panelu
    private Transform canvasTransform; // Canvas, w kt�rym wy�wietlasz panel ulepsze�
    private bool isUpgradesPanelOpen = false;
    public Transform Canvas; // Canvas, w kt�rym wy�wietlasz panel

    // Statystyki wie�y
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
        Debug.Log("Klikni�to wie��: " + gameObject.name);

        if (activeUpgradePanel == null)
        {
            // Tworzenie instancji panelu ulepsze�
            activeUpgradePanel = Instantiate(towerUpgradePanelPrefab, Canvas);
            activeUpgradePanel.SetActive(true); // Uaktywnij panel
            activeUpgradePanel.transform.position = Input.mousePosition; // Ustaw pozycj� panelu
        }
        else
        {
            // Zamknij panel, je�li ju� istnieje
            Destroy(activeUpgradePanel);
        }
    }


    // Mo�esz doda� dodatkowe metody do zarz�dzania ulepszeniami tutaj.
}