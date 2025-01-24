using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Butona tıklanıldığında çalışacak fonksiyon
    public void QuitGame()
    {
        // Oyun editörde çalışıyorsa oyunu durdurur
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Oyun dışında çalışıyorsa uygulamayı kapatır
            Application.Quit();
        #endif
    }
}