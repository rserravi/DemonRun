using UnityEngine;
using UnityEngine.UI;

public class ItemsInventory : MonoBehaviour
{
    public bool showing;
    public Image[] squares = new Image[8];
    public Image[] icons = new Image[8];
    // Start is called before the first frame update
    void Start()
    {
        Show(showing);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show (bool show){
        //TODO: Check items and show only appropiate squares and icons
        showing = show;

        foreach (Image square in squares)
        {
            square.enabled = show;
        }

        /* foreach (Image icon in icons)
        {
            icon.enabled = show;
        } */
    }
}

