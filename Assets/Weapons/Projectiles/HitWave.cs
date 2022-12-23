using UnityEngine;

public class HitWave : Projectile
{
    private ParticleSystem _ps;
    protected override void Start()
    {
        base.Start();
        _ps = GetComponent<ParticleSystem>();
        
    }

    protected override void AfterHit(Collider other)
    {
        base.AfterHit(other);

    }
}
