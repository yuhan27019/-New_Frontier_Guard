using UnityEngine;

// BaseUnit을 상속받음
public class MeleeUnit : BaseUnit
{
    // 근접 유닛은 BaseUnit의 기본 로직(가까이 가서 때리기)을 그대로 사용하므로
    // 추가 코드가 거의 필요 없습니다.

    // 공격 시 애니메이션 추가 정도만 구현
    protected override void Attack()
    {
        base.Attack(); // 부모의 기본 공격 실행 (데미지 주기)
        // if(anim != null) anim.SetTrigger("Attack"); 
    }
}