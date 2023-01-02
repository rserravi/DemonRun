using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratController : MonoBehaviour
{
    public enum RatHair {white, grey, brown, darkbrown, blackish};
    public enum RatSize {small, medium, big, humanoid};
    public enum RatState {idle, shortwalk, alert, running, attacking};
    public enum RatMood {relaxed, nervous, scared, angry}
    public enum Obstacle {none, rat, interesting, scary, fall};
    
    
    public RatHair ratHair;
    public RatSize ratSize;
    public RatState ratState;

    public RatMood ratMood;
    public float actionTimer = 0f;
    public float actionFinish = 0f;
    public float idleBaseSpeed;
    public float walkBaseSpeed;
    public float runBasepeed;
    public int currentIdle;
    public bool randomCharacteristics = false;

    [Header ("Components")]
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Rigidbody _rb;
    public Animator _anim;

    public Collider _col;

    [Header ("Materials")]
    public Material whiteMat;
    public Material greyMat;
    public Material brownMat;
    public Material darkBrownMat;
    public Material blackMat;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //COMPONENTS LOAD
        if (!_rb || _rb == null){
            _rb = GetComponent<Rigidbody>();
        } 
        if (!skinnedMeshRenderer || skinnedMeshRenderer == null){
            skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }
         if (!_anim || _anim == null){
            _anim = GetComponent<Animator>();
        }

        if (!_col || _col == null){
            _col = GetComponent<Collider>();
        }
        if (randomCharacteristics){
            ratHair = (RatHair) Random.Range(0,5);
            ratSize = (RatSize) Random.Range(0,3);
            idleBaseSpeed = Random.Range(0.7f, 1.2f);
            walkBaseSpeed = 3f;
            runBasepeed = 8f;

        }
        _anim.SetFloat("IdleSpeed", idleBaseSpeed);


        switch (ratSize)
        {
            case RatSize.small:
                float newNum = Random.Range(0.6f,0.9f);
                transform.localScale = new Vector3(newNum,newNum,newNum);
                break;
            case RatSize.medium:
                transform.localScale = new Vector3(1f,1f,1f);
                break;
            case RatSize.big:
                float newNum2 = Random.Range(1.1f,1.25f);
                transform.localScale = new Vector3(newNum2,newNum2,newNum2);
                break;
            case RatSize.humanoid:
                transform.localScale = new Vector3(5f,5f,5f);
                break;
            default:
                break;
        }
        
        Material[] matArray = skinnedMeshRenderer.materials;

        switch (ratHair)
        {
            case RatHair.white:
                matArray[1] = whiteMat;
                
                break;
            case RatHair.grey:
                matArray[1] = greyMat;
                break;
            case RatHair.brown:
                 matArray[1]= brownMat;   
                break;
            case RatHair.darkbrown:
                 matArray[1]= darkBrownMat;   
                break;
            case RatHair.blackish:
                 matArray[1] = blackMat;   
                break;
            default:
                 matArray[1]= brownMat;
                break;
        }
        skinnedMeshRenderer.materials = matArray;
        SetAction(RatState.idle, Random.Range(4f, 10f));

    }

    void SetAction(RatState state, float duration){
        ratState = state;
        actionFinish = duration;
        actionTimer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actionTimer+= Time.fixedDeltaTime;
        switch (ratState)
        {
            case RatState.idle:
                IdleUpdate();
                break;
            case RatState.shortwalk:
                WalkUpdate();
                break;
            case RatState.running:
                RunUpdate();
                break;
            case RatState.attacking:
                AttackUpdate();
                break;
            default:
                IdleUpdate();
                break;
        }
        
    }

    void IdleUpdate(){
        _anim.SetInteger("Idle", currentIdle);
        _anim.SetFloat("Speed", 0f);
        if (actionTimer >= actionFinish){
            int randomIdle = Random.Range(0,2);
            int randomD100 = Random.Range(0,101);
            switch (ratMood)
            {
                case RatMood.relaxed:
                    if(randomD100<85){
                        SetAction(RatState.idle, Random.Range(4f,10f));
                    }
                    else {
                        SetAction(RatState.shortwalk,Random.Range(0.2f,2f));
                    }
                    currentIdle = randomIdle;
                    _anim.SetInteger("Idle", currentIdle);
                    break;
                case RatMood.nervous:
                    if(randomD100>85){
                        SetAction(RatState.idle, Random.Range(1f,4f));
                    }
                    else {
                        SetAction(RatState.shortwalk,Random.Range(0.5f,3f));
                    }
                    currentIdle = randomIdle;
                    _anim.SetInteger("Idle", currentIdle);
                    break;
                case RatMood.scared:
                    SetAction(RatState.running, Random.Range(2f,4f));
                    break;
                case RatMood.angry:
                    SetAction(RatState.attacking, 2f);
                    break;
                default:
                    break;
            }
            
        }
       
    }

    void WalkUpdate(){
        _anim.SetFloat("Speed",walkBaseSpeed);
        Obstacle checkObs = CheckObstacles();
        if (checkObs ==Obstacle.none){
           _rb.AddForce(transform.forward * walkBaseSpeed * 5f * Time.fixedDeltaTime,ForceMode.VelocityChange);
        }
        else{ //TODO: Create method to pass over rats.
            float turn = 0f;
            if (checkObs == Obstacle.fall){
                int newDir = Random.Range(-3,4);
                turn = 45*newDir;
            }
            else{
                int newDir = Random.Range(-3,4);
                turn = 20*newDir;
            }
            _rb.MoveRotation(Quaternion.Euler(transform.rotation.eulerAngles.x, turn, transform.root.eulerAngles.z));
        }
        if (actionTimer >= actionFinish){
            int randomIdle = Random.Range(0,2);
            int randomD100 = Random.Range(0,101);
            switch (ratMood)
            {
                case RatMood.relaxed:
                    if(randomD100<85){
                        SetAction(RatState.idle, Random.Range(4f,10f));
                    }
                    else {
                        SetAction(RatState.shortwalk,Random.Range(1f,4f));
                    }
                    currentIdle = randomIdle;
                    _anim.SetInteger("Idle", currentIdle);
                    break;
                case RatMood.nervous:
                    if(randomD100>85){
                        SetAction(RatState.idle, Random.Range(1f,4f));
                    }
                    else {
                        SetAction(RatState.shortwalk,Random.Range(1f,3f));
                    }
                    currentIdle = randomIdle;
                    _anim.SetInteger("Idle", currentIdle);
                    break;
                case RatMood.scared:
                    SetAction(RatState.running, Random.Range(2f,4f));
                    break;
                case RatMood.angry:
                    SetAction(RatState.attacking, 2f);
                    break;
                default:
                    break;
            }
        }

    }

    Obstacle CheckObstacles(){
        RaycastHit hit;
        RaycastHit groundHit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f)){
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            //Debug.Log("HIT");
            return Obstacle.rat;
        }
        else{
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.white);
            if (Physics.Raycast(transform.position+transform.forward, Vector3.down, out groundHit, 10f)){
                 return Obstacle.none;
            }
            else {
                Debug.DrawRay(transform.position +transform.forward,Vector3.down, Color.white);
                return Obstacle.fall;
            }
           
        }

        


        

        
    }

    void RunUpdate(){

    }

    void AttackUpdate(){

    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Rat")){
            int randomIdle = Random.Range(0,2);
            int randomD100 = Random.Range(0,101);
            switch (ratMood)
            {
                case RatMood.nervous:
                    if(randomD100>85){
                        SetAction(RatState.idle, Random.Range(1f,4f));
                    }
                    else {
                        SetAction(RatState.shortwalk,Random.Range(1f,3f));
                    }
                    currentIdle = randomIdle;
                    _anim.SetInteger("Idle", currentIdle);
                    break;
                case RatMood.scared:
                    SetAction(RatState.running, Random.Range(2f,4f));
                    break;
                case RatMood.angry:
                    SetAction(RatState.attacking, 2f);
                    break;
                default:
                    break;
            }
        }
    }


}
