using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Brightness Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;

    [Header("Fullscreen Setting")]
     [SerializeField] private Toggle fullScreenToggle;

    [Header("Sensitivity Setting")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;

    [Header("Invert Y Setting")]
    [SerializeField] private Toggle invertYToggle = null;

    private void Awake(){
        if(canUse){
            // if(PlayerPrefs.HasKey("masterVolume")){
            //     float localVolume = PlayerPrefs.GetFloat("masterVolume");

            //     volumeTextValue.text = localVolume.ToString("0.0");
            //     volumeSlider.value = localVolume;
            //     AudioListener.volume = localVolume;
            // }
            // else {
            //     menuController.ResetButton("Audio");
            // }
            if(PlayerPrefs.HasKey("masterFullscreen")){
                int localFullscreen = PlayerPrefs.GetInt("masterFullscreen");

                if(localFullscreen == 1){
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else{
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }
            if(PlayerPrefs.HasKey("masterBrightness")){
                float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                brightnessTextValue.text = localBrightness.ToString("0.0");
                brightnessSlider.value = localBrightness;
                //change actual game brightness
            }
            if(PlayerPrefs.HasKey("masterSen")){
                float localSensitivity = PlayerPrefs.GetFloat("masterSen");

                controllerSenTextValue.text = localSensitivity.ToString("0.0");
                controllerSenSlider.value = localSensitivity;
                menuController.mainControllerSen = Mathf.RoundToInt(localSensitivity);
            }
            if(PlayerPrefs.HasKey("masterInvertY")){
                if(PlayerPrefs.GetInt("masterInvertY") == 1){
                    invertYToggle.isOn = true;
                }
                else {
                    invertYToggle.isOn = false;
                }
            }
        }
    }
}
