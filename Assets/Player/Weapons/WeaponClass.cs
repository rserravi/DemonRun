using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public WeaponsSystem _wSystem;
    public AudioSource _AudioSource;
    public int maxDamagePoints;
    public int minDamagePoints;
    public int speed;

   
    public int weaponTimedPower = 0;
    
    [Tooltip("0-> FistFight, 1-> SpellThrow, 2-> Staff/Mace, 3->Sword")]
    public int animationType = 0; //0 -> FirstFight //1 -> SpelThrow // 2-> Staff // 3-> Sword
    public Animator _Animator;
    public bool inDefense;

    [Header("Particles")]
    public GameObject particlesAttack;
    public Transform particlesPlace;

    public GameObject projectile;

    [Header ("Corrections")]
    public Vector3 positionCorrection;
    public Quaternion rotationCorrection;

    [Header ("Shield")]
    private GameObject shieldInstance;
    public GameObject defensePrefab;
    public int shieldResistance;
    
    [Header("Sounds")]
    public AudioClip drawWeapon;
    public AudioClip holsterWeapon;
    public AudioClip holdingWeapon;
    public AudioClip moveWeapon;
    public AudioClip hitWeapon;

    [Header("Definitions")]
    public string weaponName;
    public string WeaponDescription;
    public GameController.goodEvil weaponSide;
    public Sprite  weaponIconSmall;
    public Sprite weaponIconMedium;


   
    void Start()
    {
        _wSystem = GameController._Player.GetComponent<WeaponsSystem>();
        _Animator = GameController._Player.GetComponent<Animator>();
    
        if (!_wSystem){  _wSystem = GameController._Player.GetComponent<WeaponsSystem>();}
        if (!_Animator){_Animator = GameController._Player.GetComponent<Animator>(); };

      //  _Animator.SetBool("Defense", inDefense);
        
        if (inDefense){
           // Debug.Log("Updating position");
            shieldInstance.transform.position = _wSystem.shieldPosition;
            shieldInstance.transform.rotation = _wSystem.shieldRotation;
        }
    }

    private void Update() {

        if (_wSystem.weaponState == WeaponsSystem.WeaponState.holding){
            if (!_AudioSource.isPlaying){_AudioSource.Play();}
           // Debug.Log("AUDIO PLAYING?=" + _AudioSource.isPlaying.ToString());
            if (_AudioSource.isPlaying){
                _AudioSource.volume = 0.5f;
                _AudioSource.pitch = _wSystem.holdingTimer;
            }
        }
    }

    public virtual void Attack(int combo, int timedPower){ //Combo range is 1-10;
        if (!_wSystem){  _wSystem = GameController._Player.GetComponent<WeaponsSystem>();}
        if (!_Animator){_Animator = GameController._Player.GetComponent<Animator>(); };
        weaponTimedPower = timedPower;
        Debug.Log("WEAPONTIMEDPOWER in WEAPON ATTACK "+ weaponTimedPower);
        _Animator.SetInteger("WeaponType", animationType);
        _Animator.SetInteger("Combo", combo);
       
        _Animator.SetBool("Holding", false);
        _AudioSource.Stop();
    }

    public virtual void Hold(int combo){
        if (!_Animator){_Animator = GameController._Player.GetComponent<Animator>(); };
         _Animator.SetInteger("WeaponType", animationType);
        _Animator.SetBool("Holding", true);
        _Animator.SetInteger("Combo", combo);
    
        _AudioSource.clip = holdingWeapon;
        _AudioSource.loop = true;  


    }

    public void setDefense(bool newSet){
          if (!_Animator){_Animator = GameController._Player.GetComponent<Animator>(); };
         _Animator.SetInteger("WeaponType", animationType);
      
        //Debug.Log("IN DEFENSE EN" + this.name + " " +inDefense.ToString());
        if (newSet && !inDefense && shieldResistance>0){
           _AudioSource.Stop();
           inDefense = newSet;
           _Animator.SetBool("Defense", inDefense);
           //SHIELD SOUND;
           shieldInstance = Instantiate(defensePrefab, _wSystem.shieldPosition, _wSystem.shieldRotation);
           shieldInstance.GetComponent<Shield>()._weapon = this;     
        }
        if (!newSet && inDefense){
            Destroy(shieldInstance);
            inDefense= newSet;
        }
        
    }

    public void ShieldDamage(int damage, GameController.DamageType damageType){
        shieldInstance.GetComponent<Shield>().GetDamage(damage, damageType);
    }

    public virtual void HitAnimationPoint(int combo){
       _AudioSource.Stop();
        Debug.Log("ANIMATION HIT" + combo.ToString());
        Quaternion instRotation =  transform.rotation * rotationCorrection;
        projectile = Instantiate(particlesAttack,_wSystem.throwStart.position , instRotation);
        //Debug.Break();
        _wSystem.holdingTimer = 0;
        _wSystem.weaponState = WeaponsSystem.WeaponState.reloading;
         _Animator.SetInteger("Combo",1);
        
    }

    public virtual void AnimationNone(){
        _Animator.SetBool("Defense", false);
        _Animator.SetBool("Holding", false);
        _AudioSource.Stop();
    }


}
