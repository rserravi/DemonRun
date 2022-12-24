using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirChanger : MonoBehaviour
{
    public enum HorizontalDir {Right, Left}
    public enum VerticalDir {Front, Back};
    [TextArea]
    public string instructions;
    private Collider _collider;
    public string entering;
    public float centerFineTuning = 0.2f;
    public VerticalDir verticalExit;
    public HorizontalDir horizontalExit;
    

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
            //Debug.Log(other.transform.position);
            //Debug.Log(_collider.ClosestPoint(other.transform.position));
            other.gameObject.GetComponent<Pj>().overridenDirDetection=true;
            if (System.Math.Round(other.transform.position.x,2)  < System.Math.Round(_collider.ClosestPoint(other.transform.position).x,2)){
                entering = "WEST";
                
            }
            if (System.Math.Round(other.transform.position.x,2)  > System.Math.Round(_collider.ClosestPoint(other.transform.position).x,2)){
                 entering = "EAST";
                
            }
            if (System.Math.Round(other.transform.position.z,2)  < System.Math.Round(_collider.ClosestPoint(other.transform.position).z,2)){
                entering = "NORTH";
                
            }
            if (System.Math.Round(other.transform.position.z,2)  > System.Math.Round(_collider.ClosestPoint(other.transform.position).z,2)){
                entering= "SOUTH";
                
            }
            Debug.Log("ENTERING " + entering);
            
        }
    }
    private void OnTriggerStay(Collider other) {
        Debug.Log(Vector3.Distance(other.transform.position, transform.position));
        if (Vector3.Distance(other.transform.position, transform.position)<centerFineTuning){
            switch (entering)
            {
                case "WEST":
                    other.gameObject.GetComponent<Pj>().verticalGround = true;
                    if (verticalExit == VerticalDir.Back){
                        other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.back;
                    }
                    else{
                        other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.front;
                    }
                    break;
                case "EAST":
                    other.gameObject.GetComponent<Pj>().verticalGround = true;
                    if (verticalExit == VerticalDir.Back){
                        other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.back;
                    }
                    else{
                        other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.front;
                    }
                    break;
                case "NORTH":
                    other.gameObject.GetComponent<Pj>().verticalGround = false;
                    if (horizontalExit == HorizontalDir.Left){
                        other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.left;
                    }
                    else{
                        other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.right;
                    }
                    break;
                case "SOUTH":
                    other.gameObject.GetComponent<Pj>().verticalGround = false;
                    if (horizontalExit == HorizontalDir.Left){
                        other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.left;
                    }
                    else{
                        other.GetComponentInParent<BasePlayerController>().facing = GameController.Facing.right;
                    }
                    break;
                default:
                    break;
            }
        }   
    }

    private void OnTriggerExit(Collider other) {
         if (other.CompareTag("Player")){
            other.gameObject.GetComponent<Pj>().overridenDirDetection=false;
         }
    }

}
