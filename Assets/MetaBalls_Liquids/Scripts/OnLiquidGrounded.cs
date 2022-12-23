using UnityEngine;

public class OnLiquidGrounded : MonoBehaviour
{

    [SerializeField] private Animator FluidAnimator;


    void Start()
    {
        if (FluidAnimator == null)
        {
            FluidAnimator = GetComponent<Animator>();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            FluidAnimator.SetBool("IsGrounded", true);
        }
        if (col.gameObject.CompareTag("Player")){
            GameController.instance.TakeDamage(10, transform.position, GameController.DamageType.critical);
            GameController.instance.deadCause = "You have touched the Green Goo.";
        }
    }

    void OnCollisionExit(Collision col)
    {
        /* if (col.gameObject.CompareTag("Ground"))
        {
            FluidAnimator.SetBool("IsGrounded", false);
        } */
        

    } 
}
