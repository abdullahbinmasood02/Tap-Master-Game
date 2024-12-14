using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class gameplayUI : MonoBehaviour
{
    public GameObject pause;

    public float vol=1f;
    public GameObject winscreen;
    public GameObject gameover;
    public Material defaultMaterial;
    public GameObject play;
    public TextMeshProUGUI highscoreViewer;
    public TMP_FontAsset bebasnue;


    public AudioSource audio1;

    public Material babasnueMaterial;
    public Material arvoMaterial;
    public TMP_FontAsset arvo;
    public TextMeshProUGUI counterViewer;

 

    public TextMeshProUGUI timeViewer;

    public gameplayMechanics mechanicsreference;
    
    // Start is called before the first frame update
    void Start()
    {
        ApplySavedFont();
        pause.SetActive(false);
        winscreen.SetActive(false);
        play.SetActive(true);
        gameover.SetActive(false);
        
        displayHighScore();
        audio1.volume=vol;
        ApplySavedFont();

        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        audio1.volume = savedVolume;

        if (PlayerPrefs.GetString("audio", "enabled") == "disabled")
        {

            audio1.volume = 0f;

        }

        else if (PlayerPrefs.GetString("audio", "enabled") == "enabled")
        {

            audio1.volume = 1f;
        }

    }

    void OnEnable()
    {
        ApplySavedFont();
    }

    public void ApplySavedFont()
    {
        string selectedFont = PlayerPrefs.GetString("SelectedFont", "Arvo");  // Default to Arvo
        string Selectedmaterial = PlayerPrefs.GetString("SelectedMaterial", "arvoMaterial");

        TMP_FontAsset fontToApply = selectedFont == "Bebas" ? bebasnue : arvo;
        Material material = selectedFont == "Bebas" ? babasnueMaterial : arvoMaterial;

        TextMeshProUGUI[] textElements = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var text in textElements)
        {
            text.font = fontToApply;
            text.material = material;
        }
    }

    public void displayHighScore(){

        highscoreViewer.text=PlayerPrefs.GetString("highscore","0");
    }


    public void pausebtnclicked(){

        play.SetActive(false);
        pause.SetActive(true);
    }

    public void mainmenubtnclicked(){

        Debug.Log("clicked");
        SceneManager.LoadScene(0);

    }

    public void gameWon(){

        play.SetActive(false);
        winscreen.SetActive(true);
    }

    public void gameovershow(){

        play.SetActive(false);
        gameover.SetActive(true);
    }
 

    public void resumebtnclicked(){

        
        pause.SetActive(false);
        
        play.SetActive(true);
    }

    public void updateTapCounter(){

        counterViewer.text=mechanicsreference.Counter.ToString();
        Debug.Log(mechanicsreference.Counter);

        
    }

    public void updateTimer(){

        timeViewer.text= mechanicsreference.seconds.ToString();
       
    }


    // Update is called once per frame
    void Update()
    {
        

   

     

       
        
    }
}
