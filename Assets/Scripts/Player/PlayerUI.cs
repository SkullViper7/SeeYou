using System.Threading.Tasks;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiPlayer = new GameObject[0];

    private GameObject uiTransitionHunter;
    private GameObject uiTransitionPrey;

    private PlayerMain playerMain;

    [SerializeField]
    private GameObject playerPseudoVisuel;

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
            playerMain.playerMovement.GetComponent<CharacterController>().enabled = false;
            if (playerMain.IsHunter)
            {
                HunterUI();
            }
            else
            {
                PreyUI();
            }

            await Task.Delay(2500);

            if (playerMain.IsHunter)
            {
                await Task.Delay(1000);
            }
            
            uiTransitionHunter.transform.parent.gameObject.SetActive(false);
            playerMain.playerMovement.GetComponent<CharacterController>().enabled = true;
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
