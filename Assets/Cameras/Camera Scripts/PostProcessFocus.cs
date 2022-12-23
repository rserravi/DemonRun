using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessFocus : MonoBehaviour
{
    public PostProcessVolume _ppVolume;
    public DepthOfField dop;
    public float focusDistance;
    // Start is called before the first frame update

    private void Awake() {
         DontDestroyOnLoad(this.gameObject);        
    }
    void Start()
    {
        _ppVolume = GetComponent<PostProcessVolume>();
       dop = _ppVolume.profile.GetSetting<DepthOfField>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
       focusDistance = (Camera.main.transform.position - GameController._Player.transform.position).magnitude;
       dop.focusDistance.Override(focusDistance);
        
    }
}
