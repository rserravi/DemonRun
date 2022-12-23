using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingEnemies : Enemy
{
    [Header("Target Points")]
    public List<Transform> targetPoints = new List<Transform>();     
    private int currentTarget;
    private float oldSpeed;

    protected override void Start() {
        base.Start();
        oldSpeed = speed;
        currentTarget = 0;
        target = targetPoints[0].position;
     }

    protected override void TargetUpdate(){
        

        if (ReachedTarget(0.4f)){
            if (enemyState== EnemyState.retreating){
                enemyState = EnemyState.wandering;
            }
            if (currentTarget+1== targetPoints.Count){
                currentTarget = 0;
                
            }
            else{currentTarget+=1;}

            target = targetPoints[currentTarget].position;
        }

        if (playerDetected &&!playerClose && enemyState != EnemyState.retreating){
            enemyState = EnemyState.goingToPlayer;
            target = playerPosition;
            speed = oldSpeed;
        }
        if (playerClose && enemyState == EnemyState.goingToPlayer){
           
            EnemyState action = AggressivityCheck();
            
            enemyState = action;
           
            
            switch (action)
            {
                case EnemyState.attacking:
                    target = playerPosition;
                    speed = oldSpeed * 1.5f;
                    _Animator.SetBool("Attack", true);
                    _AS_OtherSounds.PlayOneShot(audioAttack);
                    if (AggressivityCheck()== EnemyState.runningAway){
                        enemyState = EnemyState.retreating;
                    }
                    
                    break;
                case EnemyState.retreating:
                    target = targetPoints[currentTarget].position;
                    speed = oldSpeed;
                     _Animator.SetBool("Attack", false);
                    break;
                case EnemyState.runningAway:
                    target = (target - transform.position) *-1;
                    speed = oldSpeed * 1.5f;
                    _Animator.SetBool("Attack", false);
                    break;
                case EnemyState.waiting:
                    enemyState = EnemyState.wandering;
                    target = targetPoints[currentTarget].position;
                    speed = oldSpeed;
                    _Animator.SetBool("Attack", false);
                    break;   
                default:
                    break;
            }
        }
        
    }
}
