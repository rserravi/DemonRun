using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPack : MonoBehaviour
{
    public int ratsNumber;
    public GameObject ratPrefab;
    public Collider initialBounds;
    public List<GameObject> rats = new List<GameObject>();

    public float playerDistance;

    public float instantiateDistance = 50f;
    public float nervousDistance = 10f;
    public float alertDistance = 2f;

    public bool instantiatedRats = false;
    
    private ratController.RatMood _ratsMood;
    [SerializeField]
    public ratController.RatMood ratsMood {
        get {return _ratsMood;}
        set {_ratsMood = value;
            for (int i = 0; i < rats.Count; i++)
            {
                rats[i].GetComponent<ratController>().ratMood = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       if (!initialBounds || initialBounds ==null){
            initialBounds = GetComponent<Collider>();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, GameController._Player.transform.position);

        if (playerDistance<= instantiateDistance){
            InstantiateRats();
        }
        if (playerDistance <= nervousDistance){
            ratsMood = ratController.RatMood.nervous;
        }
        if (playerDistance > nervousDistance){
            ratsMood = ratController.RatMood.relaxed;
        } 
        if (playerDistance <= alertDistance){
            ratsMood = ratController.RatMood.scared;
        }
        if (playerDistance > instantiateDistance && instantiatedRats){
            EliminateRats();
        }
    }

    private void InstantiateRats(){
        if (!instantiatedRats){
            for (int i = 0; i < ratsNumber; i++)
            {

                float randomX = Random.Range(0,initialBounds.bounds.size.x);
                float randomY = Random.Range(0,359);
                float randomZ = Random.Range(0,initialBounds.bounds.size.z);

                Vector3 newPos = new Vector3(
                    initialBounds.bounds.center.x - initialBounds.bounds.extents.x + randomX,
                    initialBounds.bounds.center.y - initialBounds.bounds.extents.y ,
                    initialBounds.bounds.center.z - initialBounds.bounds.extents.z + randomZ
                );
                Quaternion newRot = Quaternion.identity;
                newRot = Quaternion.Euler(newRot.eulerAngles.x,randomY, newRot.eulerAngles.z);
                GameObject newRat = Instantiate(ratPrefab, newPos, newRot,transform);
                rats.Add(newRat);
                
            }
        instantiatedRats = true;
        }
    }

    private void EliminateRats(){
        for (int i = 0; i < rats.Count; i++)
        {
            Destroy(rats[i].gameObject);
        }
        instantiatedRats = false;

    }
}
