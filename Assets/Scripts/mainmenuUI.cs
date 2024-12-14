using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenuUI : MonoBehaviour
{

    public TMP_FontAsset arvo; 
    public TMP_FontAsset bebasnue;
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject settings;
    public GameObject HowToPlay;
    public AudioClip buttonSound;
    public Slider volumeSlider;
    public Animator settingsAnimator;
    public Animator creditsAnimator;
    public AudioSource audio2;
    public Material arvoMaterial;
    public Material babasnueMaterial;

    public Animator htpanimator;
    public ScrollRect scrollRect;
    public float scrollSpeed = 70f;
    //public Animator settingsAnimator;

    public static bool isMuted = false;
    private RectTransform content;

    private Dictionary<TextMeshProUGUI, TextProperties> initialTextProperties = new Dictionary<TextMeshProUGUI, TextProperties>();

    // Start is called before the first frame update
    void Start()
    {
        audio2.mute=isMuted;
        mainMenu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
        HowToPlay.SetActive(false);

        
        volumeSlider.value = audio2.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
        content = scrollRect.content;
        //DontDestroyOnLoad(audio2.gameObject);
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            
            button.onClick.RemoveListener(playbuttonAudio);

        
            button.onClick.AddListener(playbuttonAudio);
        }

    }

    private void StoreInitialProperties()
    {
        // Find all TextMeshProUGUI elements and store their initial properties
        TextMeshProUGUI[] textElements = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var textElement in textElements)
        {
            initialTextProperties[textElement] = new TextProperties
            {
                fontAsset = textElement.font,
                material = textElement.fontMaterial,
                text = textElement.text,
                color = textElement.color,
                fontSize = textElement.fontSize
            };
        }
    }

    private struct TextProperties
    {
        public TMP_FontAsset fontAsset;
        public Material material;
        public string text;
        public Color color;
        public float fontSize;
    }
    public void SetFontArvo()
    {
        PlayerPrefs.SetString("SelectedFont", "Arvo");
        PlayerPrefs.SetString("SelectedMaterial","arvoMaterial");
        ApplyFont(arvo,arvoMaterial);
    }

    void OnEnable()
    {
        ApplySavedFont();
    }
    public void SetFontBebas()
    {
        PlayerPrefs.SetString("SelectedFont", "Bebas");
        PlayerPrefs.SetString("SelectedMaterial", "babasnueMaterial");
        ApplyFont(bebasnue,babasnueMaterial);
    }

    private void ApplyFont(TMP_FontAsset font, Material material)
    {
        // Apply font to any relevant UI elements in the current scene
        TextMeshProUGUI[] textElements = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var text in textElements)
        {
            text.font = font;
            text.material=material;
            
        }

        ApplySavedFont();
    }
    public void startAudio(){

        audio2.mute=false;
        isMuted=false;
        PlayerPrefs.SetString("audio","enabled");
    }

    public void ApplySavedFont()
    {
        string selectedFont = PlayerPrefs.GetString("SelectedFont", "Arvo");  // Default to Arvo
        string Selectedmaterial=PlayerPrefs.GetString("selectedMaterial","arvoMaterial");

        TMP_FontAsset fontToApply = selectedFont == "Bebas" ? bebasnue : arvo;
        Material material= selectedFont == "Bebas" ? babasnueMaterial : arvoMaterial;

        TextMeshProUGUI[] textElements = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var text in textElements)
        {
            text.font = fontToApply;
            text.material=material;
        }
    }

    public void stopAudio(){

        audio2.mute=true;
        isMuted=true;
        PlayerPrefs.SetString("audio", "disabled");
    }
    public void SetVolume(float value){

        audio2.volume = value;
        PlayerPrefs.SetFloat("Volume", value);

    }
    
    public void creditsbtnclicked(){

        mainMenu.SetActive(false);
        credits.SetActive(true);
        creditsAnimator.SetTrigger("slideIn");
    }

    public void settingsbtnclicked(){

        mainMenu.SetActive(false);
        settings.SetActive(true);
        settingsAnimator.SetTrigger("slideIn");

    }



    public void close_settings(){

        StartCoroutine(ReenableMainMenu());
        //ApplySavedFont();
        settingsAnimator.SetTrigger("slideOut");
    }

    public void close_htp()
    {

        StartCoroutine(ReenableMainMenuhtp());

        htpanimator.SetTrigger("slideout");
    }

    public void close_credits()
    {

        StartCoroutine(ReenableMainMenucred());

       creditsAnimator.SetTrigger("slideOut");
    }

    private IEnumerator ReenableMainMenu()
    {
        yield return new WaitForSeconds(1f); 
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    private IEnumerator ReenableMainMenucred()
    {
        yield return new WaitForSeconds(1f);
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }

    private IEnumerator ReenableMainMenuhtp()
    {
        yield return new WaitForSeconds(1f);
        HowToPlay.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void playbuttonAudio(){

        audio2.PlayOneShot(buttonSound);
    }
    public void HowToPlaybtnclicked(){

        mainMenu.SetActive(false);
        HowToPlay.SetActive(true);
        htpanimator.SetTrigger("slidein");
    }

    public void backbtnclicked(){

        HowToPlay.SetActive(false);
        credits.SetActive(false);
        settings.SetActive(false);
        mainMenu.SetActive(true);
      //  settingsAnimator.SetTrigger("hide_settings");
    }

    public void playbtnclicked(){

        SceneManager.LoadScene(1);
    }

    public void resetHighScore(){

        PlayerPrefs.SetString("highscore","0");
    }

    // Update is called once per frame
    void Update()
    {
      //  ApplySavedFont();

        content.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        


        if (content.anchoredPosition.y >= content.sizeDelta.y)
        {
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, -scrollRect.viewport.rect.height);
        }
    }
}
