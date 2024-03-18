using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text score1Text;

    private void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
        score1Text.text = PlayerPrefs.GetInt("score1").ToString();

    }


    public void JugarA(string name)
    {
        SceneManager.LoadScene(name);


    }

    public void Salir()
    {
        Application.Quit();
    }
}
