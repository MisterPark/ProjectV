using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpJewel_03 : Item
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
        int rand = Random.Range(100, 400);
        stat.Increase_FinalStat(StatType.Exp, rand);
    }
}
