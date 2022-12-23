using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public enum CamType {zoomCam, inverseVertical, Vertical, forceNormal};
    public CamType camType;

    private void  OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Player")){
            switch (camType)
            {
                case CamType.zoomCam:
                    break;
                case CamType.inverseVertical:
                    GameController.instance._CameraAnimator.SetBool("VerticalInverse", true);
                    GameController._Player.GetComponent<BasePlayerController>().inversedControls = true;
                    break;
                case CamType.Vertical:
                    break;
                
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){
            switch (camType)
            {
                case CamType.zoomCam:
                    break;
                case CamType.inverseVertical:
                    GameController.instance._CameraAnimator.SetBool("VerticalInverse", false);
                    GameController._Player.GetComponent<BasePlayerController>().inversedControls = false;
                    break;
                case CamType.Vertical:
                    break;
                
                default:
                    break;
            }
        }
        
    }
}
