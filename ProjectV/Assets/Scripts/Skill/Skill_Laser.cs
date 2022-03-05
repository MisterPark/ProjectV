using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Laser : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.Laser;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        Kind = SkillKind.Laser;
        base.Start();
    }

    // Update is called once per frame
    protected override void Active()
    {
        

        GameObject nearest = null;
        SpawnManager.Instance.SpawnQueue.Dequeue(out nearest);

        if (nearest == null)
        {
            // 적이 없으면 공격 안함.
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("Laser",transform.position);
        obj.transform.forward=transform.forward;
        
        
        float randDistance = UnityEngine.Random.Range(-1, 1);
        Vector3 random = new Vector3(randDistance, 0f, 2f);
        obj.transform.position = transform.position+unit.skillOffsetPosition+ random;

        DamageObject dmgobj = obj.GetComponent<DamageObject>();
        dmgobj.delay = delay;
        dmgobj.damage = damage;
        dmgobj.duration = duration;
        dmgobj.OnCollision.RemoveAllListeners();
        dmgobj.OnCollision.AddListener(OnCollisionCallback);
    }

    void OnCollisionCallback(Vector3 pos)
    {
        GameObject impact = ObjectPool.Instance.Allocate("Impact3");
        impact.transform.position = pos;

    }
}
