
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
    float timer = 0f;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Mathf.Abs(mainCamera.transform.position.z - transform.position.z)));
        mousePos.z = transform.position.z;
        Vector2 dir = mousePos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
    }
    public void Attack()
    {
        Shoot();
    }
    public void Shoot()
    {
        if (timer < cooldown) return;
        timer = 0;
        Instantiate(projectile, transform.position, transform.rotation);
        Debug.Log("Shoooooot");
    }
}