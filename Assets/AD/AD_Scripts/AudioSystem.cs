using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private AudioSource _AS_Steps;
    private AudioSource _AS_OtherSounds;

    public AudioClip footLeft;
    public AudioClip footRight;
    // Start is called before the first frame update
    void Start()
    {
        _AS_Steps =  gameObject.AddComponent<AudioSource>();
        _AS_OtherSounds = gameObject.AddComponent<AudioSource>();
               
    }

    private void LateUpdate() {
        if (_AS_Steps.spatialBlend !=1){
            GameController.instance._Level.ConfigureAudioSource(_AS_Steps, "FX");
        }

        if (_AS_OtherSounds.spatialBlend !=1){
            GameController.instance._Level.ConfigureAudioSource(_AS_OtherSounds, "FX");
        }
    }

    void AnimStep(string side){
        float volume = 1f;

        //Debug.Log("Event: " + side + " called at: " + Time.time);
        if (side == "Left"){
            _AS_Steps.PlayOneShot(footLeft, volume);
        }
        else {
            _AS_Steps.PlayOneShot(footRight, volume);
        }
    }
}
