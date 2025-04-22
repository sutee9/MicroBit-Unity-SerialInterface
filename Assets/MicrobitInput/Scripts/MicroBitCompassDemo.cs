using TMPro;
using UnityEngine;

public class MicroBitCompassDemo : MonoBehaviour
{

    [Header("Setup")]
    public Transform compassFingerRoot;
    public TextMeshProUGUI compassHeadingText;

    // Update is called once per frame
    void Update()
    {
        compassFingerRoot.localRotation = Quaternion.Euler(0f, 0f, MicroBit.Input.compassHeading);

        compassHeadingText.text = $"{MicroBit.Input.compassHeading}";
    }
}
