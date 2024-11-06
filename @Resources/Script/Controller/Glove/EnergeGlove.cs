using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeGlove : GloveController, Iinit
{
    public void Init()
    {
        GloveName = Managers.Data.GloveDatas[(int)Define.Gloves.Energy].Name;
        Speed = Managers.Data.GloveDatas[(int)Define.Gloves.Energy].Speed;
        Attack = Managers.Data.GloveDatas[(int)Define.Gloves.Energy].Attk;

        SkillCoolDown = Managers.Data.GloveDatas[(int)Define.Gloves.Energy].SkillCoolDown;
        Debug.Log(GloveName);
        Debug.Log(Attack);
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
