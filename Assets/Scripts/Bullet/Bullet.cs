using UnityEngine;

public class Bullet : MonoBehaviour
{
  private GameObject Hunter;
  public void InitBullet(GameObject _hunter)
  {
    Hunter = _hunter;
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Player" && collision.gameObject != Hunter)
    {
        collision.gameObject.SendMessage("Collision");
        Destroy(gameObject);
    }
  }
}
