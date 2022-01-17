using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatformSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private LastSelectedPresetHolder lastSelectedPreset;

    public static RandomPlatformSelector instance { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            lastSelectedPreset.LastPresetHolderIndex = 0;
        }
        
        GameObject gameObject = platforms[GetUniquePresetIndex()];
        gameObject.SetActive(true);
        lastSelectedPreset.selectedPlatform = gameObject;
    }

    private int GetUniquePresetIndex()
    {
        if (platforms.Count <= 1)
            return 0;

        int x = lastSelectedPreset.LastPresetHolderIndex;
        while (x == lastSelectedPreset.LastPresetHolderIndex)
        {
            x = Random.Range(0, platforms.Count);
        }

        lastSelectedPreset.LastPresetHolderIndex = x;
        return x;

    }
}
