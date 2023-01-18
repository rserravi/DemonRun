using UnityEngine;

public class DressingRoom : MonoBehaviour
{

    public enum Clothing {nude, tunica, armour};
    public enum WingType {normal, advanced, ultra}
    [Header("Good or Evil")]
    
    public GameController.goodEvil body;
    public GameController.goodEvil eyes;
    public GameController.goodEvil collar;
    public GameController.goodEvil wings;
    public GameController.goodEvil weapon;

    [Header("General GameObjects")]
    public ParticleSystem lavaParticles; 
    public Animator wingAnimator;

    [Header("Basic Parameters")]
    public Clothing clothing;
    public WingType wingType;
    public bool helmet;

    [Header("GameObjects Angel")]

    public GameObject A_Hair;
    public GameObject A_Eyes;
    public GameObject A_EyesInternal;
    public GameObject A_WingsNormal;
    public GameObject A_WingsAdvanced;
    public GameObject A_WingsUltra;
    public GameObject A_Tunica;
    public GameObject A_Armour;
    public GameObject A_Collar;
    public GameObject A_Helmet;
    public GameObject A_Boots;
    public GameObject A_BodyMesh;
    public Avatar A_Avatar;
    public GameObject A_Halo;
    public Material A_BodyMat;
    

    [Header("GameObjects Demon")]
    public GameObject D_Hair;
    public GameObject D_Eyes;
    public GameObject D_EyesInternal;
    public GameObject D_WingsNormal;
    public GameObject D_WingsAdvanced;
    public GameObject D_WingsUltra;
    public GameObject D_Tunica;
    public GameObject D_Armour;
    public GameObject D_Collar;
    public GameObject D_Helmet;
    public GameObject D_BodyMesh;
    public Avatar D_Avatar;
    public GameObject D_Tail;
    public GameObject D_Horns;
    public Material D_BodyMat;

