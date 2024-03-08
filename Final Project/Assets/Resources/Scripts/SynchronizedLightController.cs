using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizedLightController : MonoBehaviour
{
    public Light[] lights; // Array to hold the lights to be synchronized
    public bool canFlicker = true; // Toggle this to enable/disable flickering
    public float minTime = 0.1f;
    public float maxTime = 0.5f;

    private float timer;

    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        if (!canFlicker)
        {
            return; // If flickering is disabled, do nothing
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Toggle the state of all lights
            foreach (var light in lights)
            {
                if (light != null) // Check if the light reference is not null
                {
                    light.enabled = !light.enabled;
                }
            }

            // Reset the timer
            timer = Random.Range(minTime, maxTime);
        }
    }
}
