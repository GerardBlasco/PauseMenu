using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreferencesController : SaveableObject
{
    AudioManager audioManager;

    [SerializeField] bool isFullscreen;
    Resolution[] resolutions;
    int selectedResolution; 
    List<Resolution> selectedResolutionList = new List<Resolution>();

    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Toggle toggle;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    private void Awake()
    {
        dropdown.value = 16;
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        resolutions = Screen.resolutions;
        List<string> resolutionsList = new List<string>();
        string newRes;
        foreach (Resolution r in resolutions)
        {
            newRes = r.width.ToString() + " x " + r.height.ToString();
            if (!resolutionsList.Contains(newRes))
            {
                resolutionsList.Add(newRes);
                selectedResolutionList.Add(r);
            }
        }

        dropdown.AddOptions(resolutionsList);

        ChangeScreenMode();
    }

    public void ChangeScreenMode()
    {
        isFullscreen = toggle.isOn;
        Debug.Log("Screen changed to" + Screen.fullScreen);
        ChangeResolution();
    }

    public void ChangeResolution()
    {
        selectedResolution = dropdown.value;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, 
            selectedResolutionList[selectedResolution].height, isFullscreen);
    }

    public void ChangeMusicVolume()
    {
        audioManager.musicSource.volume = musicSlider.value;
    }

    public void ChangeEffectsVolume()
    {
        audioManager.SFXSource.volume = SFXSlider.value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public override object CaptureData()
    {
        return data = new PreferencesData(this);
    }

    public override void RestoreData(object data)
    {
        PreferencesData d = (PreferencesData)data;
        musicSlider.value = d.GetMusicVolume();
        SFXSlider.value = d.GetSFXVolume();
        toggle.isOn = d.GetFullscreen();
        dropdown.value = d.GetResolutionKey();
    }

    public float GetMusicVolume()
    {
        return musicSlider.value;
    }

    public float GetSFXVolume()
    {
        return SFXSlider.value;
    }

    public bool GetFullscreen()
    {
        return isFullscreen;
    }

    public int GetResolutionKey()
    {
        return dropdown.value;
    }
}
