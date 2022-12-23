using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// COMBOS

/// Combo 1. CTRL -> JUST HOLD
/// Combo 2. CTRL + FORWARD -> Main Weapon Forward;
/// Combo 3. CTRL + BACKWARD -> Main Wapon Defense.
/// Combo 4. CTRL + UP -> Main Weapon Up moviment.
/// Combo 5. CTRL + DOWN -> Main Weapon low attack.
/// Combo 6. CTRL + FORWARD + UP -> Main Weapon Jump Penetrator,
/// Combo 7. CTRL + FORWARD + DOWN -> Main Weapon low penetrator.
/// Combo 8. CTRL + BACKWARD + UP -> Spiral
/// Combo 9. CTRL + BACKWARD + DOWN -> Low Spiral.
/// Combo 10. CTRL + ALT -> Special attack (needs recovery)
public class WeaponsSystem : MonoBehaviour
{
    public enum WeaponState {none, holding, attacking, inDefense, reloading}
    public WeaponState weaponState  = WeaponState.none;
    public int selectedWeapon=0;
    public GameObject actualWeapon;
    public List<GameObject> weapons = new List<GameObject>();
    public bool holdingFire1 = false;
    public bool holdingFire2 = false;
    public bool holdingBackwards = false;
    public bool holdingForwards = false;
    public bool holdingUp = false;
    public bool holdingDown = false;
    public Vector2 moveVal;

    [Header("Weapons Position")]
    public Transform weaponPlace;
    public Transform throwStart;

    [HideInInspector]
    public Vector3 shieldPosition;
    [HideInInspector]
    public Quaternion shieldRotation;
    public Vector3 shieldCorrectorPosition;
    public Quaternion shieldCorrectorRotation;
    private GameController.Facing facing;
    public float holdingTimer = 0;
    public int weaponPower = 0;
    public float reloadTimer = 0;
    public float reloadTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        shieldPosition= transform.position + shieldCorrectorPosition;
        shieldRotation= transform.rotation * shieldCorrectorRotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (weaponState==WeaponState.reloading){
            reloadTimer+= Time.deltaTime;
        }

        //STATE WHEN TIME OR RELOADING FINISHES;
        if (reloadTimer>= reloadTime){
            if (holdingFire1){
                weaponState = WeaponState.holding;
            }
            if (!holdingFire1){
                weaponState = WeaponState.none;
            }
            reloadTimer = 0;
        }

        // WEAPON TRANSFORM

        facing = GameController._Player.GetComponent<BasePlayerController>().facing;
        moveVal = GameController._Player.GetComponent<BasePlayerController>().moveVal;
  
        float shieldX = shieldCorrectorPosition.x;
        if (facing == GameController.Facing.left){
            shieldX = -shieldCorrectorPosition.x;
        }
        if(actualWeapon){
            actualWeapon.transform.position = weaponPlace.transform.position + actualWeapon.GetComponent<WeaponClass>().positionCorrection;
            actualWeapon.transform.rotation = weaponPlace.transform.rotation * actualWeapon.GetComponent<WeaponClass>().rotationCorrection;
        }

        shieldPosition= transform.position + new Vector3(shieldX, shieldCorrectorPosition.y, shieldCorrectorPosition.z);
        shieldRotation= shieldCorrectorRotation;

        //SEND TO GAMECONTROLLER FOR UI
        weaponPower = Mathf.RoundToInt(holdingTimer * 10);
        GameController.instance.weaponHold = weaponPower;

        // STATES
        
        if (holdingFire1 && holdingTimer < 0.1f){
            weaponState = WeaponState.holding;
            Hold(1);
        }
        if (holdingFire1 && holdingBackwards &&!holdingDown && weaponState != WeaponState.inDefense){
            weaponState = WeaponState.inDefense;
            
        }
        if ((!holdingBackwards || !holdingFire1) && weaponState == WeaponState.inDefense){
            weaponState = WeaponState.none;
        }
           

        //SI ESTAMOS EN DEFENSE
        if (weaponState== WeaponState.inDefense){
            holdingTimer = 0;
             if (weapons.Count>0){
                weapons[selectedWeapon].GetComponent<WeaponClass>().setDefense(true);
             }
        }
        else {
             if (weapons.Count>0){
                weapons[selectedWeapon].GetComponent<WeaponClass>().setDefense(false);
             }
        }
        if (weaponState != WeaponState.reloading && weaponState != WeaponState.inDefense){
        //if (weaponState != WeaponState.reloading){
            if (holdingFire1){
                if (holdingTimer<=3f){
                    holdingTimer+= Time.deltaTime;
                }
                weaponState = WeaponState.holding;
                // COMBOS WHILE HOLDINGFIRE1.
                //COMBO 2 - CTRL + FORWARD
                if (holdingForwards && !holdingUp && !holdingDown){Fire(2, weaponPower);}
                //COMBO 3 - CTRL + BACKWARD ( DEFENSE )
               
                //COMBO 4 - CTRL + UP
                if (holdingUp && !holdingBackwards && !holdingForwards){Fire(4, weaponPower);}
                //COMBO 5 - CTRL + DOWN
                if (holdingDown && !holdingBackwards && !holdingForwards){Fire(5, weaponPower);}
               
            }

            if (!holdingFire1){
                if (weaponState != WeaponState.inDefense){ //Ha soltado el boton CTRL
                    weaponState = WeaponState.none;
                }
            }

            if (weaponState == WeaponState.none){
                if (actualWeapon!=null && !actualWeapon.Equals(null)){
                    actualWeapon.GetComponent<WeaponClass>().AnimationNone();
                }
                holdingTimer = 0;
                 
            }
        }
        GameController.instance._CameraAnimator.SetBool("Zoom", holdingFire1);
        
    }
    public void Fire(int combo, int value){
        Debug.Log("FIRING "+ value +" COMBO " + combo);
        if (weapons.Count>0){
            weapons[selectedWeapon].GetComponent<WeaponClass>().Attack(combo, value);
            //holdingTimer = 0;
            //reloading = true;
        }
    }

    public void Hold(int combo){
        if (weapons.Count>0){
            weapons[selectedWeapon].GetComponent<WeaponClass>().Hold(combo);
        }
    }

    public void ChangeWeapon(int newWeapon){
        //Holster(int weapon)
        //Draw(int weapon)
        if (actualWeapon){
            Destroy(actualWeapon);
        }
        if (weapons.Count>= newWeapon){
            selectedWeapon = newWeapon;
            actualWeapon = Instantiate(weapons[selectedWeapon].gameObject,weaponPlace.position, Quaternion.identity);
            actualWeapon.transform.SetParent(weaponPlace);
            actualWeapon.transform.position = actualWeapon.transform.position + actualWeapon.GetComponent<WeaponClass>().positionCorrection;
            actualWeapon.transform.rotation = actualWeapon.transform.rotation * actualWeapon.GetComponent<WeaponClass>().rotationCorrection;
        }
    }

    public void AddWeapon(GameObject newWeapon){
        weapons.Add(newWeapon);
        ChangeWeapon(weapons.Count -1);
    }

    public void NextWeapon(){
        if (weapons.Count>0){
            if (selectedWeapon +1 == weapons.Count ){
                ChangeWeapon(0);
            }
            else {
                ChangeWeapon(selectedWeapon+1);
            }
        }
    }

}
