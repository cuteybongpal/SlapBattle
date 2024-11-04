using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGlove : GloveController
{

    void Start()
    {
        GloveName = Managers.Data.GloveDatas[(int)Define.Gloves.Default].Name;
        Speed = Managers.Data.GloveDatas[(int)Define.Gloves.Default].Speed;
        Attack = Managers.Data.GloveDatas[(int)Define.Gloves.Default].Attk;
    }
    public override void Skill1()
    {
        ;
    }

    public override void Skill2()
    {
        ;
    }

    public override void Skill3()
    {
        ;
    }
}
