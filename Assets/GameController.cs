using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum goodEvil {none, good, neutral, evil}; 
    public enum ItemType {health, weapon, dressitem};
    public enum PlayState {mainMenu, playing, pausemenu, playdialog, dieDialog, loading};
    public enum DialogType {tutorial, item, narration};
    public enum CharacterMood {normal, angry, happy, alert};
    public enum Facing {left, right, front, back};
    public enum DeadStyle {normal, eaten}


    public enum DamageType {normal, critical, fail};
    public static GameController instance;

    public LevelController _Level;
    public static GameObject _Player;
    public GameObject damagePointsPrefab;
    public GameObject PlayerPrefab;

    [Tooltip("String with unity level name")]
    public string RespawnLevel ="";
    public int RespawnPlace =0;

    [Header("UI")]

    public UiController uiController;
    public AudioSource _AudioSource;

    public Camera _Camera;
    public CinemachineStateDrivenCamera _VirtualCam;
    public Animator _CameraAnimator;

    private float originalCameraFov;

    public PlayState playState;
    public int angelPoints;
    public int devilPoints;
    public int playerHealth = 20;
    public int playerMaxHealth= 20;
    public int weaponHold = 9;

    public bool showingWeaponsInventory = false;
    public bool showingItemsInventory  = false;
    private CinemachineBrain brain;
    public string deadCause;
        


    // Singleton Initialization
    void Awake()
    {
        if (!GameController.instance)
        {  
            GameController.instance = this;
            SceneManager.LoadScene(RespawnLevel, LoadSceneMode.Single);
        }           
        else
        {
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);        
    }

    // Start is called before the first frame update
    void Start()
    {
        //LOADING LEVEL
       
        _Level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelController>();
        _Player =  Instantiate(PlayerPrefab, _Level.respawnPoints[RespawnPlace].position, Quaternion.identity);
        _AudioSource = GetComponent<AudioSource>();
        _Camera = Camera.main;
        brain = CinemachineCore.Instance.GetActiveBrain(0);
        _VirtualCam = brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineStateDrivenCamera>();
        _CameraAnimator = _VirtualCam.gameObject.GetComponent<Animator>();
        playState = PlayState.playing;

        //LEVEL LOADED;
    }

    private void Update() {
        if (!_Player){
           _Player= GameObject.FindGameObjectWithTag("Player");
        }
        if (!_Level){
           _Level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelController>();
        }

        Scene activeScn = SceneManager.GetActiveScene();
        if (activeScn.name != _Level.unityLevelString){
            _Level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelController>();
        }
        if (playerHealth <= 0 && playState!=PlayState.dieDialog){
            PlayerDie(DeadStyle.normal, _Player.transform);
        }
         
        
       UpdateUI();
        
    }

    private void UpdateUI(){
        uiController.angelPoints = angelPoints;
        uiController.demonPoints = devilPoints;
        uiController.playerHealth = playerHealth;
        uiController.weaponHold = weaponHold;
        uiController.chapterTitle.text = _Level.levelName;
        uiController.playState = playState;
        uiController.showingWeaponsInventory = showingWeaponsInventory;
        uiController.showingItemsInventory = showingItemsInventory;
        
    }
    
    public static void GetItem(ItemController item){
        Debug.Log("ADQUIRIDO "+item.name);
        if(item.getItemSound){
             instance._AudioSource.PlayOneShot(item.getItemSound, 0.5f);
           }
        if (item.itemType == ItemType.weapon){
           
            if (item.side == goodEvil.good){
                instance.angelPoints += item.sidePoints;
            }
            if (item.side == goodEvil.evil){
                instance.devilPoints += item.sidePoints;
            }
            _Player.GetComponent<WeaponsSystem>().AddWeapon(item.newItemPrefab);
            //TODO: SHOW DIALOG;
            instance.uiController.ShowWeaponDialog(item.side, item);
            instance.showingWeaponsInventory = true;
        }
        if (item.itemType == ItemType.health){
   
            if (instance.playerHealth+item.sidePoints> instance.playerMaxHealth){
                        instance.playerHealth = instance.playerMaxHealth;
                    }
            else{
                instance.playerHealth+=item.sidePoints;
            }
            instance.DamageMessage(item.sidePoints,item.transform,DamageType.critical);
        }

        if (item.itemType == ItemType.dressitem){
            if (item.side == goodEvil.good){
                    instance.angelPoints += item.sidePoints;
                }
            if (item.side == goodEvil.evil){
                    instance.devilPoints += item.sidePoints;
                }
            //_Player.GetComponent<InvetorySystem>().AddWeapon(item.newItemPrefab);
            instance.uiController.ShowWeaponDialog(item.side, item);
            //instance.showingItemsInventory = true;

            switch (item.itemName)
            {
                 
                case "Angel Collar":
                   
                    break;
                
                default:
                    break;
            }
        }
        
    }

    public void TakeDamage(int damage, Vector3 place, DamageType damageType){
        _Player.GetComponent<BasePlayerController>().TakeDamage(damage, place, damageType);
    }

    public void GiveDamage(int damage, Vector3 place, Enemy enemy, DamageType damageType){
        enemy.GetComponent<Enemy>().TakeDamage(damage, place, damageType);
    }

    public void DamageMessage(int damage, Transform place, DamageType damageType){
        //INSTANTIATE DAMAGE
        Vector3 newPos = place.position;
        float randX = Random.Range(-1,1);
        newPos.y = newPos.y +2;
        newPos.x = newPos.x +randX;
        var damagePointsObj = Instantiate(damagePointsPrefab,newPos, Quaternion.identity);
        damagePointsObj.GetComponent<FadingText>().SetText(damage.ToString(), damageType);
    }

    public void SetPausePlay(){
        if (playState == PlayState.playing){
            playState = PlayState.pausemenu;
            Time.timeScale = 0;
        }
        else {
            playState = PlayState.playing;
            Time.timeScale = 1;
        }
    }

    public void TransformToCamera (Transform transf){
        transf.LookAt(_Camera.transform, Vector3.up);
    }

    public void UnloadLevel(string level){
        SceneManager.UnloadSceneAsync(level);
    }

    public void LoadLevel(string level){
        SceneManager.LoadSceneAsync(level,LoadSceneMode.Additive);
        
    }

    

    public void PlayerDie(DeadStyle style, Transform pos){
        switch (style)
        {
            case DeadStyle.normal:
                Debug.Log("DEAD");
                playState = PlayState.dieDialog;
                Time.timeScale = 0.02f;
                if (deadCause==""){
                    uiController.ShowDieDialog("You just Died. \n Unfortunately, you are immortal");    
                }
                else {
                     uiController.ShowDieDialog(deadCause + " \n Unfortunately, you are immortal");
                }
               
                break;
            case DeadStyle.eaten:
                Debug.Log("DEAD");
                playState = PlayState.dieDialog;
                _Player.GetComponent<BasePlayerController>().GetEaten(pos);
                Time.timeScale = 0.02f;
                uiController.ShowDieDialog("You just been eated by a Evil Blobboon. \n Unfortunately, you are immortal");

                
                break;
            default:
                break;
        }
    }

    public void ResetAndRespawn(){
        playState = PlayState.loading;
        
        Destroy(_Player);
        deadCause = "";
        playerHealth = playerMaxHealth;
        if (SceneManager.GetActiveScene().name != RespawnLevel){
              SceneManager.LoadScene(RespawnLevel, LoadSceneMode.Single);
              Scene currentScene = SceneManager.GetActiveScene ();
        }
        
      
       
         Time.timeScale = 1;
        uiController.ResetBackdrop();
        _Level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelController>();
        _Player =  Instantiate(PlayerPrefab, _Level.respawnPoints[RespawnPlace].position, Quaternion.identity);
        playState = PlayState.playing;
    }


  /*   public void SetInventoryMenu(){
        showingInventory = !showingInventory;
    } */
    
}