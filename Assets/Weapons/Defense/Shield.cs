using UnityEngine;


public class Shield : MonoBehaviour
{
    public WeaponClass _weapon;
    public int maxShieldPoints;
    // Start is called before the first frame update
    public void GetDamage(int damage, GameController.DamageType damageType){
        _weapon.shieldResistance -= damage;
        GameController.instance.DamageMessage(damage, transform, damageType);

        MeshRenderer ms = GetComponent<MeshRenderer>();
        ms.material.SetInt("_FresnelPower", 3*_weapon.shieldResistance /maxShieldPoints);

        if (_weapon.shieldResistance <=0){
            Destroy(this.gameObject);
        }

    }
}
