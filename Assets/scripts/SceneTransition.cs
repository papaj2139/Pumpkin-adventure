using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float interactionRange = 3f; 
    public Transform playerCamera; 
    public PumpkinCollection pumpkinCollection; 

    private void Update()
    {
        if (pumpkinCollection.collectedPumpkins == 10)
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                if (hit.collider.CompareTag("Staircase") && Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }
}
