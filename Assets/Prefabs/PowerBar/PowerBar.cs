
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    public GameObject[] unitBar = new GameObject[10];
    public Color initColor;
    public Color finalColor;
    private Color previousCol;
    // Start is called before the first frame update
    void Start()
    {

        //setColor
        for (int i = 0; i < unitBar.Length; i++)
        {
            Renderer rend = unitBar[i].GetComponent<Renderer>();
           
            rend.material.color = Color.Lerp(initColor, finalColor, 0.1f * i);
            rend.material.SetColor("_EmissionColor", Color.Lerp(initColor, finalColor, 0.1f * i));
            rend.enabled = false;
        }
       
        
    }

    public void ChangeValue(float newValue){ //VALUES FROM 0 to 10;
        
        for (int i = 0; i < newValue; i++)
        {
            Renderer rend = unitBar[i].GetComponent<Renderer>();
            rend.enabled = true;
        }

        for (int i = unitBar.Length; i < 0; i--)
        {
             Renderer rend = unitBar[i].GetComponent<Renderer>();
             rend.enabled = false;
        }
    }

    public void Kill(){
        for (int i = 0; i < unitBar.Length; i++)
        {
            Renderer rend = unitBar[i].GetComponent<Renderer>();
            Color newCol = rend.material.color;
            newCol.a -=0.1f;
            rend.material.color =  newCol;
            
        }
        Destroy(this.gameObject);
    }
}
