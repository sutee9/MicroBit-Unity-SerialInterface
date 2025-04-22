using TMPro;
using UnityEngine;

public class MicroBitRotationDemo : MonoBehaviour
{
    [Header("Setup")]
    public Transform model;
    public TextMeshProUGUI roll;
    public TextMeshProUGUI pitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AdjustRotation(MicroBit.Input.rotation);

        roll.text = $"{Mathf.RoundToInt(MicroBit.Input.rotation.x)}";
        pitch.text = $"{Mathf.RoundToInt(MicroBit.Input.rotation.y)}";
    }

    public void AdjustRotation(Vector2 rotation){
        model.localRotation = Quaternion.Euler(rotation.x, 0f, rotation.y);
    }
}
