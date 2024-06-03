using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject Hunter;
    public void InitBullet(GameObject _hunter)
    {
        Hunter = _hunter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prey" && other != Hunter)
        {
            other.SendMessage("Collision");
            Destroy(gameObject);
        }
    }
}
