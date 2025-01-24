using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    public GameObject objectPrefab;            // Prefab obiektu
    private GameObject previewObject;          // Obiekt pod¹¿aj¹cy za kursorem
    public Button myButton;                    // Przycisk do rozpoczêcia umieszczania
    private bool isPlacing = false;            // Flaga kontroluj¹ca stan umieszczania obiektu
    private Vector3[,] gridPoints;             // Tablica punktów siatki 10x10

    void Start()
    {
        // Ustawienie s³uchacza na przycisku
        myButton.onClick.AddListener(StartPlacingObject);

        // Inicjalizacja punktów siatki 10x10 z odstêpem 1 jednostki
        int gridWidth = 10;
        int gridHeight = 10;
        float gridSpacing = 1f;
        gridPoints = new Vector3[gridWidth, gridHeight];

        // Wype³nienie tablicy punktami siatki
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float posX = x * gridSpacing - 5;  // Pozycja X
                float posZ = y * gridSpacing - 5;  // Pozycja Z
                gridPoints[x, y] = new Vector3(posX, 0, posZ);  // Œrodek ka¿dego pola
            }
        }
    }

    void Update()
    {
        if (isPlacing)
        {
            FollowCursor();

            // Umieszczenie obiektu po klikniêciu na odpowiednie pole
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PlaceObject();
            }
        }
    }

    public void StartPlacingObject()
    {
        // Jeœli obiekt ju¿ jest w trakcie umieszczania, usuñ go
        if (isPlacing)
        {
            CancelPlacement();
            return;
        }

        // Rozpocznij umieszczanie obiektu
        previewObject = Instantiate(objectPrefab);
        previewObject.GetComponent<Renderer>().material.color = Color.green; // Zmiana koloru dla obiektu pod¹¿aj¹cego
        isPlacing = true;
    }

    private void FollowCursor()
    {
        // Raycast, aby uzyskaæ pozycjê kursora
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Znalezienie najbli¿szego punktu siatki do pozycji kursora
            Vector3 closestGridPoint = GetClosestGridPoint(hit.point);
            previewObject.transform.position = closestGridPoint;
        }
    }

    private Vector3 GetClosestGridPoint(Vector3 position)
    {
        Vector3 closestPoint = gridPoints[0, 0];
        float minDistance = Vector3.Distance(position, closestPoint);

        // Szukanie najbli¿szego punktu siatki
        for (int x = 0; x < gridPoints.GetLength(0); x++)
        {
            for (int y = 0; y < gridPoints.GetLength(1); y++)
            {
                float distance = Vector3.Distance(position, gridPoints[x, y]);
                if (distance < minDistance)
                {
                    closestPoint = gridPoints[x, y];
                    minDistance = distance;
                }
            }
        }
        return closestPoint;
    }

    private void PlaceObject()
    {
        // Umieszcza obiekt i resetuje flagê isPlacing
        previewObject.GetComponent<Renderer>().material.color = Color.white; // Reset koloru
        previewObject = null;  // Ustawienie previewObject na null
        isPlacing = false;
    }

    private void CancelPlacement()
    {
        // Usuniêcie obiektu pod¹¿aj¹cego, jeœli przerwano umieszczanie
        Destroy(previewObject);
        previewObject = null;
        isPlacing = false;
    }
}