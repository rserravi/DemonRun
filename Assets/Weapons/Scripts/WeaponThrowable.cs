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

public class WeaponThrowable : WeaponClass
{
     public override void HitAnimationPoint(int combo){
        //Debug.Break();
        //Debug.Log("INSTANCIANDO DESDE THROWABLE " + _wSystem.weaponPower);
        projectile = Instantiate(particlesAttack, _wSystem.throwStart.position, Quaternion.identity);
        Projectile prjComp = projectile.GetComponent<Projectile>();
        Rigidbody prjRb = projectile.GetComponent<Rigidbody>();
        switch (combo)
        {
            case 1:
                //HOLDING;
                break;
            case 2: //FORWARD
                prjComp.gravity = false;
                prjRb.AddRelativeForce(GameController._Player.transform.forward * speed *30);
                prjComp.maxDamagePoints = (30 * _wSystem.weaponPower) / maxDamagePoints;
                prjComp.minDamagePoints = minDamagePoints;
                _Animator.SetInteger("Combo",1);
                
                break;
            case 3: //BACKWARDS
                //DEFENDING
                break;
            case 4: //UP
                prjComp.gravity = true;
                prjComp.lifeTime = 4f;
                prjRb.AddRelativeForce(GameController._Player.transform.forward * speed/2 + GameController._Player.transform.up * speed * _wSystem.weaponPower);
                prjRb.AddRelativeForce(GameController._Player.transform.forward * speed * _wSystem.weaponPower);
                prjComp.maxDamagePoints = (30 * _wSystem.weaponPower) / maxDamagePoints;
                prjComp.minDamagePoints = minDamagePoints;
                 _Animator.SetInteger("Combo",1);
                break;
            default:
                break;
        }
        //Debug.Break();
        _wSystem.holdingTimer = 0;
        _wSystem.weaponState = WeaponsSystem.WeaponState.reloading;
    }

}
