using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadingText : MonoBehaviour
{
    public TextMeshProUGUI tm;
    public Material fontMaterialWhite;
    public Material fontMaterialRed;
    public float speed;
    private bool moving = false;

    // Update is called once per frame
    void Update()
    {
        if (moving){
            transform.Translate(new Vector3(0,1,1) *speed * Time.deltaTime,Space.World);
        }
  
    }

   public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i, GameController.DamageType damageType)
    {
        if (damageType == GameController.DamageType.critical){
            i.fontSharedMaterial = fontMaterialRed;
        }
        else {
            i.fontSharedMaterial = fontMaterialWhite;
        }
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            i.fontMaterial.color = i.color;
            yield return null;
        }
        if (i.color.a <0.6f){
            Destroy(this.gameObject);
        }
    }

    public void SetText(string txt, GameController.DamageType damageType){
        tm.text = txt;
        if (damageType == GameController.DamageType.fail){
            tm.fontStyle = FontStyles.Italic;
            tm.fontSize = 4;
            tm.text = "Fail!";
        }
        if (damageType == GameController.DamageType.critical){
            tm.fontStyle = FontStyles.Bold;
            tm.text = txt + "!";
        }
        StartCoroutine(FadeTextToZeroAlpha(8f, tm, damageType)); 
        moving = true;
    }
}
