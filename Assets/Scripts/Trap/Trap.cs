using UnityEngine;

public class Trap : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prey"))
        {
            other.SendMessage("TriggerTrap", this);
        }       
    }

    public virtual void TriggerEvent()
    {

    }
}
