using UnityEngine;

public class BouncingProjectile : Projectile
{
    public LayerMask wallLayer;
    private Collider2D projectileCollider;
    public override void Start()
    {
        base.Start();
        projectileCollider = GetComponent<Collider2D>();
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && isPlayerProjectile)
        {
            pierce--;
            IEnemy enemy = other.GetComponent<IEnemy>();
            enemy.dealDamage(damage);
            //move to nearest other enemy

        }
        else if (other.CompareTag("Player") && !isPlayerProjectile)
        {
            //useless currently
            pierce--;
            PlayerController player = other.GetComponent<PlayerController>();
            player.dealDamage(damage);
        }
        else if (other.CompareTag("Walls"))
        {
            float angle = Mathf.Deg2Rad * (transform.eulerAngles.z - rotationOffset);
            Vector2 dir = new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
            RaycastHit2D hit = Physics2D.CircleCast(rb.position-dir*0.7f, 0.05f, dir, 1.2f, wallLayer);
            Vector2 hitNormal = -dir;
            if (hit.collider != null) hitNormal = hit.normal;
            Debug.Log(hit.collider==null);
            Vector2 reflected = Vector2.Reflect(dir, hitNormal).normalized;
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg*Mathf.Atan2(reflected.y,reflected.x)+rotationOffset,Vector3.forward);
        }
        if (pierce == 0) Die();
    }
}