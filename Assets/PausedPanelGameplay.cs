using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausedPanelGameplay : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] ActionPlaySound actionPlaySound;

    private void OnEnable()
    {
        musicSlider.value = actionPlaySound.GetMusicVolume();
        sfxSlider.value = actionPlaySound.GetFxVolume();
    }
}
