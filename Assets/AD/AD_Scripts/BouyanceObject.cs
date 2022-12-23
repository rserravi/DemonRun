using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class BouyanceObject : MonoBehaviour
{
    public bool activated;
    public float underWaterDrag = 3;
    public float underWaterAngularDrag = 1;

    public float bouyancePointAdjustment = 0f;

    public float airDrag = 0f;
    public float airAngularDrag = 0.05f; 
    public float floatingPower = 15f;

    public float waterHeight = 0f; 
    public MovingLava water; //ASSIGN FROM SCRIPT AND THEN ACTIVATE

    bool underwater;
    public float difference;
    
    
    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>(); 
        airDrag = _rb.drag;
        airAngularDrag = _rb.angularDrag;
        
    }

    void FixedUpdate()
    {
        if (activated==true){
            waterHeight = water.LiquidYAtPosition(transform.position);
            difference = transform.position.y + bouyancePointAdjustment - (water.transform.position.y + waterHeight);
            //difference = transform.position.y - water.LiquidYAtPosition(transform.position);
            if (difference < 0){
                _rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
                if (!underwater){
                    underwater = true;
                    SwitchState(true);
                }
                else if (underwater){
                    underwater = false;
                    SwitchState(false);
                }
            }
        }
        
    }

    void SwitchState (bool isUnderwater){
        if (isUnderwater){
            _rb.drag = underWaterDrag;
            _rb.angularDrag = underWaterAngularDrag;
        }
        else
        {
            _rb.drag = airDrag;
            _rb.angularDrag = airAngularDrag;
        }

    }
}
