using UnityEngine;
using UnityEngine.UI;

public class WeaponsInventory : MonoBehaviour
{
    public bool showing;
    public Image[] squares = new Image[8];
    public Image[] icons = new Image[8];

    public Image selected;
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
        //TODO: Check weapons and show only appropiate squares and icons
        showing = show;
        if (showing){
            WeaponsSystem playerWeapons = GameController._Player.GetComponent<WeaponsSystem>();
            int totalWeapons =  playerWeapons.weapons.Count;
            for (int i = 0; i < totalWeapons; i++)
            {
                icons[i].sprite =  playerWeapons.weapons[i].GetComponent<WeaponClass>().weaponIconSmall;
                icons[i].enabled = true;
                squares[i].enabled = true;
            }
            Vector3 selectedPosition = squares[playerWeapons.selectedWeapon].rectTransform.position;
            selected.rectTransform.position = selectedPosition;
            if (playerWeapons.weapons.Count>0){
                selected.enabled = true;
            }
        }
        else{
            foreach (Image square in squares)
            {
                square.enabled = false;
            }

            foreach (Image icon in icons)
            {
                icon.enabled = false;
            } 
            selected.enabled = false;
        }
    }
}
