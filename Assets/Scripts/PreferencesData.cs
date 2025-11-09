using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PreferencesData : SaveData
{
    private float musicVolume;
    private float SFXVolume;
    private bool fullscreen;
    private int resolutionKey;

    public PreferencesData(PreferencesController preferences)
    {
        musicVolume = preferences.GetMusicVolume();
        SFXVolume = preferences.GetSFXVolume();
        fullscreen = preferences.GetFullscreen();
        resolutionKey = preferences.GetResolutionKey();

        Debug.Log(musicVolume + ", " + SFXVolume + ", " + fullscreen + ", " + resolutionKey);
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return SFXVolume;
    }

    public bool GetFullscreen()
    {
        return fullscreen;
    }

    public int GetResolutionKey()
    {
        return resolutionKey;
    }
}
