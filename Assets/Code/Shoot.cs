using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour, ISleep
{
    public Transform Target;
    public GameObject Projectile;

    public float speed = 5f;
    public float lifeTime;
    public float cooldown = 1f;
    public float inherit = 0f;

    public Vector2 Velocity { get; set; } // hack

    // Start is called before the first frame update
    void Start()
    {
        if (!Target)
            Target = hors.Hors.transform;
    }


    private void OnDrawGizmos()
    {
        if (Target)
            Gizmos.DrawLine(transform.position, Target.position);
    }

    public IEnumerator AutoFire()
    {
        yield return new WaitForSeconds(cooldown * Random.value);
        while(true)
        {
            Fire(Target.transform.position);
            yield return new WaitForSeconds(cooldown);
        }
    }

    public void Fire(Vector2 target)
    {
        Vector2 start = transform.position;
        var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        var direction = (target - start).normalized * speed;
        projectile.transform.up = direction;
        if(speed != 1f)
            projectile.GetComponent<LinearMove>().speed *= speed;
        if (inherit > 0f)
            projectile.GetComponent<LinearMove>().inherit = Velocity * inherit;
        Destroy(projectile, lifeTime);
    }

    public void Sleep()
    {
        enabled = false;
        StopAllCoroutines();
    }

    public void WakeUp()
    {
        StopAllCoroutines();
        StartCoroutine(AutoFire());
        enabled = true;
    }
}
