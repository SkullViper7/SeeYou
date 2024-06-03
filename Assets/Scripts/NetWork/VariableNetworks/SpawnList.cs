using Unity.Netcode;
using UnityEngine;
using Unity.Collections;


[System.Serializable]
public struct SpawnList : INetworkSerializable
{
    public int id;
    public Vector3 position;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref id);
        serializer.SerializeValue(ref position);
    }

}
