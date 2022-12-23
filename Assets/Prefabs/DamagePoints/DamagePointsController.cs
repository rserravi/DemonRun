using System.Collections;
using UnityEngine;
using TMPro;

public class DamagePointsController : MonoBehaviour
{
    public TextMeshProUGUI tm;
    public float speed;
    private bool moving = false;
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (moving){
            transform.Translate(Vector3.up*speed * Time.deltaTime,Space.World);
        }
  
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i, Color col)
    {
        
        //i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        i.color = new Color(col.r, col.g, col.b, 1);
        
        while (i.color.a > 0.0f)
        {
            //Debug.Log(intensity + "MAT "+ i.fontMaterial.name);
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            i.fontMaterial.color = i.color;
            yield return null;
        }
        if (i.color.a <0.6f){
            Destroy(this.gameObject);
        }
    }

    public void SetText(string txt, Color color){
        tm.text = txt;
        StartCoroutine(FadeTextToZeroAlpha(8f, tm, color)); 
        moving = true;
    }
}
