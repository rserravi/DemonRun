using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ADBarController : MonoBehaviour
{
    public Image angelIcon;
    public Image demonIcon;
    public Slider angelSlider;
    public Slider demonSlider;
    public TextMeshProUGUI angelPointsTxt;
    public TextMeshProUGUI demonPointsTxt;
    public int demonPoints;
    public int angelPoints;
    // Start is called before the first frame update
    void Start()
    {
        demonSlider.maxValue = 20f;
        angelSlider.maxValue = 20f;
                
    }

    // Update is called once per frame
    void Update()
    {
        angelPointsTxt.text = angelPoints.ToString();
        demonPointsTxt.text = demonPoints.ToString();
        if (angelPoints>demonPoints){
            angelIcon.enabled = true;
        }
        if (demonPoints>angelPoints){
            demonIcon.enabled = true;
        }
        if (angelPoints==demonPoints){
            angelIcon.enabled = false;
            demonIcon.enabled = false;
        }
        
    }

    public void SetDemonPoints(int points){
        demonPoints = points;
        demonSlider.value = points ;
        
    }

    public void SetAngelPoints(int points){
        angelPoints = points;
        angelSlider.value = points;
    }

    public void SetMax(int max){
        demonSlider.maxValue = max;
        angelSlider.maxValue = max;
    }
    
}
