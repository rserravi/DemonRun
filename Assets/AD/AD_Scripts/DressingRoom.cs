using UnityEngine;

public class DressingRoom : MonoBehaviour
{

    [Header("Good or Evil")]
    public GameController.goodEvil bodySkin;
    public GameController.goodEvil eyes;
    public GameController.goodEvil headWings;
    public GameController.goodEvil collar;
    public GameController.goodEvil wings;
    public GameController.goodEvil weapon;

    [Header("General GameObjects")]
    public GameObject body;
    public GameObject powerBarPrefab;
    public GameObject powerBar;
    public Transform powerBarTransform;
    public ParticleSystem lavaParticles;    

    [Header("GameObjects Angel")]
    public GameObject A_Hair;
    public GameObject A_EyeRight;
    public GameObject A_EyeRightInt;
    public GameObject A_EyeLeft;
    public GameObject A_EyeLeftInt;
    public GameObject A_WingRight;
    public GameObject A_WingLeft;
    public GameObject A_HeadWings;
    public GameObject A_Collar;
    public Material A_BodyMat;
    public Material A_Mouth;

    [Header("GameObjects Demon")]
    public GameObject D_Hair;
    public GameObject D_EyeRight;
    public GameObject D_EyeRightInt;
    public GameObject D_EyeLeft;
    public GameObject D_EyeLeftInt;
    public GameObject D_WingRight;
    public GameObject D_WingLeft;
    public GameObject D_HeadWings;
    public GameObject D_Collar;
    public Material D_BodyMat;
    public Material D_Mouth;

    private Pj _pj;
    // Start is called before the first frame update
    void Start()
    {
        _pj = GetComponent<Pj>();
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
        
        switch (bodySkin)
        {
            case GameController.goodEvil.good:
                Material[] mats = new Material[]{A_BodyMat, A_Mouth};
                body.GetComponent<SkinnedMeshRenderer>().materials = mats;
                A_Hair.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;;
                D_Hair.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                break;
            case GameController.goodEvil.evil:
                Material[] mats2 = new Material[]{D_BodyMat, A_Mouth};
                body.GetComponent<SkinnedMeshRenderer>().materials = mats2;
                A_Hair.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;;
                D_Hair.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                break;
            default:
                Material[] mats3 = new Material[]{D_BodyMat, A_Mouth};
                body.GetComponent<SkinnedMeshRenderer>().materials = mats3;
                A_Hair.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;;
                D_Hair.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                break;
        }

        switch (eyes)
        {
            case GameController.goodEvil.good:
                A_EyeLeft.GetComponent<Renderer>().enabled = true;
                A_EyeLeftInt.GetComponent<Renderer>().enabled = true;
                A_EyeRight.GetComponent<Renderer>().enabled = true;
                A_EyeRightInt.GetComponent<Renderer>().enabled = true;

                D_EyeLeft.GetComponent<Renderer>().enabled = false;
                D_EyeLeftInt.GetComponent<Renderer>().enabled = false;
                D_EyeRight.GetComponent<Renderer>().enabled = false;
                D_EyeRightInt.GetComponent<Renderer>().enabled = false;
                break;
            case GameController.goodEvil.evil:
                A_EyeLeft.GetComponent<Renderer>().enabled = false;
                A_EyeLeftInt.GetComponent<Renderer>().enabled = false;
                A_EyeRight.GetComponent<Renderer>().enabled = false;
                A_EyeRightInt.GetComponent<Renderer>().enabled = false;

                D_EyeLeft.GetComponent<Renderer>().enabled = true;
                D_EyeLeftInt.GetComponent<Renderer>().enabled = true;
                D_EyeRight.GetComponent<Renderer>().enabled = true;
                D_EyeRightInt.GetComponent<Renderer>().enabled = true;
                break;
            default:
                A_EyeLeft.GetComponent<Renderer>().enabled = false;
                A_EyeLeftInt.GetComponent<Renderer>().enabled = false;
                A_EyeRight.GetComponent<Renderer>().enabled = false;
                A_EyeRightInt.GetComponent<Renderer>().enabled = false;

                D_EyeLeft.GetComponent<Renderer>().enabled = true;
                D_EyeLeftInt.GetComponent<Renderer>().enabled = true;
                D_EyeRight.GetComponent<Renderer>().enabled = true;
                D_EyeRightInt.GetComponent<Renderer>().enabled = true;
                break;
        }

        switch (headWings)
        {
            case GameController.goodEvil.none:
               
                A_HeadWings.GetComponent<Renderer>().enabled = false;
                D_HeadWings.GetComponent<Renderer>().enabled = false;
                break;
            case GameController.goodEvil.good:
                A_HeadWings.GetComponent<Renderer>().enabled = true;
                D_HeadWings.GetComponent<Renderer>().enabled = false;
                break;
            case GameController.goodEvil.evil:
                A_HeadWings.GetComponent<Renderer>().enabled = false;
                D_HeadWings.GetComponent<Renderer>().enabled = true;
                break;
            default:
                A_HeadWings.GetComponent<Renderer>().enabled = false;
                D_HeadWings.GetComponent<Renderer>().enabled = false;
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
                A_WingLeft.GetComponent<Renderer>().enabled = false;
                A_WingRight.GetComponent<Renderer>().enabled = false;
                D_WingLeft.GetComponent<Renderer>().enabled = false;
                D_WingRight.GetComponent<Renderer>().enabled = false;
                break;
            case GameController.goodEvil.good:
                A_WingLeft.GetComponent<Renderer>().enabled = true;
                A_WingRight.GetComponent<Renderer>().enabled = true;
                D_WingLeft.GetComponent<Renderer>().enabled = false;
                D_WingRight.GetComponent<Renderer>().enabled = false;
                break;
            case GameController.goodEvil.evil:
                A_WingLeft.GetComponent<Renderer>().enabled = false;
                A_WingRight.GetComponent<Renderer>().enabled = false;
                D_WingLeft.GetComponent<Renderer>().enabled = true;
                D_WingRight.GetComponent<Renderer>().enabled = true;
                break;
            default:
                A_WingLeft.GetComponent<Renderer>().enabled = false;
                A_WingRight.GetComponent<Renderer>().enabled = false;
                D_WingLeft.GetComponent<Renderer>().enabled = false;
                D_WingRight.GetComponent<Renderer>().enabled = false;
                break;
        }
    }

}
