using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPack : MonoBehaviour
{
    public int ratsNumber;
    public GameObject ratPrefab;
    public Collider initialBounds;
    public List<GameObject> rats = new List<GameObject>();
    
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
            GameObject newRat = Instantiate(ratPrefab, newPos, newRot);
            rats.Add(newRat);
            
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
