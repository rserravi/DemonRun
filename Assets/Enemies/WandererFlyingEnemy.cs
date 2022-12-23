using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererFlyingEnemy : Enemy
{

    [Header("Target Points")]
     public List<Transform> targetPoints = new List<Transform>();     
     private int currentTarget;

     protected override void Start() {
        base.Start();
        currentTarget = 0;
        target = targetPoints[0].position;
     }

    protected override void TargetUpdate(){

        if (ReachedTarget(0.4f)){
            if (currentTarget+1== targetPoints.Count){
                currentTarget = 0;
                
            }
            else{currentTarget+=1;}

            target = targetPoints[currentTarget].position;
        }
        
    }
}

