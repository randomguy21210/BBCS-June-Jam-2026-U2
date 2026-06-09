using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputAction moveAction;
    public float moveSpeed = 8;
    float timer = 0f;
    public Tilemap tilemap;
    private int [,] visited = new int[Navigation.mapx,Navigation.mapy];
    private int health;
    public void DecreaseHealth(int amt){health -= amt;}
    private GameObject[] Weapons = new GameObject[3];
    private IWeapon[] IWeapons = new IWeapon[3];
    [SerializeField] private GameObject Weapon1;

    [SerializeField] private InputAction AttackAction;
    private void OnEnable()
    {
        AttackAction.Enable();
        AttackAction.performed += OnAttackPressed;
    }
    private void OnDisable()
    {
        AttackAction.performed -= OnAttackPressed;
        AttackAction.Disable();
    }
    private void OnAttackPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Atk");
        IWeapons[0].Attack();
    }

    void Start()
    {
        Weapons[0] = Instantiate(Weapon1, gameObject.GetComponent<Transform>());
        IWeapons[0] = Weapons[0].GetComponent<IWeapon>();
        Navigation.player = this;
        moveAction = InputSystem.actions.FindAction("Move");
        BoundsInt bounds = tilemap.cellBounds;
        Vector3Int bottomLeft = new Vector3Int(bounds.xMin, bounds.yMin, 0);
        foreach(Vector3Int cellPosition in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(cellPosition))
            {
                int rX = cellPosition.x - bottomLeft.x;
                int rY = cellPosition.y - bottomLeft.y;
                visited[2*rX,2*rY] = 1;
                visited[2*rX+1,2*rY+1] = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveValue = moveAction.ReadValue<Vector2>().normalized;
        transform.Translate(moveValue * moveSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer > 0.1f)
        {
            timer -= 0.1f;
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            int [,] directions = new int[Navigation.mapx,Navigation.mapy];
            int [,] cloneVisited = (int[,])visited.Clone();
            directions[(int)Math.Floor(transform.position.x/Navigation.tilesize),(int)Math.Floor(transform.position.y/Navigation.tilesize)] = 0;
            cloneVisited[(int)Math.Floor(transform.position.x/Navigation.tilesize),(int)Math.Floor(transform.position.y/Navigation.tilesize)] = 1;
            queue.Enqueue(new Vector2Int((int)Math.Floor(transform.position.x/Navigation.tilesize), (int)Math.Floor(transform.position.y/Navigation.tilesize)));
            while(queue.Count > 0)
            {
                Vector2Int t = queue.Peek();
                queue.Dequeue();
                for(int i = 0; i < 4; i++)
                {
                    int a = Navigation.diri[i].x;
                    int b = Navigation.diri[i].y;
                    if (!(t.x + a >= 0 && t.x + a <= Navigation.mapx-1 && t.y + b >= 0 && t.y + b <= Navigation.mapy-1)) continue;
                    if (cloneVisited[t.x+a,t.y+b] == 1) continue;
                    {
                        cloneVisited[t.x+a,t.y+b] = 1;
                        directions[t.x+a,t.y+b] = i+1;
                        queue.Enqueue(new Vector2Int(t.x+a,t.y+b));
                    }
                }

            }
            Navigation.directions = directions;
        }
    }
}
