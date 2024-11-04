using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeGlove : GloveController
{
    void Start()
    {
        GloveName = Managers.Data.GloveDatas[(int)Define.Gloves.Energy].Name;
        Speed = Managers.Data.GloveDatas[(int)Define.Gloves.Energy].Speed;
        Attack = Managers.Data.GloveDatas[(int)Define.Gloves.Energy].Attk;

        SkillCoolDown = Managers.Data.GloveDatas[(int)Define.Gloves.Energy].SkillCoolDown;
        Debug.Log(GloveName);
    }
    public override void Skill1()
    {

    }

    public override void Skill2()
    {

    }

    public override void Skill3()
    {

    }
}
