using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3.8f;
    public LayerMask obstacleLayers;
    private PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Navigation.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) player = Navigation.player;
        if (player != null && !Physics2D.Linecast(transform.position, player.transform.position, obstacleLayers)) transform.Translate(Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime)- new Vector2(transform.position.x,transform.position.y));
        else if (Navigation.directions[(int)Math.Floor(transform.position.x/Navigation.tilesize),(int)Math.Floor(transform.position.y/Navigation.tilesize)] != 0)transform.Translate(Navigation.oppdir[Navigation.directions[(int)Math.Floor(transform.position.x/Navigation.tilesize),(int)Math.Floor(transform.position.y/Navigation.tilesize)]-1] * moveSpeed * Time.deltaTime);
    }
}
