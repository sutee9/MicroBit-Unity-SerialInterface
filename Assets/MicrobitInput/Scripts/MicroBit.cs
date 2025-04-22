using System.Numerics;
using UnityEditor.Rendering;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Unity.VisualScripting;
using UnityEngine.Events;
using System;

public class MicroBit : MonoBehaviour
{
    public static MicroBit Input
    {
        get
        {
            if (_input == null)
            {
                ReassignReference();
            }
            return _input;
        }
    }
    private static MicroBit _input;

    [Header("Settings")]

    [Tooltip("If acceleration is too unsteady, activate this. Will impair/delay detection of fast immediate movements.")]
    public bool smoothAcceleration = true;
    [Tooltip("If rotation seems, activate this. Will impair/delay detection of fast immediate movements.")]
    public bool smoothRotation = true;

    [Tooltip("MicroBit only delivers temperature as integer number. For faking a smooth temperature adjustment, activate this")]
    public bool smoothTemperature = true;
    
    [Range(10, 1024)]
    public float loudSoundThreshold = 128f;
    public bool soundLevelSmoothing = false;

    [Space]
    public bool logButtonPresses = false;

    [Header("Raw Input Visualization")]
    public bool buttonA = false;
    public bool buttonB = false;
    public bool buttonLogo = false;

    public Vector3 acceleration = new Vector3();

    public Vector2 rotation = new Vector2();

    public float soundLevel;
    public bool loudSound; 

    public float temperature = 0f;

    public float lightLevel = 0f;

    public float compassHeading = 0f;

    [Header("Events (Hover for explanations)")]
    [Tooltip("When Button A is pressed. Fires once, when pressed.")]
    public UnityEvent btnAPressed;

    [Tooltip("When Button B is pressed. Fires once, when pressed.")]
    public UnityEvent btnBPressed;

    [Tooltip("When Logo is pressed. Fires once, when pressed.")]
    public UnityEvent btnlogoPressed;

    [Tooltip("When Button A is released. Fires once, when released. To get current pressed-state, use field buttonA")]
    public UnityEvent btnAReleased;

    [Tooltip("When Button B is released. Fires once, when released. To get current pressed-state, use field buttonB")]
    public UnityEvent btnBReleased;

    [Tooltip("When Logo is released. Fires once, when released. To get current pressed-state, use property buttonLogo")]
    public UnityEvent btnlogoReleased;

    [Tooltip("When the soundLevel surpasses loud sound threshold. To get current state, use property loudSound")]
    public UnityEvent loudSoundStarted;

    [Tooltip("When the soundLevel falls below loud sound threshold. To get current state, use property loudSound")]
    public UnityEvent loudSoundEnded;

    [Space]
    [Tooltip("Current acceleration, smoothed if smoothing is enabled. Fires once per frame.")]
    public UnityEvent<Vector3> accelerationUpdated;

    [Tooltip("Current rotation, smoothed if smoothing is enabled. Fires once per frame.")]
    public UnityEvent<Vector3> rotationUpdated;

    void Awake()
    {
        _input = this;
    }

    /// <summary>
    /// Fire
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Reassigns a static reference to Input if any can be found.
    /// </summary>
    public static void ReassignReference()
    {
        if (_input == null)
        {
            _input = FindFirstObjectByType<MicroBit>();
        }
    }

    /**
     * ExpectsMicrobit to send following format:
     * 1) acceleratorX
     * 2) acceleratorY
     * 3) acceleratorZ
     * 4) buttonA
     * 5) buttonB
     * 6) buttonLogo
     * 7) rotationPitch
     * 8) rotationRoll
     * 9) soundLevel
     * 10) temperature
     * 11) lightLevel
     * 12) compass
     */
    public void ProcessInputMessage(string microBitInput)
    {

        //Debug.Log(microBitInput);
        string[] tokenizedMessage = microBitInput.Split(" ");

        if (tokenizedMessage.Length < 12)
        {
            Debug.Log("Micro:Bit Input message had too few parameters");
            return;
        }

        //ButtonA
        if (tokenizedMessage[3] == "1")
        {
            if (!buttonA)
            {
                if (logButtonPresses) Debug.Log("ButtonA pressed");
                btnAPressed.Invoke();
            }
            buttonA = true;
        }
        else
        {
            if (buttonA)
            {
                if (logButtonPresses) Debug.Log("ButtonA released");
                btnAReleased.Invoke();
            }
            buttonA = false;
        }

        //ButtonB
        if (tokenizedMessage[4] == "1")
        {
            if (!buttonB)
            {
                if (logButtonPresses) Debug.Log("ButtonB pressed");
                btnBPressed.Invoke();
            }
            buttonB = true;
        }
        else
        {
            if (buttonB)
            {
                if (logButtonPresses) Debug.Log("ButtonB released");
                btnBReleased.Invoke();
            }
            buttonB = false;
        }

        //Button Logo
        if (tokenizedMessage[5] == "1")
        {
            if (!buttonLogo)
            {
                if (logButtonPresses) Debug.Log("Logo pressed");
                btnlogoPressed.Invoke();
            }
            buttonLogo = true;
        }
        else
        {
            if (buttonLogo)
            {
                if (logButtonPresses) Debug.Log("Logo released");
                btnlogoReleased.Invoke();
            }
            buttonLogo = false;
        }

        //Acceleration
        acceleration.x = float.Parse(tokenizedMessage[0]);
        acceleration.y = float.Parse(tokenizedMessage[1]);
        acceleration.z = float.Parse(tokenizedMessage[2]);

        //Rotation
        if (smoothRotation)
        {
            rotation.x = Mathf.Lerp(rotation.x, -float.Parse(tokenizedMessage[6]), 0.2f);
            rotation.y = Mathf.Lerp(rotation.y, -float.Parse(tokenizedMessage[7]), 0.2f);
        }
        else
        {
            rotation.x = -float.Parse(tokenizedMessage[6]);
            rotation.y = -float.Parse(tokenizedMessage[7]);
        }

        //SoundLevel
        if (soundLevelSmoothing){
            soundLevel = Mathf.Lerp(soundLevel, float.Parse(tokenizedMessage[8]), 0.1f);
        }
        else {
            soundLevel = float.Parse(tokenizedMessage[8]);
        }
        if (soundLevel > loudSoundThreshold){
            if (!loudSound){
                loudSoundStarted.Invoke();
            }
            loudSound = true;
        }
        if (soundLevel < loudSoundThreshold){
            if (loudSound){
                loudSoundEnded.Invoke();
            }
            loudSound = false;
        }

        //Temperature 
        if (smoothTemperature)
        {

            temperature = Mathf.Lerp(temperature, float.Parse(tokenizedMessage[9]), Time.deltaTime);
        }
        else
        {
            temperature = float.Parse(tokenizedMessage[9]);
        }

        //Light Level
        lightLevel = float.Parse(tokenizedMessage[10]);

        //Compass Heading
        compassHeading = float.Parse(tokenizedMessage[11]);
    }
}
