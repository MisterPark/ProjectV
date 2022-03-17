using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpJewel_01 : Item
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Use()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        int rand = Random.Range(1, 5);
        stat.Increase_FinalStat(StatType.Exp, rand);
    }
}
