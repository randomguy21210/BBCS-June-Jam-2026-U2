
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RangedWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject projectile;
    public float rotationOffset = 0;
    public float cooldown = 0.5f;
    public bool isPlayerWeapon = false;
    float timer = 0f;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        transform.position = transform.parent.position;
        timer += Time.deltaTime;
        if (isPlayerWeapon)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Mathf.Abs(mainCamera.transform.position.z - transform.parent.position.z)));
            mousePos.z = transform.parent.position.z;
            Vector2 dir = mousePos - transform.parent.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.Translate(Vector3.right * Navigation.weaponDistance);
            transform.Rotate(0,0,rotationOffset);
        }
        else
        {
            Vector3 playerPos = Navigation.player.transform.position;
            playerPos.z = transform.parent.position.z;
            Vector2 dir = playerPos - transform.parent.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,angle);
            transform.Translate(Vector3.right * Navigation.weaponDistance);
            transform.Rotate(0,0,rotationOffset);
        }
    }
    public void Attack()
    {
        Shoot();
    }
    public void Shoot()
    {
        if (timer < cooldown) return;
        timer = 0;
        Projectile p = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,transform.rotation.eulerAngles.z-rotationOffset)).GetComponent<Projectile>();
        p.isPlayerProjectile = isPlayerWeapon;

    }
    public bool canSwing()
    {
        return timer >= cooldown;
    }
    public float getAttackRange()
    {
        return float.MaxValue;
    }
}