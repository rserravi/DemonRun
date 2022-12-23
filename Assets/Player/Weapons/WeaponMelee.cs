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

using UnityEngine;

public class WeaponMelee : WeaponClass
{    

    private void OnTriggerEnter(Collider other) {
         if (other.CompareTag("Enemy")){
            Debug.Log("HITTING ENEMY WITGH GAUNTLET");
            int dam = Random.Range(minDamagePoints,maxDamagePoints+1);
            GameController.DamageType damageType = GameController.DamageType.normal;
            if (dam == maxDamagePoints){
               damageType = GameController.DamageType.critical;
            }
            if (dam == minDamagePoints){
               damageType = GameController.DamageType.fail;
            }
            other.attachedRigidbody.AddForceAtPosition(transform.forward * dam*5,_wSystem.weaponPlace.position, ForceMode.Force);
            GameController.instance.GiveDamage(dam, other.ClosestPoint(transform.position), other.gameObject.GetComponent<Enemy>(), damageType);
        }
    }

}
