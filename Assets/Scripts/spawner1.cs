using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner1 : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab của Enemy
    public Transform spawnPoint;    // Vị trí spawn

    public void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            // Tạo một đối tượng Enemy từ Prefab
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Có thể thêm các thao tác khác sau khi spawn, chẳng hạn như thiết lập thông số của Enemy, gắn kết các script, v.v.
        }
        else
        {
            Debug.LogError("EnemyPrefab or SpawnPoint is not assigned!");
        }
    }
}
