using System.Threading.Tasks;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiPlayer = new GameObject[0];

    private GameObject uiTransitionHunter;
    private GameObject uiTransitionPrey;

    private PlayerMain playerMain;

    public void InitPlayerMain(PlayerMain _PM)
    {
        playerMain = _PM;
        _PM.PlayerUI = this;
        uiTransitionHunter = GameManager.Instance.UiTransitionHunter;
        uiTransitionPrey = GameManager.Instance.UiTransitionPrey;
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

    public async void TransitionUI()
    {
        if (playerMain.playerNetwork.IsOwner) 
        {
            await Task.Delay(100);
            uiTransitionHunter.transform.parent.gameObject.SetActive(true);
            playerMain.playerInputs.InTransition = true;
            if (playerMain.IsHunter)
            {
                HunterUI();
            }
            else
            {
                PreyUI();
            }

            await Task.Delay(3500);
            uiTransitionHunter.transform.parent.gameObject.SetActive(false);
            playerMain.playerInputs.InTransition = false;
        }
    }

    private void PreyUI()
    {
        uiTransitionHunter.SetActive(false);
        uiTransitionPrey.SetActive(true);
    }

    private void HunterUI()
    {
        uiTransitionHunter.SetActive(true);
        uiTransitionPrey.SetActive(false);

    }

}
