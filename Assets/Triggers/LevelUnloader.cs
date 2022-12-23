using UnityEngine;


public class LevelUnloader : MonoBehaviour

{
    public string levelToUnload;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            GameController.instance.UnloadLevel(levelToUnload);
        }
    }
   
}
