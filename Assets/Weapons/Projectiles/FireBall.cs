using UnityEngine;

public class FireBall : Projectile
{
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    protected override void Start()
    {
         _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected override void  Update()
    {
        
        _rigidbody.useGravity = gravity;
        timer += Time.deltaTime;

        if (timer > lifeTime){
            Explode();
        }
        
        
    }
    protected override void FixedUpdate() {
         //ROTATE TO FACING DIRECTION
        Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity.normalized);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(targetRotation);
    }
}
