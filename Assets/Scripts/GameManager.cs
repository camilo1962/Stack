using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Color32[] gameColors = new Color32[4];

    public GameObject previousTile;
    public GameObject currentTile;
    public GameObject tilePrefab;
    public int score = 1;
    public TMP_Text scoreText;
   
    public GameObject panelGameOver;

    public float speed = 0.01f;
    private AudioSource aud;
    
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            DealWithPlayerClick();
        }
        if(currentTile != null)
        {
            currentTile.transform.localScale += new Vector3(1, 0, 1) * speed;
        }
    }
    public void CambiarEscena(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Salir()
    {
        Application.Quit();
    }

    public void DealWithPlayerClick()
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        if (currentTile == null)
        {
            currentTile = Instantiate(tilePrefab, previousTile.transform.position + Vector3.up, previousTile.transform.rotation);
        }
        else
        {
            if(previousTile.transform.localScale.x > currentTile.transform.localScale.x)
            {
                Debug.Log("Detenido justo a tiempo");
                previousTile = currentTile;
                currentTile = Instantiate(tilePrefab, previousTile.transform.position + Vector3.up, previousTile.transform.rotation);
                ColorMesh( currentTile.GetComponent<MeshFilter>().mesh);
                foreach (AudioSource aud in audios)
                {
                    aud.Play();
                    
                }

                Camera.main.transform.position += Vector3.up;
                score++;
                scoreText.text = score.ToString();
            }
            else
            {
                Debug.Log("No lo suficientemente preciso");
                currentTile = null;
                if (PlayerPrefs.GetInt("score1") < score)
                    PlayerPrefs.SetInt("score1", score);
                panelGameOver.SetActive(true);
                foreach (AudioSource aud in audios)
                {
                    aud.Pause();
                    
                }
            }
            
        }

    }

    private void ColorMesh(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        Color32[] colors = new Color32[vertices.Length];
        float f = Mathf.Sin(score * 0.25f);

        for (int i = 0; i < vertices.Length; i++)
            colors[i] = Lerp4(gameColors[0], gameColors[1], gameColors[2], gameColors[3],f);

        mesh.colors32 = colors;
    }


    private  Color32 Lerp4 (Color32 a, Color32 b, Color32 c, Color32 d,float t)
    {
        if (t < 0.33f)
            return Color.Lerp(a, b, t / 0.33f);
        else if (t < 0.66f)
            return Color.Lerp(b, c, (t - 0.33f) / 0.33f);
        else
            return Color.Lerp(c, d, (t - 0.66f) / 0.33f);
    }
}
