using UnityEngine;

public class BlobController : MonoBehaviour
{
    public int minAttack;
    public int maxAttack;
    public Transform bitePlace;
    [Header("Sensors")]
    public bool playerDetected;
    public bool playerClose;
    public float detectRadius;

    public Vector3 playerPosition;
    public float playerDistance;
    public bool swimaway = false;
    public bool jumping = false;

    private Animator _Animator;

    void Start()
    {
       _Animator = GetComponent<Animator>();
   
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController._Player){
            playerPosition = GameController._Player.transform.position;
        }
        else {
            playerPosition = Vector3.zero;
        }
        
        playerDistance = Vector3.Distance(playerPosition,transform.position);
        playerDetected = playerDistance<= detectRadius;
        if(playerDetected){
            //Debug.Log("ATACANDO");
            //Debug.Break();
            Attack();
        }
        if (jumping){
            transform.Translate(transform.up *10* Time.deltaTime + transform.forward *5*Time.deltaTime,Space.World);
        }
        if (swimaway){
            transform.Translate(transform.up *-5 * Time.deltaTime + transform.forward *5 * Time.deltaTime, Space.World); 
        }

    }

    void Attack(){
        _Animator.SetBool("Attack", true);

    }

    void GoUp(string str){
        Debug.Log(str);
        jumping = true;
        
    }

    void Bite(string str){
       // Debug.Break();
    }

    void GoDown(string str){
        Debug.Log(str);
        jumping = false;
        swimaway = true;
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Player")){
            GameController.instance.PlayerDie(GameController.DeadStyle.eaten, bitePlace);
        }
    }
}
