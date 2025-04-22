using System;
using TMPro;
using UnityEngine;

public class MicroBitThermometerDemo : MonoBehaviour
{
    [Header("Settings")]
    

    [Range(0, 40)]
    public int minTemperature = 10;

    [Range(0, 40)]
    public int maxTemperature = 40;

    [Header("Setup")]
    public Transform temperatureIndicator;
    public Transform maxPoint;
    public Transform minPoint;
    public TextMeshProUGUI tempText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currTemp = MicroBit.Input.temperature;
        AdjustTemperature(currTemp);
    }

    public void AdjustTemperature(float temperature){
        tempText.text = Mathf.RoundToInt(temperature)+"°";
        
        temperature = Math.Max(temperature, minTemperature);
        temperature = Math.Min(temperature, maxTemperature);

        float range = maxTemperature-minTemperature;

        temperatureIndicator.position = Vector3.Lerp(minPoint.position, maxPoint.position, (temperature-minTemperature)/range);
    }
}
