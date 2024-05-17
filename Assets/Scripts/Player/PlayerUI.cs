using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiPlayer = new GameObject[0];

    public void InitPlayerMain(PlayerMain _PM)
    {
        uiPlayer = GameObject.FindGameObjectsWithTag("UIToRemove");
        RemoveUI();
    }

    public void RemoveUI()
    {
        foreach (GameObject ui in uiPlayer)
        {
            ui.SetActive(false);
        }
    }

}
