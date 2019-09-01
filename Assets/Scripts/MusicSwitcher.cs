using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSwitcher : MonoBehaviour
{
    public Button bttnMusicOn;
    public Button bttnMusicOff;
    public AudioSource music;

    public static float bgMusicLevel = 1F;

    void Start()
    {
        if (PlayerPrefs.HasKey("BgMusicLevel"))
            bgMusicLevel = PlayerPrefs.GetFloat("BgMusicLevel");
            music.enabled = bgMusicLevel > 0;
        OnEnable();
    }

    public void OnEnable()
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
        music.enabled = bgMusicLevel > 0;
        PlayerPrefs.Save();
    }
}
