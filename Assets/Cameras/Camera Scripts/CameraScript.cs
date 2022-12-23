using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
    
{
    public Cinemachine.CinemachineStateDrivenCamera _stateDrivenCam;
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
           _stateDrivenCam.Follow = GameController._Player.transform;
            _stateDrivenCam.LookAt = GameController._Player.transform;   
        }
        catch (System.Exception)
        {
            
            
        }
       
          
      
        
    }
}
