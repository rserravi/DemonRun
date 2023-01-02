using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]

public class BasePlayerController : MonoBehaviour
{
    public float speed = 0;
    public float maxSpeed = 15f;
    public float jumpPower = 1f;
    private float originalSpeed;

    public bool isGrounded;
    public bool canFly;
    public bool isFlying;
    public GameController.Facing facing = GameController.Facing.right;
    private Animator _animator;
    private CharacterController _cC;
    
    //public Sensors _sensors;
    public AudioSystem _audioSystem;
    public WeaponsSystem _weapons;

    public GameObject hitParticles;

    public HealthSlider healthSlider;
    public Transform meshParent;

    public bool inversedControls;
    
    public Pj _Pj;

    [HideInInspector]
    public Vector2 moveVal;
      [HideInInspector]
    private Vector2 lookVal;
    private bool running = false;
    //private bool jumping = false;
    private bool crounching = false;
    private bool firing1 = false;
    private bool firing2 = false;
    private Vector3 movement;
    private bool vertical;

    private bool beingEaten = false;
    private Transform beingEatenTarget;
    
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //_cC = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _Pj = GetComponent<Pj>();
        _weapons = GetComponent<WeaponsSystem>();

        originalSpeed = speed;
    }
    private void FixedUpdate() {
        if (!beingEaten){
            if (_Pj.movementStyle=="Walking"){
                WalkMovement();
            }
            if (_Pj.movementStyle=="Slider") {
                LavaSlide();
            }
            if (_Pj.movementStyle=="Drowning"){
                Drowning();
            }
        }
        if (beingEaten){
            BeingEaten();            
        }
    }

    private void WalkMovement(){
        
         GameController.instance._CameraAnimator.SetBool("Sliding", false);
         _animator.SetBool("Sliding", false);
         _Pj._rb.mass = 5f;
         _Pj._rb.drag = 1;
         _Pj._rb.angularDrag = 0.05f;
         
         //SETTINGS
        float limitSpeed = maxSpeed;
        float newSpeed = running?speed * 2:speed;
        vertical = _Pj.verticalGround;

        if (firing1){
            moveVal.x = 0f;
        }
        //movement = transform.forward * Mathf.Abs(moveVal.x) * newSpeed;
        movement = _Pj.groundSlopeDir * Mathf.Abs(moveVal.x) * newSpeed;
        
        movement.y = _Pj._rb.velocity.y + Physics.gravity.y; 
        isGrounded = _Pj.isGrounded;

        _Pj._rb.constraints = RigidbodyConstraints.None;
        _Pj._rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (isGrounded && _Pj.ground != null){
            Vector3 newPos;
            if (!_Pj.verticalGround){ //SUELO HORIZONTAL
                
                if (!_Pj.overridenDirDetection){
                    newPos = new Vector3 (transform.position.x, transform.position.y, _Pj.ground.z);
                    transform.position = newPos;
                }
                _Pj._rb.constraints = RigidbodyConstraints.FreezePositionZ;
            }
            else{ //SUELO VERTICAL
                if (!_Pj.overridenDirDetection){
                    newPos = new Vector3 ( _Pj.ground.x, transform.position.y, transform.position.z);
                    transform.position = newPos;
                }
                _Pj._rb.constraints = RigidbodyConstraints.FreezePositionX;
            }
            
        }

         //Set Facing;
        if (moveVal.x<0 && facing== GameController.Facing.right && !vertical) facing = GameController.Facing.left;
        if (moveVal.x>0 && facing== GameController.Facing.left && !vertical) facing = GameController.Facing.right;
        if (moveVal.x<0 && facing== GameController.Facing.back && !vertical) facing = GameController.Facing.left;
        if (moveVal.x>0 && facing== GameController.Facing.front && !vertical) facing = GameController.Facing.right;

        if (moveVal.x<0 && facing== GameController.Facing.right && vertical) facing = GameController.Facing.front;
        if (moveVal.x>0 && facing== GameController.Facing.left && vertical) facing = GameController.Facing.back;
        if (moveVal.x<0 && facing== GameController.Facing.back && vertical) facing = GameController.Facing.front;
        if (moveVal.x>0 && facing== GameController.Facing.front && vertical) facing = GameController.Facing.back;

        
        FaceChange();

        _Pj._rb.useGravity  = !isFlying;
            

        //CROUNCH
        crounching = (isGrounded && moveVal.y <0);
        if (crounching){
            movement.x = movement.x/2;
        }

        //SET ANIMATOR

        _animator.SetFloat("Velocity",_Pj._rb.velocity.magnitude);
        _animator.SetBool("Falling", (!isGrounded && _Pj._rb.velocity.y <0));
        if (_Pj._rb.velocity.y <0){
             _animator.SetBool("Jump",false);
        }
       
        _animator.SetBool("Crounching", crounching);

        if(running){
            limitSpeed = maxSpeed*1.5f;
        }
          
        _Pj._rb.AddForce(movement * Time.fixedDeltaTime, ForceMode.VelocityChange);

        if (_Pj._rb.velocity.magnitude > limitSpeed)
        {
            float brakeSpeed = speed - limitSpeed;  // calculate the speed decrease
        
            Vector3 normalisedVelocity =  _Pj._rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value
        
            _Pj._rb.AddForce(-brakeVelocity);  // apply opposing brake force
        }
    }

    void LavaSlide(){
        
        facing = GameController.Facing.right;
        FaceChange();
        _Pj._rb.constraints = RigidbodyConstraints.None;
        _Pj._rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        float lavaSpeed = _Pj.playerSliderSpeed;
        float bankLimit = 45f;
        float bankSnapFactor = 1f;

        GameController.instance._CameraAnimator.SetBool("Sliding", true);
        _animator.SetBool("Sliding", true);
        Vector3 slideForce = (transform.forward * lavaSpeed + transform.right * speed * moveVal.x);
        slideForce.y = _Pj._rb.velocity.y + Physics.gravity.y; 
        //Debug.Log(slideForce);
         _Pj._rb.AddForce(slideForce * Time.fixedDeltaTime, ForceMode.VelocityChange);

         //ROTATION
         Quaternion bankAngle = Quaternion.Euler(0, bankLimit * moveVal.x, bankLimit * moveVal.x);
         meshParent.localRotation= Quaternion.Lerp(meshParent.localRotation,bankAngle, Time.deltaTime * bankSnapFactor);
    }

    void Drowning(){
        _Pj._rb.constraints = RigidbodyConstraints.None;
        //_Pj._rb.constraints = RigidbodyConstraints.FreezeRotation;
        if (GameController.instance.playerHealth>0){
            TakeDamage(2,transform.position, GameController.DamageType.normal);
        }
        GameController.instance.deadCause ="You have been swallowed by lava. ";
        Vector3 pos = transform.position;
        pos.y -= 4 * Time.deltaTime;
        transform.position = pos;
    }

    //INPUTS

    void OnMove(InputValue value){
        moveVal = value.Get<Vector2>();
        if(inversedControls){
            moveVal= value.Get<Vector2>()*-1;
        }
        _weapons.holdingForwards= (moveVal.x>0 && facing== GameController.Facing.right ||moveVal.x<0 && facing== GameController.Facing.left);
        _weapons.holdingBackwards= (moveVal.x<0 && facing== GameController.Facing.right ||moveVal.x>0 && facing== GameController.Facing.left);
        _weapons.holdingUp = moveVal.y >0;
        //_weapons.holdingDown = moveVal.y <0;
    }

    void BeingEaten(){
        _Pj._rb.position = Vector3.zero;
    }

    void OnRun(InputValue value){
        running = value.isPressed;
    }
    void OnLook(InputValue value){
        lookVal= value.Get<Vector2>();
    }

    void OnFire1(InputValue value){
        firing1 = value.Get<float>() ==1;
        _weapons.holdingFire1 = value.Get<float>() ==1;
    }

    void OnFire2(InputValue value){
        _weapons.holdingFire2 = value.Get<float>() ==1;
    }

    void OnPause(InputValue value){
        if (value.Get<float>() == 1){
            GameController.instance.SetPausePlay();
        }
    }

    void OnInventory(InputValue value){
         if (value.Get<float>() == 1){
            GameController.instance.showingItemsInventory  = !GameController.instance.showingItemsInventory;
        }
    }

    void OnChangeWeapon(InputValue value){
        if (value.Get<float>() ==1){
            _weapons.NextWeapon();
        }
        
    }

    void OnJump(InputValue value){
      
    
            if (_Pj.isGrounded){
                _animator.SetBool("Jump", true);
                _Pj._rb.AddForce(Vector3.up * jumpPower * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        
    }

     private void FaceChange(){
        if (facing== GameController.Facing.right){
            transform.rotation = Quaternion.Euler(0,90,0);
            GameController.instance._CameraAnimator.SetBool("FacingRight", true);
        }
        if (facing == GameController.Facing.left){
            transform.rotation = Quaternion.Euler(0,-90,0);
             GameController.instance._CameraAnimator.SetBool("FacingRight", false);
        }
        if (facing == GameController.Facing.front){
            transform.rotation = Quaternion.Euler(0,180,0);
            GameController.instance._CameraAnimator.SetBool("FacingRight", true);
        }
        if (facing == GameController.Facing.back){
            transform.rotation = Quaternion.Euler(0,0,0);
            GameController.instance._CameraAnimator.SetBool("FacingRight", true);
        }
    }

    public void ChangeCam(){
        //FAKE, DO NOT DELETE
    }

    public void TakeDamage(int damage, Vector3 place, GameController.DamageType damageType){
        
        GameController.instance.playerHealth -= damage;

        healthSlider.maxValue = GameController.instance.playerMaxHealth;
        healthSlider.value = GameController.instance.playerHealth;
        healthSlider.TakeDamage(damage, damageType);

        //INSTANTIATE HITPARTICLES;
        Instantiate(hitParticles,place,Quaternion.identity);
    }

    public void HitAnimationPoint(int combo){

        _weapons.actualWeapon.GetComponent<WeaponClass>().HitAnimationPoint(combo);
    }

    public void GetEaten(Transform pos){
        Debug.Log("BEING EATEN");
        beingEaten = true;
        beingEatenTarget = pos;
        transform.position = pos.position;
        transform.SetParent(pos);
        Instantiate(hitParticles,transform.position,Quaternion.identity);
    }
}
