using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] Sprite speakerMuteSprite;
    [SerializeField] Sprite VibrationMuteSprite;
    [SerializeField] Sprite MusicMuteSprite;

    [SerializeField] Sprite speakerSprite;
    [SerializeField] Sprite VibrationSprite;
    [SerializeField] Sprite MusicSprite;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] Image musicImage;
    [SerializeField] Image sfxImage;

    [SerializeField] Button vibrationOn;
    [SerializeField] Button vibrationOff;

    [SerializeField] ActionPlaySound playSound;

    private void OnEnable()
    {
        sfxSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.AddListener((val) => 
        { 
            playSound.SetFXVolume(val);
            SetIcons();
        });

        musicSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.AddListener((val) => {
            SetIcons();
            playSound.SetMusicVolume(val);
        });

        vibrationOff.onClick.RemoveAllListeners();
        vibrationOff.onClick.AddListener(() => {
            Debug.Log("Vibrate");
            HapticPatterns.PlayConstant(0.4f, 0.4f, 1.01f);
        });

        sfxSlider.value = playSound.GetFxVolume();
        musicSlider.value = playSound.GetMusicVolume();
    }

    public void MuteSound()
    {
        sfxSlider.value = 0;
        playSound.SetFXVolume(0);
        SetIcons();
    }
    public void MuteMusic()
    {
        musicSlider.value = 0;
        playSound.SetMusicVolume(0);
        SetIcons();
    }

    private void SetIcons()
    {
        if (musicSlider.value <= 0)
        {
            musicImage.sprite = MusicMuteSprite;
        }
        else
        {
            musicImage.sprite = MusicSprite;
        }

        if (sfxSlider.value <= 0)
        {
            sfxImage.sprite = speakerMuteSprite;
        }
        else
        {
            sfxImage.sprite = speakerSprite;
        }
    }
}
