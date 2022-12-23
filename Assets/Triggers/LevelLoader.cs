using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    public string levelToLoad;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            GameController.instance.LoadLevel(levelToLoad);
        }
    }
}
