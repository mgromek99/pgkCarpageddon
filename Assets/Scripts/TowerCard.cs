using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TowerCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public GameObject objectPrefab; // Prefab przeci¹ganego obiektu
    private GameObject draggedObject;
    private Renderer objectRenderer;
    public GameObject towerInfoPanel;

    public int towerCost = 300;

    // Przyk³adowa macierz punktów siatki heksagonalnej (10x10)
    private Vector3[,] hexGrid = new Vector3[10, 10];
    private int gridWidth = 10;
    private int gridHeight = 10;
    private float hexSpacing = 1.5f; // Odleg³oœæ miêdzy punktami heksagonów
    //private Vector2Int gridSize = new Vector2Int(10, 10);  
    //private float cellSize = 1.0f;  
    //private Vector3 gridOffset;


    public Transform CanvasTransform;
    [SerializeField] public GameObject towerUpgradePanel;




    private void Start()
    {
        // Wy³¹czenie panelu informacyjnego na pocz¹tku
        if (towerInfoPanel != null)
        {
            towerInfoPanel.SetActive(false);
        }

        InitializeHexGrid();
    }

    // Inicjalizacja macierzy punktów heksagonalnych z przesuniêciem
    private void InitializeHexGrid()
    {
        // Wylicz przesuniêcie, aby siatka by³a wyœrodkowana
        Vector3 gridOffset = new Vector3(-gridWidth * hexSpacing * 0.5f, 0, -gridHeight * hexSpacing * 0.5f);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Ustalanie pozycji punktów siatki heksagonalnej
                float posX = x * hexSpacing;
                float posZ = y * hexSpacing * Mathf.Sqrt(3) / 2;

                // Przesuwanie co drugi wiersz o po³owê szerokoœci heksagonu
                if (y % 2 == 1)
                {
                    posX += hexSpacing * 0.5f;
                }

                hexGrid[x, y] = new Vector3(posX, 0, posZ) + gridOffset;
            }
        }
    }

    // Wywo³ywana przy klikniêciu, aby pokazaæ okienko
    public void OnPointerClick(PointerEventData eventData)
    {
        if (towerInfoPanel != null)
        {
            bool isActive = towerInfoPanel.activeSelf;
            towerInfoPanel.SetActive(!isActive); // Prze³¹cz widocznoœæ
        }
    }

    // Rozpoczêcie przeci¹gania
    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedObject = Instantiate(objectPrefab);

        if (draggedObject != null)
        {
            objectRenderer = draggedObject.GetComponent<Renderer>();

            if (objectRenderer == null)
            {
                Debug.LogError("Brak komponentu Renderer w draggedObject.");
            }

            //draggedObject.transform.localScale = objectPrefab.transform.localScale * 10; //SKALA OBIEKTU

        }
        else
        {
            Debug.LogError("Nie mo¿na utworzyæ draggedObject");
        }
    }

    // Pod¹¿anie za kursorem i snapowanie do siatki
    public void OnDrag(PointerEventData eventData)
    {
        if (draggedObject != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 worldPosition = ray.GetPoint(distance);

                // Snapowanie pozycji obiektu do siatki z przesuniêciem
                Vector3 snappedPosition = FindNearestHexPoint(worldPosition);
                draggedObject.transform.position = snappedPosition;

                // Ustawienie koloru w zale¿noœci od pozycji w siatce
                bool isInsideGrid = IsInsideHexGrid(snappedPosition);
                SetColor(isInsideGrid);
            }
        }
    }

    // Zakoñczenie przeci¹gania i umieszczenie obiektu
    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedObject != null)
        {
            // Sprawdzenie poprawnoœci miejsca
            Vector3 position = draggedObject.transform.position;
            bool isInsideGrid = IsInsideHexGrid(position);

            if (isInsideGrid && CoinManager.Instance.CanAffordTower(towerCost))
            {
                ResetColor();
                CoinManager.Instance.DeductCoinsForTower(towerCost);

                // Tworzenie wie¿y
                Tower towerScript = draggedObject.AddComponent<Tower>();
                towerScript.towerUpgradePanelPrefab = towerUpgradePanel; // Przypisanie prefab panelu ulepszeñ
                GameObject upgradesPanelInstance = Instantiate(towerUpgradePanel, CanvasTransform);
                upgradesPanelInstance.SetActive(false); // Ustaw panel na nieaktywny



                //// Tworzenie panelu ulepszeñ (sprawdŸ, czy prefab jest poprawnie przypisany)
                //if (towerUpgradePanel != null)
                //{
                //    GameObject upgradesPanelInstance = Instantiate(towerUpgradePanel, CanvasTransform);
                //    upgradesPanelInstance.SetActive(false); // Na pocz¹tku ukryty panel

                //    TowerUpgradePanel upgradePanelScript = upgradesPanelInstance.GetComponent<TowerUpgradePanel>();
                //    if (upgradePanelScript != null)
                //    {
                //        upgradePanelScript.Initialize(towerScript); // Przypisanie wie¿y do panelu
                //    }
                //    else
                //    {
                //        Debug.LogError("Prefab panelu ulepszeñ nie ma skryptu TowerUpgradePanel!");
                //    }

                //    towerScript.towerUpgradePanel = upgradesPanelInstance;
                //}
                //else
                //{
                //    Debug.LogError("towerUpgradePanel nie jest przypisany w Inspectorze!");
                //}

            }
            else
            {
                // Z³a pozycja lub brak œrodków
                Destroy(draggedObject);
                Debug.Log("Nie uda³o siê postawiæ wie¿y: brak œrodków lub niew³aœciwa pozycja.");
            }

            draggedObject = null;
        }
    }

    // Sprawdza, czy pozycja znajduje siê wewn¹trz siatki hexów
    private bool IsInsideHexGrid(Vector3 position)
    {
        position.y = 0; // Ignorowanie osi y siatki

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 gridPosition = hexGrid[x, y];
                gridPosition.y = 0; // Ignorowanie osi y siatki
                if (hexGrid[x, y] == position)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Znajduje najbli¿szy punkt heksagonalny do pozycji kursora
    private Vector3 FindNearestHexPoint(Vector3 position)
    {
        position.y = 0; // Ignorowanie osi y przy szukaniu najbli¿szego punktu
        Vector3 closestPoint = hexGrid[0, 0];
        closestPoint.y = 0;

        float closestDistance = Vector3.Distance(position, closestPoint);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 gridPosition = hexGrid[x, y];
                gridPosition.y = 0; // Ignorowanie osi y dla pozycji siatki

                float distance = Vector3.Distance(position, gridPosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = gridPosition;
                }
            }
        }
        closestPoint.y += 0.25f;         //WYSKOŒÆ PO£O¯ENIA MODELU
        Debug.Log("Snapped Position: " + closestPoint);
        return closestPoint;
    }




    // Ustawianie koloru obiektu w zale¿noœci od pozycji
    private void SetColor(bool isInsideGrid)
    {
        if (objectRenderer != null)
        {
            if (isInsideGrid && CoinManager.Instance.CanAffordTower(towerCost))
            {
                objectRenderer.material.color = Color.green;  // Zielony, jeœli wszystko jest w porz¹dku
            }
            else
            {
                objectRenderer.material.color = Color.red;  // Czerwony, jeœli brakuje œrodków lub pozycja jest z³a
            }
        }
        else
        {
            Debug.LogWarning("objectRenderer jest null w SetColor.");
        }
    }
    // Resetowanie koloru obiektu po umieszczeniu
    private void ResetColor()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Color.white;
        }
    }

    // Funkcja do pokazywania okna informacji po klikniêciu
    public void ShowTowerInfo()
    {
        if (towerInfoPanel != null)
        {
            towerInfoPanel.SetActive(true);  // Pokazuje okno informacji
        }
    }

    // Funkcja do umieszczania obiektu na odpowiedniej pozycji
    private void PlaceObject(Vector3 position)
    {
        Debug.Log("Obiekt umieszczony na pozycji: " + position);
    }
}