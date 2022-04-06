using UnityEngine;

public class Skill_PoisonTrail : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.PoisonTrail;
        activeInterval = 0.1f;
        activeOnce = true;
    }

    public override void Active()
    {
        GameObject obj = ObjectPool.Instance.Allocate("PoisonTrail");

        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }

        //float angle = UnityEngine.Random.Range(-180, 180);
        //float dist = Random.Range(0,6);
        //Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        Vector3 pos = Player.Instance.transform.position+ unit.skillOffsetPosition;


        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Damage = damage;
        missile.Speed = speed;
        //�� �����̴� ���̽� ��Ʈ ������� ����.
        missile.Delay = delay;
        missile.Range = range;
        missile.Type = MissileType.Guided;
        missile.IsPenetrate = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);
        obj.transform.position = pos;
        obj.transform.GetChild(0).gameObject.transform.localScale = Vector3.one * range;

        SoundManager.Instance.PlaySFXSound("PoisonTrail");
    }
    void OnCollisionCallback(Vector3 pos, Unit other )
    {
        //�ǰ� ����Ʈ

    }
}
