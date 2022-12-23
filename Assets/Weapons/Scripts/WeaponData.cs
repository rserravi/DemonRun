using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField] public bool throwable;
    [SerializeField] public float speed;
    [SerializeField] public int damagePoints;
    [SerializeField] public bool gravity;

    [SerializeField] public GameObject[] hitParticles = new GameObject[4];
    [SerializeField] public AudioClip hitSound;
    [SerializeField] public AudioClip drawSound;
    


    private Rigidbody _rigidbody;
    private Collider _collider;
    private AudioSource _audioSource; 

    private float timer=0;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
        damagePoints = Random.Range(damagePoints/2,damagePoints);
        if(drawSound){
            _audioSource.clip = drawSound;
            _audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        _rigidbody.useGravity = gravity;
        timer += Time.deltaTime;

        if (timer > 5f && throwable){
            Explode(null);
        }
        
        
    }

    private void FixedUpdate() {
        //ROTATE TO FACING DIRECTION
        Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity.normalized);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(targetRotation);
        
    }

    private void OnCollisionEnter(Collision other) {
        if (throwable){
            if (!other.collider.CompareTag("Player") && !other.collider.CompareTag("Weapon")){
                Debug.Log("HITTING" + other.collider.name);
               
            }
            Explode(other);
           
        }
    }

    private void Explode(Collision other){
        _audioSource.PlayOneShot(hitSound,1);
        if (other == null){
            Destroy(this.gameObject);
            return;
        }
        if (other.collider.CompareTag("Enemy")){

            Instantiate(hitParticles[0], transform.position, transform.rotation);
            

        }
        else {
            Instantiate(hitParticles[1], transform.position, transform.rotation);
        }
        if (other == null){

        }
        Destroy(this.gameObject);
    }
}
