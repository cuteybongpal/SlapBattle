using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GloveController : BaseController
{
    public string GloveName;
    public int Attack;
    public float Speed;
    public List<float> SkillCoolDown = new List<float>();

    protected bool[] CanUseSkill = new bool[3];
    protected override void Init()
    {
        
    }

    public abstract void Skill1();
    public abstract void Skill2();
    public abstract void Skill3();

    protected async UniTaskVoid DisableSkill(int skillCoolDownIndex)
    {
        if (CanUseSkill.Length != SkillCoolDown.Count)
            return;
        CanUseSkill[skillCoolDownIndex] = false;
        await UniTask.Delay(System.TimeSpan.FromSeconds(SkillCoolDown[skillCoolDownIndex]));
        CanUseSkill[skillCoolDownIndex] = true;
    }
}
