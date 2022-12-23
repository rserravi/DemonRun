using System.Collections.Generic;
using UnityEngine;

public class GooLauncher : MonoBehaviour
{
    public float interval;
    public GameObject liquidPrefab;
    public Transform instantiatePlace;
    private float timer=0f;
    public List<GameObject> liquids = new List<GameObject>();

    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active){
            timer += Time.deltaTime;
            if (timer>=interval){
                timer = 0f;
                liquids.Add(Instantiate(liquidPrefab,instantiatePlace.position, Quaternion.identity));
            }
            
            if (liquids.Count>2){
                Destroy(liquids[0]);
                liquids.RemoveAt(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            active = true;
            timer = interval-0.2f;
        }
    }

    private void OnTriggerExit(Collider other) {
         if (other.CompareTag("Player")){
            active = false;
        }
    }
}
