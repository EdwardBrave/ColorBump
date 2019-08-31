using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSwitcher : MonoBehaviour
{
    public Button bttnMusicOn;
    public Button bttnMusicOff;

    public static float bgMusicLevel = 1F;

    public void Start()
    {
        if (PlayerPrefs.HasKey("BgMusicLevel"))
            bgMusicLevel = PlayerPrefs.GetFloat("BgMusicLevel");
        OnEnable();
    }

    void OnEnable()
    {
        if (bgMusicLevel > 0)
            bttnMusicOn.Select();
        else
            bttnMusicOff.Select();
    }

    public void isMusicEnabled(bool val)
    {
        bgMusicLevel = val ? 1F : 0F;
        PlayerPrefs.SetFloat("BgMusicLevel", bgMusicLevel);
        OnEnable();
        PlayerPrefs.Save();
    }
}
