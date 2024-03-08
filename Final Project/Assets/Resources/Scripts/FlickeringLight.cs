using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public bool canFlicker = true; // This is the checkbox to toggle flickering
    private Light flickerLight;
    public float minTime = 0.1f;
    public float maxTime = 0.5f;

    private float timer;

    void Start()
    {
        flickerLight = GetComponent<Light>();
        timer = GetNewTimer();
    }

    void Update()
    {
        if (!canFlicker)
        {
            return; // Skip the rest of the update if flickering is disabled
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            flickerLight.enabled = !flickerLight.enabled;
            timer = GetNewTimer();
        }
    }

    private float GetNewTimer()
    {
        return Random.Range(minTime, maxTime);
    }
}