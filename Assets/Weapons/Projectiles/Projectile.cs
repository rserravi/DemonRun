using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // Start is called before the first frame update
    public bool gravity;
    [HideInInspector]
    protected float timer;
    public float lifeTime;
    
    //public AudioClip hitSound;
    public GameObject[] hitParticles = new GameObject[2];

    public int maxDamagePoints;
    public int minDamagePoints;
    protected virtual void Start()
    {
       
        
    }

    protected virtual void Update()
    {
        
       
    }
    protected virtual void FixedUpdate() {

    }

    private void OnCollisionEnter(Collision other) {

        if (other.collider.CompareTag("Enemy")){
            Debug.Log("PARTICLE HITTING" + other.collider.name);
            int dam = Random.Range(minDamagePoints,maxDamagePoints+1);
            GameController.DamageType damageType = GameController.DamageType.normal;
            if (dam == maxDamagePoints){
               damageType = GameController.DamageType.critical;
            }
            if (dam == minDamagePoints){
               damageType = GameController.DamageType.fail;
            }
            GameController.instance.GiveDamage(dam, other.collider.ClosestPoint(transform.position), other.gameObject.GetComponent<Enemy>(), damageType);
            
        }
        if (!other.collider.CompareTag("Player") && !other.collider.CompareTag("Weapon")){

            Explode(other);
        }
           
    }

    protected void Explode(Collision other){
       // _audioSource.PlayOneShot(hitSound,1);
        if (other == null){
            Destroy(this.gameObject);
            return;
        }
        
        if (other.collider.CompareTag("Enemy")){

            Instantiate(hitParticles[0], transform.position, transform.rotation);
            AfterHit(other.collider);

        }
        else {
            Instantiate(hitParticles[1], transform.position, transform.rotation);
        }
        if (other == null){

        }
        Destroy(this.gameObject);
    }

    protected void Explode(){

        Destroy(this.gameObject);
    }

    protected virtual void AfterHit(Collider other){

    }
}
