using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    public GameObject objectPrefab;            // Prefab obiektu
    private GameObject previewObject;          // Obiekt pod��aj�cy za kursorem
    public Button myButton;                    // Przycisk do rozpocz�cia umieszczania
    private bool isPlacing = false;            // Flaga kontroluj�ca stan umieszczania obiektu
    private Vector3[,] gridPoints;             // Tablica punkt�w siatki 10x10

    void Start()
    {
        // Ustawienie s�uchacza na przycisku
        myButton.onClick.AddListener(StartPlacingObject);

        // Inicjalizacja punkt�w siatki 10x10 z odst�pem 1 jednostki
        int gridWidth = 10;
        int gridHeight = 10;
        float gridSpacing = 1f;
        gridPoints = new Vector3[gridWidth, gridHeight];

        // Wype�nienie tablicy punktami siatki
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float posX = x * gridSpacing - 5;  // Pozycja X
                float posZ = y * gridSpacing - 5;  // Pozycja Z
                gridPoints[x, y] = new Vector3(posX, 0, posZ);  // �rodek ka�dego pola
            }
        }
    }

    void Update()
    {
        if (isPlacing)
        {
            FollowCursor();

            // Umieszczenie obiektu po klikni�ciu na odpowiednie pole
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PlaceObject();
            }
        }
    }

    public void StartPlacingObject()
    {
        // Je�li obiekt ju� jest w trakcie umieszczania, usu� go
        if (isPlacing)
        {
            CancelPlacement();
            return;
        }

        // Rozpocznij umieszczanie obiektu
        previewObject = Instantiate(objectPrefab);
        previewObject.GetComponent<Renderer>().material.color = Color.green; // Zmiana koloru dla obiektu pod��aj�cego
        isPlacing = true;
    }

    private void FollowCursor()
    {
        // Raycast, aby uzyska� pozycj� kursora
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Znalezienie najbli�szego punktu siatki do pozycji kursora
            Vector3 closestGridPoint = GetClosestGridPoint(hit.point);
            previewObject.transform.position = closestGridPoint;
        }
    }

    private Vector3 GetClosestGridPoint(Vector3 position)
    {
        Vector3 closestPoint = gridPoints[0, 0];
        float minDistance = Vector3.Distance(position, closestPoint);

        // Szukanie najbli�szego punktu siatki
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
        // Umieszcza obiekt i resetuje flag� isPlacing
        previewObject.GetComponent<Renderer>().material.color = Color.white; // Reset koloru
        previewObject = null;  // Ustawienie previewObject na null
        isPlacing = false;
    }

    private void CancelPlacement()
    {
        // Usuni�cie obiektu pod��aj�cego, je�li przerwano umieszczanie
        Destroy(previewObject);
        previewObject = null;
        isPlacing = false;
    }
}