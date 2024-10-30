using UnityEngine;

public class VibrationController : MonoBehaviour
{
    private void Start()
    {
        VibrateInLoop();
    }

    private void VibrateInLoop()
    {
        Debug.Log("Vibration not supported on this device.");
        //VibrateDevice(100);
        //Invoke(nameof(VibrateInLoop), 2f);
    }

    public void VibrateDevice(long milliseconds)
    {
        if (SystemInfo.supportsVibration)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            vibrator.Call("vibrate", milliseconds);
            Debug.Log("Device vibrated for " + milliseconds + " milliseconds");
        }
        else
        {
            Debug.Log("Vibration not supported on this device.");
        }
    }
}