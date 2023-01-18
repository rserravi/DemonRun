using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Pj : MonoBehaviour
{
    public Collider _Collider;
    public Rigidbody _rb;

    private BouyanceObject _boya;
    [Header("Ground Sensors")]
    [SerializeField] private int groundSensorsNumber;
    [SerializeField] private float groundSensorsSize;
    [SerializeField] private float groundSensorYPosition;
    [SerializeField] private Vector3[] groundSensors;
    private RaycastHit[] groundRays;
    [SerializeField] private LayerMask groundLayerMask;

    [Header("Movement constrains")]
    public int SlopeLimit;
    public float StepOffset;
    public float SkinWidth;
    public Vector3 gravity;
    public float playerSliderSpeed;

    public bool canFly;

    [Header("INFO")]

    public bool isGrounded;
    public string movementStyle = "Walking";
    [SerializeField] private float[] groundDistances;
    [SerializeField] private float[] groundInclinationAngles;
    [SerializeField] private float groundAngleAverage = 0;
    [SerializeField] public Vector3 groundSlopeDir = Vector3.zero;
    [SerializeField] private Vector3 groundNormal = Vector3.zero;
    public Ground ground;
    public bool verticalGround;
    public bool overridenDirDetection = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _Collider = GetComponent<Collider>();
        _boya = GetComponent<BouyanceObject>();
        _boya.activated = false;
        groundSensors = new Vector3[groundSensorsNumber];
        groundRays = new RaycastHit[groundSensorsNumber];
        groundDistances = new float[groundSensorsNumber];
        groundInclinationAngles = new float[groundSensorsNumber];

        gravity = Physics.gravity;
        
    }

    // Update is called once per frame
    void Update()
    {
        float increment = groundSensorsSize / (groundSensorsNumber-1) * transform.forward.normalized.x;
        float sensorsPlaceX = transform.position.x + transform.forward.normalized.x *(groundSensorsSize/2);
        
        for (int i = 0; i < groundSensorsNumber; i++)
        {
           // Debug.Log("SENSOR " + i + " en "+ sensorsPlaceX + " con incremento " + increment);
            groundSensors[i] = new Vector3( sensorsPlaceX, transform.position.y -groundSensorYPosition, transform.position.z);
            sensorsPlaceX-=increment;
        }


        float angleSum =0f;
        int hitCount = 0;

        for (int i = 0; i < groundSensorsNumber; i++)
        {
            if (Physics.Raycast(groundSensors[i], -transform.up, out groundRays[i], Mathf.Infinity, groundLayerMask)){
                Debug.DrawRay(groundSensors[i], -transform.up * groundRays[i].distance, Color.cyan);
                groundDistances[i] = groundRays[i].distance;
                groundInclinationAngles[i]= Vector3.Angle(groundRays[i].normal, Vector3.up);
                if (groundInclinationAngles[i]!=0){
                    angleSum+= groundInclinationAngles[i];
                    hitCount++;
                }  
            }
            else{
                Debug.DrawRay(groundSensors[i], -transform.up * 1000, Color.white);
                groundDistances[i] = 1000f;
                groundInclinationAngles[i] =0f;
            }
        }

        CheckGrounded();


        if (angleSum == 0){
            groundAngleAverage = 0;
        }
        else{
            groundAngleAverage = angleSum / hitCount;
        }
                    
        //CHECK SLOPES
        groundNormal = new Vector3(Mathf.Sin(Mathf.Deg2Rad * groundAngleAverage), Mathf.Cos(Mathf.Deg2Rad * groundAngleAverage),0);
        if (!verticalGround){
            groundSlopeDir= Vector3.Cross(transform.right, groundNormal);
            groundSlopeDir.z = 0;
        }else{
            groundSlopeDir= Vector3.Cross(transform.right, groundNormal);
            groundSlopeDir.x = 0;
        }
       
        Debug.DrawRay(transform.position, groundSlopeDir * 4, Color.magenta);
        Debug.DrawRay(transform.position, groundNormal * 4, Color.yellow);
            
    }

    void CheckGrounded(){
        isGrounded = false;

        for (int i = 0; i < groundSensorsNumber; i++)
        {
            if (groundDistances[i] <= SkinWidth+1.5f && (groundRays[i].collider.CompareTag("Slider"))){
                 isGrounded = true;
                 movementStyle = "Slider";
                    //CHECK: AÑADIDO PARA TEST DE LAVA.
                    _boya.water = groundRays[i].collider.GetComponent<MovingLava>();
                    _boya.activated = true;
                    playerSliderSpeed = groundRays[i].collider.GetComponent<MovingLava>().playerSpeed;
            }
            if (groundDistances[i] <= SkinWidth && (groundRays[i].collider.CompareTag("Ground"))){
                isGrounded = true;
                    movementStyle ="Walking";
                    _boya.activated = false;
                    ground = groundRays[i].collider.GetComponent<Ground>();
                    if(ground != null){
                        if (!overridenDirDetection){
                            verticalGround = ground.vertical;
                        }
                    }
                }
            if (groundDistances[i] <= SkinWidth+1.5f && (groundRays[i].collider.CompareTag("Drown"))){
                isGrounded = true;
                movementStyle = "Drowning";
                _boya.activated = false;
            }

                
        }
        if (!isGrounded){
            _boya.activated = false;
            if (canFly){
                movementStyle ="Flying";
            }
            
            //movementStyle = "Walking";
        }
    }

    private void OnCollisionEnter(Collision other) {
      
        ContactPoint[] contacts = new ContactPoint[other.contactCount];
        int contactCount =  other.GetContacts(contacts);
       
        Vector3 impulse = other.impulse;
         
        for (int i = 0; i < other.contactCount; i++)
        {
            //Debug.Log("Impulse " + impulse.ToString());
            if (impulse.x >0){
              //  Debug.Log("IMPACT IN THE LEFT");
            }
            if (impulse.x <0){
                //Debug.Log("IMPACT ON RIGHT");
            }
            if (impulse.y >0){
                //Debug.Log("IMPACT ON BOTTOM");
            }
            if (impulse.y <0){
                //Debug.Log("IMPACT ON TOP");
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other) {
        
    }

}
