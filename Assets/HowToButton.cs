using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HowToButton : MonoBehaviour
{
    public GameObject defaultText;   // İlk başta görünen GameObject
    public GameObject howToText;     // Tıklandığında görünen GameObject

    public void ShowHowTo()
    {
        // Default GameObject'i gizle, HowTo GameObject'ini göster
        defaultText.SetActive(false);
        howToText.SetActive(true);
    }
}