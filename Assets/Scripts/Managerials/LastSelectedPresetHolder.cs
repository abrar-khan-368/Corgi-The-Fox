using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LastLightPresetHolder", menuName = "Create LightPresetHolder")]
public class LastSelectedPresetHolder : ScriptableObject
{
    [SerializeField] private int lastPresetHolderIndex = 0;
    public GameObject selectedPlatform;

    public int LastPresetHolderIndex { get => lastPresetHolderIndex; set => lastPresetHolderIndex = value; }
}
