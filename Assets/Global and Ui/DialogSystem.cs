using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogSystem : MonoBehaviour
{
    [Header("Bad Side")]
    public Image DialogLeftBackground;
    public Image CharacterLeftImage;
    public TextMeshProUGUI LeftTitleTxt;
    public TextMeshProUGUI LeftBodyTxt;
    public Image LeftDialogImage;
    

    [Header("Good Side")]
    public Image DialogRightBackground;
    public Image CharacterRightImage;
    public TextMeshProUGUI RightTitleTxt;
    public TextMeshProUGUI RightBodyTxt;

    public Image RightDialogImage;

    [Header("Purgatory")]
     public Image DialogCenterBackground;
    public Image ImageCenterDialog;
    public TextMeshProUGUI CenterTitleText;
    public TextMeshProUGUI CenterBodyTxt;

    private float leftTimer, rightTimer, centerTimer;
    private bool countingLeft, countingRight, countingCenter;
    private float endTimeLeft, endTimeRight, endTimeCenter;
    



    // Start is called before the first frame update
    void Start()
    {
        Reset();       
    }

    // Update is called once per frame
    void Update()
    {
        if (countingRight){
            rightTimer+= Time.deltaTime;
        }
        if (countingLeft){
            leftTimer+= Time.deltaTime;
        }
        if (countingCenter){
            centerTimer+= Time.deltaTime;
        }
       
        if (rightTimer > endTimeRight){
            countingRight = false;
            rightTimer = 0;
            StartCoroutine(FadeEveryoneToZeroAlpha(1f, GameController.goodEvil.good)); 
        } 

        if (leftTimer > endTimeLeft){
            countingLeft = false;
            leftTimer = 0;
            StartCoroutine(FadeEveryoneToZeroAlpha(1f, GameController.goodEvil.evil)); 
        } 

         if (centerTimer > endTimeCenter){
            countingCenter = false;
            centerTimer = 0;
            StartCoroutine(FadeEveryoneToZeroAlpha(1f, GameController.goodEvil.neutral)); 
        } 
        
    }

    public void ShowMessage( float time, GameController.goodEvil side, string character, GameController.CharacterMood mood, string title, string body, Sprite extraImage){

        Image _background;
        Image _character;
        Image _image;
        TextMeshProUGUI _title;
        TextMeshProUGUI _body;

        switch (side)
        {
            case GameController.goodEvil.good:
                _background = DialogRightBackground;
                _character = CharacterRightImage;
                _image = RightDialogImage;
                _title = RightTitleTxt;
                _body = RightBodyTxt;
                countingRight = true;
                endTimeRight = time;
                break;
            case GameController.goodEvil.evil:
                _background = DialogLeftBackground;
                _character = CharacterLeftImage;
                _image = LeftDialogImage;
                _title = LeftTitleTxt;
                _body = LeftBodyTxt;
                countingLeft = true;
                endTimeLeft = time;
                break;
            default:
                _background = DialogCenterBackground;
                _character = null;
                _image = ImageCenterDialog;
                _title = CenterTitleText;
                _body = CenterBodyTxt;
                countingCenter =true;
                endTimeCenter = time;
                break;
        }

        //TODO: SET THE CHARACTER IMAGE FROM PNJS;
        _title.text = title;
        _body.text = body;
        if (extraImage){
            _image.sprite = extraImage;
            _image.enabled = true;
        }
        _background.enabled = true;
        _character.enabled = true;
        _body.enabled = true;
        _title.enabled = true;
    }

    public IEnumerator FadeEveryoneToZeroAlpha(float t, GameController.goodEvil side)
    {
        Image _background;
        Image _character;
        Image _image;
        TextMeshProUGUI _title;
        TextMeshProUGUI _body;

        switch (side)
        {
            case GameController.goodEvil.good:
                _background = DialogRightBackground;
                _character = CharacterRightImage;
                _image = RightDialogImage;
                _title = RightTitleTxt;
                _body = RightBodyTxt;
                break;
            case GameController.goodEvil.evil:
                _background = DialogLeftBackground;
                _character = CharacterLeftImage;
                _image = LeftDialogImage;
                _title = LeftTitleTxt;
                _body = LeftBodyTxt;
                break;
            default:
                _background = DialogCenterBackground;
                _character = null;
                _image = ImageCenterDialog;
                _title = CenterTitleText;
                _body = CenterBodyTxt;
                break;
        }

        float alpha = 1f;
        while (alpha > 0.0f)
        {
            alpha -= (Time.deltaTime / t);
            if (_background.color.a > alpha){
                _background.color = new Color(_background.color.r,_background.color.g,_background.color.b, alpha);
            }
            if (_character.color.a > alpha){
                _character.color = new Color(_character.color.r,_character.color.g,_character.color.b, alpha);
            }
            if (_image.color.a > alpha){
                _image.color = new Color(_image.color.r,_image.color.g,_image.color.b, alpha);
            }
            if (_title.color.a > alpha){
                _title.color = new Color(_title.color.r,_title.color.g,_title.color.b, alpha);
            }
            if (_body.color.a > alpha){
                _body.color = new Color(_body.color.r,_body.color.g,_body.color.b, alpha);
            }
            
            yield return null;
        }
        if (alpha <0.1f){
            //Destroy(this.gameObject);
            Reset();
        }
    }

    void Reset(){
        DialogLeftBackground.enabled = false;
        DialogLeftBackground.color = new Color(DialogLeftBackground.color.r,DialogLeftBackground.color.g,DialogLeftBackground.color.b, 0.6f);
        CharacterLeftImage.enabled = false;
        CharacterLeftImage.color = new Color(CharacterLeftImage.color.r,CharacterLeftImage.color.g,CharacterLeftImage.color.b, 1f);
        LeftTitleTxt.enabled = false;
        LeftTitleTxt.color = new Color(LeftTitleTxt.color.r,LeftTitleTxt.color.g,LeftTitleTxt.color.b, 1f);
        LeftBodyTxt.enabled = false;
        LeftBodyTxt.color = new Color(LeftBodyTxt.color.r,LeftBodyTxt.color.g,LeftBodyTxt.color.b, 1f);
        LeftDialogImage.enabled = false;
        LeftDialogImage.color = new Color(LeftDialogImage.color.r,LeftDialogImage.color.g,LeftDialogImage.color.b, 1f);

        DialogRightBackground.enabled = false;
        DialogRightBackground.color = new Color(DialogRightBackground.color.r,DialogRightBackground.color.g,DialogRightBackground.color.b, 0.6f);
        CharacterRightImage.enabled = false;
        CharacterRightImage.color = new Color(CharacterRightImage.color.r,CharacterRightImage.color.g,CharacterRightImage.color.b, 1f);
        RightBodyTxt.enabled = false;
        RightBodyTxt.color = new Color(RightBodyTxt.color.r,RightBodyTxt.color.g,RightBodyTxt.color.b, 1f);
        RightTitleTxt.enabled = false;
        RightTitleTxt.color = new Color(RightTitleTxt.color.r,RightTitleTxt.color.g,RightTitleTxt.color.b, 1f);
        RightDialogImage.enabled = false;
        RightDialogImage.color = new Color(RightDialogImage.color.r,RightDialogImage.color.g,RightDialogImage.color.b, 1f);

        DialogCenterBackground.enabled = false;
        DialogCenterBackground.color = new Color(DialogCenterBackground.color.r,DialogCenterBackground.color.g,DialogCenterBackground.color.b, 0.6f);
        ImageCenterDialog.enabled = false;
        ImageCenterDialog.color = new Color(ImageCenterDialog.color.r,ImageCenterDialog.color.g,ImageCenterDialog.color.b, 1f);
        CenterTitleText.enabled = false;
        CenterTitleText.color = new Color(CenterTitleText.color.r,CenterTitleText.color.g,CenterTitleText.color.b, 1f);
        CenterBodyTxt.enabled = false;
        CenterBodyTxt.color = new Color(CenterBodyTxt.color.r,CenterBodyTxt.color.g,CenterBodyTxt.color.b, 1f);
    }

}
