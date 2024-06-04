using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject P;

    private void Start()
    {
        P = GameManager.Instance.PauseManager;
        P.transform.Find("Resume").GetComponent<Button>().onClick.AddListener(DesactivateUI);
    }

    public void OnPause()
    {
        if (P == null)
        {
            P = GameManager.Instance.PauseManager;
        }

        if (P != null)
        {
            if (P.activeSelf)
            {
                DesactivateUI();
            }
            else
            {
                P.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void DesactivateUI()
    {
        P.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
