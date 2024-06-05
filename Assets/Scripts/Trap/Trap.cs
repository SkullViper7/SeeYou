using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private float delay;

    private bool activate;

    private void Start()
    {
        if (delay == 0) 
        {
            delay = 5;
        }

        Invoke("Activate", delay);    
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (activate) 
        {
            if (other.CompareTag("Prey"))
            {
                other.SendMessage("TriggerTrap", this);
            }
        }
    }

    public virtual void TriggerEvent()
    {

    }

    private void ActivateTheTrap() 
    {
        activate = true;
    }
}
