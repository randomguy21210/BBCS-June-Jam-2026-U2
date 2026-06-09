
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 2.5f;
    public int pierce = 3;
    public float time = 1f;
    public float rotationOffset = 0f;
    float timer = 0f;
    void Update()
    {
        if (timer + Time.deltaTime < time)
        {
            transform.Rotate(new Vector3(0,0,rotationOffset));
            transform.Translate(speed * Vector2.up * Time.deltaTime);
            transform.Rotate(new Vector3(0,0,-rotationOffset));
        }
        else
        {
            transform.Rotate(new Vector3(0,0,rotationOffset));
            transform.Translate(speed * Vector2.up * (time-timer));
            transform.Rotate(new Vector3(0,0,-rotationOffset));
            Destroy(gameObject, Time.deltaTime);
        }
        timer += Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        pierce--;


        if (pierce == 0) Destroy(gameObject);
    }
}