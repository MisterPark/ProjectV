using UnityEngine;

public class Skill_ToxicTotem : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.ToxicTotem;
    }
    protected override void Start()
    {
        Kind = SkillKind.ToxicTotem;
        base.Start();
        activeInterval = 0.1f;
    }

    protected override void Active()
    {
        GameObject obj = ObjectPool.Instance.Allocate("ToxicTotem");

        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = Random.Range(0,6);
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;


        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        missile.speed = speed;
        missile.delay = delay;
        missile.type = MissileType.Guided;
        missile.isPenetrate = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);
        obj.transform.position = pos;
    }
    void OnCollisionCallback(Vector3 pos, Unit other )
    {
        //피격 이펙트

    }
}
