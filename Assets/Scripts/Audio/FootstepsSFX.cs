using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSFX : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> _grassSFX;
    [SerializeField]
    List<AudioClip> _gravelSFX;
    [SerializeField]
    List<AudioClip> _mudSFX;

    [SerializeField]
    AudioSource _audioSource;

    TerrainDetector _terrainDetector;

    private void Awake()
    {
        _terrainDetector = new TerrainDetector();
    }

    public void Step()
    {
        AudioClip _clip = GetRandomClip();
        _audioSource.PlayOneShot(_clip);
        
    }

    AudioClip GetRandomClip()
    {
        int terrainTextureIndex = _terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        switch (terrainTextureIndex)
        {
            case 0:
            default:
                return _grassSFX[Random.Range(0, _grassSFX.Count)];
            case 1:
                return _gravelSFX[Random.Range(0, _gravelSFX.Count)];
            case 2:
                return _mudSFX[Random.Range(0, _mudSFX.Count)];
        }
    }
}