using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("소환 설정")]
    public Transform spawnPoint;
    public GameObject[] unitPrefabs;
    public int[] unitCosts;

    // 성 애니메이션 제어를 위한 변수
    public Animator castleAnimator;

    public void OnClickSpawn(int index)
    {
        if (index < 0 || index >= unitPrefabs.Length) return;

        int cost = unitCosts[index];

        // 1. BattleResourceManager에게 결제 요청
        if (BattleManager.instance != null && BattleManager.instance.TrySpendFood(cost))
        {
            // 2. 유닛 생성
            SpawnUnit(index);

            // 3. 성 애니메이션 발동
            if (castleAnimator != null)
            {
                castleAnimator.SetTrigger("Spawn");
            }
        }
    }

    void SpawnUnit(int index)
    {
        GameObject newUnit = Instantiate(unitPrefabs[index], spawnPoint.position, Quaternion.identity);
        newUnit.tag = "Player";
        newUnit.layer = LayerMask.NameToLayer("Player");
        newUnit.name = unitPrefabs[index].name;
    }
}