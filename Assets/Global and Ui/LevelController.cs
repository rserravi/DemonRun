using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string levelName;
    public string unityLevelString;
    public string otherLevelToLoad;
    public string otherLevelToLoad2;

    [Header("Missions")]
    

    [Header("Respawns")]
    public Transform[] respawnPoints;
  
    [Header("Mixer Buses")]
    public AudioMixerGroup levelMasterMixer;
    public AudioMixerGroup levelAmbientMixer;
    public AudioMixerGroup levelFXMixer;
    public AudioMixerGroup levelMusicMixer;

    public AudioMixer levelMixer;

    private void Start() {
        if (otherLevelToLoad!=""){
            Scene scn = SceneManager.GetSceneByName(otherLevelToLoad);
            if (!scn.IsValid()){
                SceneManager.LoadSceneAsync(otherLevelToLoad,LoadSceneMode.Additive);
            }
        }

        if (otherLevelToLoad2!=""){
            Scene scn2 = SceneManager.GetSceneByName(otherLevelToLoad2);
            if (!scn2.IsValid()){
                SceneManager.LoadSceneAsync(otherLevelToLoad2,LoadSceneMode.Additive);
            }
        }

        if (!GameController._Player){
            
        }
    }

    public void ConfigureAudioSource(AudioSource _as, string bus){
        _as.spatialBlend = 1;
        switch (bus)
        {
            case "Ambient":
                _as.outputAudioMixerGroup = levelAmbientMixer;
                break;
            case "Music":
                _as.outputAudioMixerGroup = levelMusicMixer;
                break;
            case "FX":
                _as.outputAudioMixerGroup = levelFXMixer;
                break;
            
            default:
                break;
        }

    }

}
