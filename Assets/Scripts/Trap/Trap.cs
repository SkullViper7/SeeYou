using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private float delay;

    private bool activate;

    protected virtual void Start()
    {
        if (delay == 0) 
        {
            delay = 5;
        }

        Invoke("ActivateTheTrap", delay);    
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
