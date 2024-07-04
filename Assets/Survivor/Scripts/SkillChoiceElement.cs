using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillChoiceElement : MonoBehaviour
{
    [SerializeField]
    Button SelfButton;

    [SerializeField]
    TMP_Text SkillNameText;

    [SerializeField]
    TMP_Text SkillDescText;

    [SerializeField]
    Image SelfBG;

    [SerializeField]
    Image SkillIcon;

    [SerializeField]
    Image[] StarIcons;

    [SerializeField]
    Sprite StarOnSprite;

    [SerializeField]
    Sprite StarOffSprite;

    [SerializeField]
    Sprite StarEvoSprite;

    [SerializeField]
    Sprite ActiveBG_Normal;

    [SerializeField]
    Sprite ActiveBG_Evo;

    [SerializeField]
    Sprite PassiveBG;

    [SerializeField]
    Sprite MeatSprite;

    [SerializeField]
    Sprite GoldSprite;

    /*[SerializeField]
	private EvoIndicator EvoIndicator;*/

    ChoiceType ChoiceType;

    SkillType SkillType;

    PassiveType PassiveType;

    byte SkillGrade;

    Tween CurrentTween;

    Image CurrentTarget;

    private void Start()
    {
        SelfButton.onClick.AddListener(OnSelectSkill);
    }

    public void SetSkillType(SkillType skillType, Sprite skillIconSprite, int skillGrade)
    {
        ChoiceType = ChoiceType.ActiveSkill;
        this.SkillType = skillType;
        this.SkillGrade = (byte)skillGrade;
        SkillIcon.sprite = skillIconSprite;
        if (skillGrade >= 5)
        {
            SelfBG.sprite = ActiveBG_Evo;
        }
        else
        {
            SelfBG.sprite = ActiveBG_Normal;
        }
        if (skillGrade < 5)
        {
            string skillDes;
            skillDes = SkillLoader.Ins.skillData.FindActiveSkill(skillType).description[skillGrade];
            SkillDescText.text = skillDes.ToString();
        }
        SkillNameText.text = skillType.ToString();
        UpdateStarIcons(skillGrade, StarOnSprite, StarOffSprite);
    }

    public void SetPassiveType(PassiveType passiveType, Sprite skillIconSprite, int skillGrade, List<Sprite> evoIcons)
    {
        ChoiceType = ChoiceType.PassiveSkill;
        this.PassiveType = passiveType;
        this.SkillGrade = (byte)skillGrade;
        var passiveDes = SkillLoader.Ins.skillData.FindPassiveSkill(passiveType).description[skillGrade];
        SelfBG.sprite = PassiveBG;
        SkillIcon.sprite = skillIconSprite;
        SkillNameText.text = passiveType.ToString();
        UpdateStarIcons(skillGrade, StarOnSprite, StarOffSprite);
        SkillDescText.text = passiveDes.ToString();
        if (evoIcons != null && evoIcons.Count > 0)
        {

        }
    }

    public void ShowMeatAndGold(bool isGold)
    {
        SkillIcon.sprite = isGold ? GoldSprite : MeatSprite;
    }

    private void UpdateStarIcons(int skillGrade, Sprite starOn, Sprite starOff)
    {
        for (int i = 0; i < StarIcons.Length; i++)
        {
            if (i < skillGrade)
            {
                StarIcons[i].sprite = starOn;
                SetBlinkingStar(StarIcons[i]);
            }
            else
            {
                StarIcons[i].sprite = starOff;
            }
        }
    }

    private void SetBlinkingStar(Image target)
    {
        CurrentTarget = target;
        CurrentTween?.Kill();
        CurrentTween = CurrentTarget.DOFade(0.5f, 0.3f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnSelectSkill()
    {
        if (ChoiceType == ChoiceType.ActiveSkill)
        {
            SkillChoicePopup.Ins.OnSelectSkillType(SkillType, SkillGrade);
            SkillSystemManager.Ins.OnSelectSkillType(SkillType, SkillGrade);
        }
        else if (ChoiceType == ChoiceType.PassiveSkill)
        {
            SkillChoicePopup.Ins.OnSelectPassiveType(PassiveType, SkillGrade);
            SkillSystemManager.Ins.OnSelectPassiveType(PassiveType, SkillGrade);
        }
    }
}
