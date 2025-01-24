using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class restartGame : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {

        ReloadScene(); 
    }
    // Function to reload the current scene
    public void ReloadScene()
    {
        // Get the current active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
