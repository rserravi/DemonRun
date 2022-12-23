using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingleSliderControl : MonoBehaviour
{
    public int value;
    public int maxValue;
    public Slider slider;
    public TextMeshProUGUI txt;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = maxValue;
        slider.value = value;
        txt.text = value.ToString();
    }

    public void SetValue(int val){
        value = val;
    }

    public void SetMaxValue (int maxVal){
        maxValue = maxVal;
    }
}