    private Pj _pj;
    private Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        _pj = GetComponent<Pj>();
        _Animator = GetComponent<Animator>();
        SetLooks();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pj.movementStyle== "Slider"){
            lavaParticles.Play();
        }
        else lavaParticles.Stop();
        
    }
     void SetLooks(){
        
        switch (body)
        {
            case GameController.goodEvil.good:
                A_BodyMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
                A_Halo.GetComponent<Renderer>().enabled = true;
                D_Tail.GetComponent<Renderer>().enabled = false;
                D_Horns.GetComponent<SkinnedMeshRenderer>().enabled = false;
                D_BodyMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
                if (helmet){
                    A_Helmet.GetComponent<Renderer>().enabled = true;
                    D_Helmet.GetComponent<Renderer>().enabled = false;
                    A_Hair.GetComponent<SkinnedMeshRenderer>().enabled = false;;
                    D_Hair.GetComponent<Renderer>().enabled = false;
                
                }
                else{
                    A_Helmet.GetComponent<Renderer>().enabled = false;
                    D_Helmet.GetComponent<Renderer>().enabled = false;
                    A_Hair.GetComponent<SkinnedMeshRenderer>().enabled = true;;
                    D_Hair.GetComponent<Renderer>().enabled = false;
                }
                switch (clothing)
                {
                    case Clothing.nude:
                        A_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                       // D_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        A_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                       // D_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        break;
                    case Clothing.tunica:
                        A_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=true;
                       // D_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        A_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        //D_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        break;
                    case Clothing.armour:
                        A_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        //D_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        A_Armour.GetComponent<SkinnedMeshRenderer>().enabled=true;
                        //D_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        break;
                    
                    default:
                        break;
                }
                _Animator.avatar = A_Avatar;
                break;
            case GameController.goodEvil.evil:
                A_BodyMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
                A_Halo.GetComponent<Renderer>().enabled = false;
                D_Tail.GetComponent<Renderer>().enabled = true;
                D_Horns.GetComponent<SkinnedMeshRenderer>().enabled = true;
                D_BodyMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
                if (helmet){
                    A_Helmet.GetComponent<Renderer>().enabled = false;
                    D_Helmet.GetComponent<Renderer>().enabled = true;
                    A_Hair.GetComponent<SkinnedMeshRenderer>().enabled = false;;
                    D_Hair.GetComponent<Renderer>().enabled  = false;
                
                }
                else{
                    A_Helmet.GetComponent<Renderer>().enabled = false;
                    D_Helmet.GetComponent<Renderer>().enabled = false;
                     A_Hair.GetComponent<SkinnedMeshRenderer>().enabled = false;;
                    D_Hair.GetComponent<Renderer>().enabled  = true;
                }
                switch (clothing)
                {
                    case Clothing.nude:
                        A_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        //D_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        A_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        //D_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        break;
                    case Clothing.tunica:
                        A_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        //D_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=true;
                        A_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        //D_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        break;
                    case Clothing.armour:
                        A_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        //D_Tunica.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        A_Armour.GetComponent<SkinnedMeshRenderer>().enabled=false;
                        //D_Armour.GetComponent<SkinnedMeshRenderer>().enabled=true;
                        break;
                    
                    default:
                        break;
                }
                _Animator.avatar = D_Avatar;
                break;
            default:
                A_BodyMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;;
                D_BodyMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
                A_Halo.GetComponent<Renderer>().enabled  = false;
                D_Tail.GetComponent<Renderer>().enabled = true;
                D_Horns.GetComponent<SkinnedMeshRenderer>().enabled = true;
                A_Hair.GetComponent<SkinnedMeshRenderer>().enabled = false;;
                D_Hair.GetComponent<Renderer>().enabled  = true;
                A_Helmet.GetComponent<Renderer>().enabled = false;
                D_Helmet.GetComponent<Renderer>().enabled = false;
                _Animator.avatar = D_Avatar;
                break;
        }

        switch (eyes)
        {
            case GameController.goodEvil.good:
                A_Eyes.GetComponent<SkinnedMeshRenderer>().enabled = true;   
                A_EyesInternal.GetComponent<SkinnedMeshRenderer>().enabled = true;            
                D_Eyes.GetComponent<Renderer>().enabled = false;
                D_EyesInternal.GetComponent<Renderer>().enabled = false;
                break;
            case GameController.goodEvil.evil:
                A_Eyes.GetComponent<SkinnedMeshRenderer>().enabled = false;   
                A_EyesInternal.GetComponent<SkinnedMeshRenderer>().enabled = false;            
                D_Eyes.GetComponent<Renderer>().enabled = true;
                D_EyesInternal.GetComponent<Renderer>().enabled = true;
                break;
            default:
                A_Eyes.GetComponent<SkinnedMeshRenderer>().enabled = false;   
                A_EyesInternal.GetComponent<SkinnedMeshRenderer>().enabled = false;            
                D_Eyes.GetComponent<Renderer>().enabled = true;
                D_EyesInternal.GetComponent<Renderer>().enabled = true;
                break;
        }

        switch (collar)
        {
            case GameController.goodEvil.none:
               
                A_Collar.GetComponent<Renderer>().enabled = false;
                D_Collar.GetComponent<Renderer>().enabled = false;
                break;
            case GameController.goodEvil.good:
                A_Collar.GetComponent<Renderer>().enabled = true;
                D_Collar.GetComponent<Renderer>().enabled = false;
                break;
            case GameController.goodEvil.evil:
                A_Collar.GetComponent<Renderer>().enabled = false;
                D_Collar.GetComponent<Renderer>().enabled = true;
                break;
            default:
                A_Collar.GetComponent<Renderer>().enabled = false;
                D_Collar.GetComponent<Renderer>().enabled = false;
                break;
        }       
        
        switch (wings)
        {
            case GameController.goodEvil.none:
                A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
                A_WingsUltra.GetComponent<SkinnedMeshRenderer>().enabled = false;
                D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                D_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
                D_WingsUltra.GetComponent<SkinnedMeshRenderer>().enabled = false;
               
                break;
            case GameController.goodEvil.good:
                switch (wingType)
                {
                    case WingType.normal:
                        Debug.Log("SETTING WINGS GOOD");
                        A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = true;
                        A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
                       // A_WingsUltra.GetComponent<Renderer>().enabled = false;
                        D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                       // D_WingsAdvanced.GetComponent<Renderer>().enabled = false;
                       // D_WingsUltra.GetComponent<Renderer>().enabled = false;
                       wingAnimator = A_WingsNormal.GetComponentInParent<Animator>();
                        break;
                        
                    case WingType.advanced:
                        A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                        A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = true;
                      //  A_WingsUltra.GetComponent<Renderer>().enabled = false;
                        D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  D_WingsAdvanced.GetComponent<Renderer>().enabled = false;
                      //  D_WingsUltra.GetComponent<Renderer>().enabled = false;
                        wingAnimator = A_WingsAdvanced.GetComponentInParent<Animator>();
                        break;
                    case WingType.ultra:
                        A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                        A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  A_WingsUltra.GetComponent<Renderer>().enabled = true;
                        D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  D_WingsAdvanced.GetComponent<Renderer>().enabled = false;
                      //  D_WingsUltra.GetComponent<Renderer>().enabled = false;
                        break;
                    
                    default:
                        A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                        A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  A_WingsUltra.GetComponent<Renderer>().enabled = false;
                        D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  D_WingsAdvanced.GetComponent<Renderer>().enabled = false;
                      //  D_WingsUltra.GetComponent<Renderer>().enabled = false;
                        break;
                }
                break;
            case GameController.goodEvil.evil:
                switch (wingType)
                {
                    case WingType.normal:
                        A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                        A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  A_WingsUltra.GetComponent<Renderer>().enabled = false;
                        D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = true;
                      //  D_WingsAdvanced.GetComponent<Renderer>().enabled = false;
                      //  D_WingsUltra.GetComponent<Renderer>().enabled = false;
                        break;
                    case WingType.advanced:
                        A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                        A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  A_WingsUltra.GetComponent<Renderer>().enabled = false;
                        D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  D_WingsAdvanced.GetComponent<Renderer>().enabled = true;
                      //  D_WingsUltra.GetComponent<Renderer>().enabled = false;
                        break;
                    case WingType.ultra:
                        A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                        A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  A_WingsUltra.GetComponent<Renderer>().enabled = false;
                        D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  D_WingsAdvanced.GetComponent<Renderer>().enabled = false;
                      //  D_WingsUltra.GetComponent<Renderer>().enabled = true;
                        break;
                    
                    default:
                        A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                        A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  A_WingsUltra.GetComponent<Renderer>().enabled = false;
                        D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                      //  D_WingsAdvanced.GetComponent<Renderer>().enabled = false;
                      //  D_WingsUltra.GetComponent<Renderer>().enabled = false;
                        break;
                }
                break;
            default:
                A_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
                A_WingsAdvanced.GetComponent<SkinnedMeshRenderer>().enabled = false;
              //  A_WingsUltra.GetComponent<Renderer>().enabled = false;
                D_WingsNormal.GetComponent<SkinnedMeshRenderer>().enabled = false;
              //  D_WingsAdvanced.GetComponent<Renderer>().enabled = false;
              //  D_WingsUltra.GetComponent<Renderer>().enabled = false;
                break;
        }
    }

}
