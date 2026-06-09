using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] private GameObject enemyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer -= 1f;
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }
}
