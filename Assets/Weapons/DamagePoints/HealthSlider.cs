using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public bool vertical;
    public int maxValue;
    public int value;
    private Slider slider;
    public RectTransform fillArea;
    [SerializeField] private Image image;

    public float duration = 0.4f;
    private float timer = 0f;
    private bool timerOn;
  
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        if (vertical){
            fillArea.rotation = Quaternion.Euler(0,0,90);
        }
        slider.maxValue = maxValue;
        slider.value = value;
        image.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = value;
        if (timerOn){
            timer += Time.deltaTime;
        }
        if (timer > duration){
            timer = 0;
            timerOn = false;
            image.enabled = false;
        }

        //TODO: FACE CAMERA
    }

    public void TakeDamage(int damage, GameController.DamageType damageType){
        image.enabled = true;
        timerOn = true;
        value -= damage;
        slider.value = value;
        GameController.instance.DamageMessage(damage,transform, damageType);
        
    }
}
