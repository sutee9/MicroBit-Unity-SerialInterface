using TMPro;
using UnityEngine;

public class MicroBitSoundDemo : MonoBehaviour
{
    [Header("Settings")]
    public float sizeMultiplier = 0.01f;

    [Header("Setup")]
    public Transform model;
    public TextMeshProUGUI soundLevelUI;

    // Update is called once per frame
    void Update()
    {
        float soundLevel = MicroBit.Input.soundLevel;
        model.localScale = new Vector3(soundLevel*sizeMultiplier,soundLevel*sizeMultiplier,soundLevel*sizeMultiplier);

        soundLevelUI.text = $"{Mathf.RoundToInt(soundLevel)}";
    }
}
