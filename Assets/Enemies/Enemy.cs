using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]

public class Enemy : MonoBehaviour
{
    
    public enum Aggressivity {coward, ignoreAll, valiant, aggressive, xtreme}
    public enum EnemyState {waiting, wandering, goingToPlayer, attacking, retreating, runningAway}
    [Header("Behaviour")]
    public EnemyState enemyState;
     
    protected Rigidbody _RigidBody;
    protected Animator _Animator;
    protected AudioSource _AS_StepsFlaps;  
    protected AudioSource _AS_OtherSounds;

    [Header("Characteristics")]

    public int minAttack;
    public int maxAttack;
    public float speed;
    public int damagePoints;
    public int maxDamagePoints;
    public HealthSlider healthSlider;
    public Aggressivity aggressivity;
    public float detectRadius;
    public float alertRadius;

    public GameObject explotionPrefab;
    public GameObject hitParticles;
    
    [Header("Sounds")]

    public AudioClip audioNormal;
    public AudioClip audioDie;
    public AudioClip audioAttack;
    public AudioClip audioFlapStep;

    public Vector3 target;

    [Header("Sensors")]
    public bool playerDetected;
    public bool playerClose;

    public Vector3 playerPosition;
    public float playerDistance;

    protected virtual void Start() {
        _RigidBody = GetComponent<Rigidbody>();
        _Animator = GetComponent<Animator>();
        _AS_StepsFlaps = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        _AS_OtherSounds  = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

    }

    protected virtual void Die(){
        _Animator.SetBool("Attack", false);
        _Animator.SetBool("Die", true);
        Instantiate(explotionPrefab,transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

    protected virtual void FixedUpdate()
    {
        playerPosition = GameController._Player.transform.position;
        playerDistance = Vector3.Distance(playerPosition,transform.position);
        TargetUpdate();
        CheckSensors();
        CheckBehaviours();
        Move();
        if (damagePoints <= 0) Die();
    }

     private void Update() {
        if (_AS_StepsFlaps.spatialBlend !=1){
             GameController.instance._Level.ConfigureAudioSource(_AS_StepsFlaps, "FX");
        }

        if (_AS_OtherSounds.spatialBlend !=1){
            GameController.instance._Level.ConfigureAudioSource(_AS_OtherSounds, "FX");
        }
    }


    protected virtual void Move(){
        Vector3 direction;
              
        direction = target - transform.position;
      
        _RigidBody.AddForce(direction.normalized * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            
        //ROTATE TO FACING DIRECTION
        Quaternion targetRotation = (_RigidBody.velocity.normalized == Vector3.zero? Quaternion.identity:Quaternion.LookRotation(_RigidBody.velocity.normalized));
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
        _RigidBody.MoveRotation(targetRotation);        
    }

    protected virtual void TargetUpdate(){
        if (ReachedTarget(0.4f)){
            Debug.Log(this.name + " HAS REACHED TARGET");
        }
    }

    public virtual void Flap(string Flap){
        _AS_StepsFlaps.PlayOneShot(audioFlapStep);
    }

    protected bool ReachedTarget(float threshold){
       
        return(Vector3.Distance(transform.position, target)<=threshold);

    }

    protected virtual void CheckSensors(){
        
        playerDetected = playerDistance<= detectRadius;
        playerClose = playerDistance <= alertRadius;
    }

    protected virtual void CheckBehaviours(){
        if (playerClose && aggressivity == Aggressivity.coward){
            target = transform.position - playerPosition;
        }
    }

    protected EnemyState AggressivityCheck(){
        int refNum =0;
        EnemyState result= EnemyState.waiting;
        switch (aggressivity)
        {
            case Aggressivity.coward:
                refNum = 10;
                break;
            case Aggressivity.valiant:
                refNum = 50;
                break;
            case Aggressivity.aggressive:
                refNum = 80;
                break;
            case Aggressivity.xtreme:
                refNum = 100;
                break;
            default:
                break;
        }

       int dice = Random.Range(0,100);
       Debug.Log(dice);
       if (dice == 0){
            result = EnemyState.attacking;
       }
       if (dice == 100){
            result = EnemyState.runningAway;
       }
       if (dice <= refNum){
            result = EnemyState.attacking;
       }
       if (dice > refNum){
            result = EnemyState.retreating;
       }
       if (dice > refNum+30){
            result = EnemyState.runningAway;
       }
       return result;

    }

    protected virtual void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Player")){
            int dam = Random.Range(minAttack,maxAttack+1);
            GameController.DamageType damageType = GameController.DamageType.normal;
            if (dam == maxAttack){
               damageType = GameController.DamageType.critical;
            }
            if (dam == minAttack){
               damageType = GameController.DamageType.fail;
            }
            GameController.instance.TakeDamage(dam, other.GetContact(0).point,damageType);
            _RigidBody.AddExplosionForce(dam*2, other.GetContact(0).point,1,0,ForceMode.Impulse);
        }
        if (other.collider.CompareTag("Shield")){
            other.gameObject.GetComponent<Shield>()._weapon.ShieldDamage(minAttack,GameController.DamageType.normal);
        }
    }

    public void TakeDamage(int damage, Vector3 place, GameController.DamageType damageType){
        Debug.Log("ENEMY TAKING  DAMAGE");
        damagePoints -= damage;
         
        healthSlider.maxValue = maxDamagePoints;
        healthSlider.value = damagePoints;
        healthSlider.TakeDamage(damage, damageType);

        //INSTANTIATE HITPARTICLES;
        Instantiate(hitParticles,place,Quaternion.identity);
    }

    public void AttackSound(string attack){
        Debug.Log("ATTACK");
        
    }


}
