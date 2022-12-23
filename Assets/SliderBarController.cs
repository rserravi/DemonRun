using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarController : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Image background;
    public int minVal;
    public Color minColor;
    public int maxVal;
    public bool gradientMinMax;
    public Color backgroundColor;
    
    
    public Color maxColor;
    public int value;
    // Start is called before the first frame update
    void Start()
    {
       
        slider.maxValue = maxVal;
        slider.minValue = minVal;
        slider.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        background.color = backgroundColor;
        if (gradientMinMax){
        float t = value / slider.maxValue;
        Color newCol = Color.Lerp(maxColor, minColor, t);
        newCol.a = 1f;
        fill.color = newCol;
        }
        else fill.color = maxColor;
        
    }

    public void SetValue(int val){
        value = val;
        slider.value = value;
    }

 
}
