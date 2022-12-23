using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLevelAndSpawn : MonoBehaviour
{
   
    public LevelController level;
    public int spawnIndex;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            Debug.Log("SETTING NEW SPAWN POINT");
            GameController.instance.SetLevelAndSpawn(level, spawnIndex);
        }
    }
  
}
