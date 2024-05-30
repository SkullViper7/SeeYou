using UnityEngine;

public class SpawnZoneObjects : MonoBehaviour
{
    public GameObject[] Items;

    [SerializeField]
    private float minX;

    [SerializeField]
    private float minZ;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float maxZ;

    [SerializeField]
    private float GroundLevel;

    public Vector2 SpawnItems()
    {
            //int randomIndex = Random.Range(0, grenade.Length);
            return new Vector2(Random.Range(minX, maxX), Random.Range(minZ, maxZ));
    }

    public void InstantiateEachItem(Vector2 _position, int _indexItem)
    {
        Vector3 randomSpawnPosition = new Vector3(_position.x, GroundLevel, _position.y);
        GameObject newItem = Instantiate(Items[_indexItem], randomSpawnPosition, Quaternion.identity);
        GameManager.Instance.Items.Add(newItem);
    }
}
