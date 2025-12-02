using UnityEngine;

public class Castle : MonoBehaviour
{
    public float hp = 500f; // 성의 체력은 유닛보다 높게 설정
    public string teamTag = "Player"; // "Player" 또는 "Enemy"

    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log(gameObject.name + " 성이 공격받음! 남은 체력: " + hp);

        if (hp <= 0)
        {
            DestroyCastle();
        }
    }

    void DestroyCastle()
    {
        Debug.Log(gameObject.name + " 성이 파괴되었습니다!");

        // 여기에 게임 오버 또는 승리 UI를 띄우는 코드를 넣으면 됩니다.
        if (teamTag == "Player")
        {
            Debug.Log("게임 패배... ㅠㅠ");
        }
        else
        {
            Debug.Log("게임 승리! ^^");
        }

        Destroy(gameObject);
    }
}