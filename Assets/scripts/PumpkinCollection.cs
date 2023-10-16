using UnityEngine;
using TMPro;

public class PumpkinCollection : MonoBehaviour
{
    public TextMeshProUGUI pumpkinText;
    public int totalPumpkins = 10;
    public float maxCollectionRange = 3f; 
    public int collectedPumpkins = 0;
    private bool isNearPumpkin = false;
    private Transform currentPumpkin; 

    private bool showCollectMessage = false;
    private float collectMessageDuration = 4f;
    private float collectMessageTimer = 0f;
    private bool showEKey = false; 

    private void Start()
    {
        UpdatePumpkinText();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxCollectionRange))
        {
            if (hit.collider.CompareTag("Pumpkin"))
            {
                isNearPumpkin = true;
                currentPumpkin = hit.transform;
            }
            else
            {
                isNearPumpkin = false;
                currentPumpkin = null;
            }
        }
        else
        {
            isNearPumpkin = false;
            currentPumpkin = null;
        }

        if (isNearPumpkin)
        {
            switch (Input.GetKeyDown(KeyCode.E))
            {
                case true:
                    if (!GameManager.Instance.IsPaused) 
                    {
                        collectedPumpkins++;
                        Destroy(currentPumpkin.gameObject); 
                        UpdatePumpkinText();

                        if (collectedPumpkins == totalPumpkins)
                        {
                            Debug.Log("collected all the pumpkins");

                            showCollectMessage = true;
                            collectMessageTimer = 0f;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        if (showCollectMessage)
        {
            collectMessageTimer += Time.deltaTime;
            if (collectMessageTimer >= collectMessageDuration)
            {
                showCollectMessage = false;
            }
        }
    }

    private void OnGUI()
    {
        if (showCollectMessage)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Nice! Now run to the staircase!");

        }

        if (showEKey)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 20, 20), "E");
        }
    }

    private void UpdatePumpkinText()
    {
        pumpkinText.text = $"{collectedPumpkins}/{totalPumpkins}";
    }
}
