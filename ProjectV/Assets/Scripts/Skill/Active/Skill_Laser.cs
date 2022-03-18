using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Laser : Skill
{
    Vector3 size;
    BoxCollider boxCollider;
    protected override void Awake()
    {
        Kind = SkillKind.Laser;
        activeInterval = 0.25f;
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
            // ���� ������ ���� ����.
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("Laser", transform.position);
        obj.transform.forward = transform.forward;


        float randDistance = UnityEngine.Random.Range(-1, 1);
        Vector3 random = new Vector3(randDistance, 0f, 2f);
        obj.transform.position = transform.position + unit.skillOffsetPosition + random;

        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        missile.speed = speed;
        missile.range = range;
        missile.delay = delay;
        missile.type = MissileType.Directional;
        missile.isPenetrate = true;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        missile.isRotate = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);

    }

    void OnCollisionCallback(Vector3 pos, Unit other )
    {
        GameObject impact = ObjectPool.Instance.Allocate("Impact3");
        impact.transform.position = other.transform.position;

    }
}
