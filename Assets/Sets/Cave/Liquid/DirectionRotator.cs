using UnityEngine;

public class DirectionRotator : MonoBehaviour
{

    public Collider horCollider, verCollider;
    public bool touchingHor, touchingVer;
    public string exitFaceHor, exitFaceVer;
    public string startingDir = "";
    public string endingDir = "";


    private void OnTriggerEnter(Collider other) {
        if (other.bounds.Intersects(horCollider.bounds)){
            touchingHor = true;
            if (startingDir==""){
                startingDir="Horizontal";
                endingDir="Vertical";
            }
        }
        if (other.bounds.Intersects(verCollider.bounds)){
            touchingVer = true;
            if (startingDir==""){
                startingDir="Vertical";
                endingDir="Horizontal";
            }
        }
    }

    private void Update() {
        if (startingDir=="Horizontal"){
         
        }
    }

    
}
