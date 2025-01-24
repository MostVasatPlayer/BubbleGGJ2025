using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Başlangıç butonuna tıklandığında çağrılacak fonksiyon
    public void StartGame(string sceneName)
    {
        // Belirtilen sahneyi yükler
        SceneManager.LoadScene(sceneName);
    }
}