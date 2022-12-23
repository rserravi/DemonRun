using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    
    public int angelPoints;
    public int demonPoints;
    public int playerHealth;
    public int weaponHold;
   
    public ADBarController adBar;
    public SingleSliderControl healthBar;
    public SingleSliderControl weaponBar;

    public Image pauseImg;
    public Image playImg;

    public GameController.PlayState playState;

    public TextMeshProUGUI chapterTitle;

    public Image floatingScreen;
    public bool showingWeaponsInventory;
    public bool showingItemsInventory;
    public Button playPauseBtn;
    public Image blackDrop;
    public TextMeshProUGUI dieText;

    public WeaponsInventory weaponInventory;
    public ItemsInventory itemsInventory;
    public DialogSystem dialogSystem;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Button btn = playPauseBtn.GetComponent<Button>();
        btn.onClick.AddListener(PausePlayButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (angelPoints>20 || demonPoints > 20){
            adBar.SetMax(40);
        }
        if (angelPoints>40 || demonPoints > 40){
            adBar.SetMax(60);
        }
        if (angelPoints>60 || demonPoints > 60){
            adBar.SetMax(80);
        }
        if (angelPoints>80 || demonPoints > 80){
            adBar.SetMax(100);
        }
        adBar.SetAngelPoints(angelPoints);
        adBar.SetDemonPoints(demonPoints);
        healthBar.SetValue(playerHealth);
        weaponBar.SetValue(weaponHold);
        
        switch (playState)
        {
            case GameController.PlayState.playing:
                playImg.enabled = false;
                pauseImg.enabled = true;
                floatingScreen.enabled = false;

                break;
            case GameController.PlayState.pausemenu:
                playImg.enabled = true;
                pauseImg.enabled = false;
                floatingScreen.enabled = true;

                break;
            case GameController.PlayState.playdialog:
                break;
             case GameController.PlayState.dieDialog:
                break;
            
            default:
                break;
        }
        
        
        ShowWeapons(showingWeaponsInventory);
        ShowItems(showingItemsInventory);


    }

    public void PausePlayButton(){
       Debug.Log("PAUSING");
       GameController.instance.SetPausePlay();

    }
    public void ShowWeapons(bool show){
        weaponInventory.Show(show);
    }

    public void ShowItems(bool show){
        itemsInventory.Show(show);
    }

    public void ShowWeaponDialog(GameController.goodEvil side, ItemController item){
        WeaponClass weap = item.GetComponent<ItemController>().newItemPrefab.GetComponent<WeaponClass>();
        //Debug.Log(weap.weaponName);
        dialogSystem.ShowMessage(4f,item.side, "none", GameController.CharacterMood.normal, weap.weaponName, weap.WeaponDescription, weap.weaponIconMedium );
    }

    public void ShowDieDialog(string dialog){
        dieText.text = dialog;
        StartCoroutine(FadeBlack(5));
    }

    public IEnumerator FadeBlack(float fadeDuration){
        
        Color targetColor = new Color(0,0,0,1);
        Color initialColor = new Color (0,0,0,0);
        float elapsedTime = 0f;
        float startTime = Time.unscaledTime;
        while (elapsedTime < fadeDuration){
          
            elapsedTime = Time.unscaledTime - startTime;
            Debug.Log("ELAPSED TIME"+elapsedTime);
            Color newColor = Color.Lerp(initialColor, targetColor, elapsedTime/ fadeDuration);
            blackDrop.color = newColor;
            Debug.Log(newColor);
            yield return null;

           
        }
        Debug.Log("CORROUTINE HAS ENDED");
        GameController.instance.ResetAndRespawn();
    }

    public void ResetBackdrop(){
        dieText.text = "";
        blackDrop.color = new Color(0,0,0,0);
    }
}
