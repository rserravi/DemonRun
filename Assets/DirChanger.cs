using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirChanger : MonoBehaviour
{
    [TextArea]
    public string instructions;
    private Collider _collider;
    public Transform doorHoritzontal;
    public Transform doorVertical;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            Debug.Log(other.transform.position);
            Debug.Log(_collider.ClosestPoint(other.transform.position));
            if (System.Math.Round(other.transform.position.x,2)  < System.Math.Round(_collider.ClosestPoint(other.transform.position).x,2)){
                Debug.Log ("ENTERING WEST");
                other.transform.position = doorVertical.position;
                other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.back;
            }
            if (System.Math.Round(other.transform.position.x,2)  > System.Math.Round(_collider.ClosestPoint(other.transform.position).x,2)){
                Debug.Log ("ENTERING EAST");
                other.transform.position = doorVertical.position;
                other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.back;
            }
            if (System.Math.Round(other.transform.position.z,2)  < System.Math.Round(_collider.ClosestPoint(other.transform.position).z,2)){
                Debug.Log ("ENTERING NORTH");
                other.transform.position = doorHoritzontal.position;
                other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.left;
            }
            if (System.Math.Round(other.transform.position.z,2)  > System.Math.Round(_collider.ClosestPoint(other.transform.position).z,2)){
                Debug.Log ("ENTERING SOUTH");
                other.transform.position = doorHoritzontal.position;
                other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.left;
            }
            
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Player")){
            Debug.Log(other.transform.position);
            Debug.Log(_collider.ClosestPoint(other.transform.position));
            if (other.transform.position.z != _collider.ClosestPoint(other.transform.position).z){
                Debug.Log ("ENTERING NORTH");
            }
            else{
                 Debug.Log("Entering West");
            }

        }
        
    }

}
