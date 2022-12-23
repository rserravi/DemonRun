using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private float originalSpeed;
    public Animator _animator;
    //public Sensors _sensors;
    //public AudioSystem _audioSystem
    //public WeaponsSystem _weapons;

    private Vector2 moveVal;
    private Vector2 lookVal;
    private bool running = false;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        //_sensors = GetComponent<Sensors>();
        //_audioSystem = GetComponent<AudioSystem>();
        //_weapons = GetComponent<WeaponsSystem>(;
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed",speed);
        
    }

    //INPUTS

    void OnMove(InputValue value){
        moveVal = value.Get<Vector2>();
    }

    void OnRun(InputValue value){
        running = value.isPressed;
    }
    void OnLook(InputValue value){
        lookVal= value.Get<Vector2>();

    }

    void OnFire(InputValue value){
        // _weapons.fi
    }

    void OnJump(InputValue value){

    }


}
