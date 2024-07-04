using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillChoicePopup : SerializedMonoBehaviour
{
	[SerializeField]
	GameObject SelfPopup;

	[SerializeField]
	List<Image> ActiveSkillIconList;

	[SerializeField]
	List<Image> PassiveSkillIconList;

	[SerializeField]
	List<SkillChoiceElement> SkillChoiceElements;

	[SerializeField]
	Button[] SkillChoiceElementButtons;

	[SerializeField]
	Dictionary<SkillType, Sprite> SkillSpriteDict;

	[SerializeField]
	Dictionary<SkillType, Sprite> EvoSkillSpriteDict;

	[SerializeField]
	Dictionary<PassiveType, Sprite> PassiveSpriteDict;

	Dictionary<SkillType, int> ChosenSkillTypeDict;

	Dictionary<PassiveType, int> ChosenPassiveTypeDict;

	Dictionary<SkillType, int> ChosenEvoTypeDict;

	Dictionary<SkillType, int> RandomSkillTypeDict;
	
	Dictionary<PassiveType, int> RandomPassiveTypeDict;

	SkillLoader skillLoader;

	public static SkillChoicePopup Ins;

	private void Awake()
	{
		if (!Ins)
		{
			Ins = this;
		}
		InitializeDictionaries();
	}

	private void OnDestroy()
	{
	}
	private void InitializeDictionaries()
	{
		ChosenSkillTypeDict = new Dictionary<SkillType, int>();
		ChosenPassiveTypeDict = new Dictionary<PassiveType, int>();
		RandomSkillTypeDict = new Dictionary<SkillType, int>();
		RandomPassiveTypeDict = new Dictionary<PassiveType, int>();
	}

	public void OnShowSkillChoice(Dictionary<SkillType, int> skillTypeDict, Dictionary<PassiveType, int> passiveTypeDict, bool isShowGold)
	{
		RandomSkillTypeDict = skillTypeDict;
		RandomPassiveTypeDict = passiveTypeDict;
		UpdateSkillChoices(isShowGold);
		EnableAllButton();
		OnOpenPopup();
	}
	private void UpdateSkillChoices(bool isShowGold)
	{
		int index = 0;
		Sprite skillSprite;
		foreach (var skill in RandomSkillTypeDict)
		{
			if (index < SkillChoiceElements.Count)
			{
				var skillType = skill.Key;
				var skillGrade = skill.Value;
				if (skillGrade >= 5)
                {
					skillSprite = EvoSkillSpriteDict[skillType];
				}
                else
                {
					skillSprite = SkillSpriteDict[skillType];
				}
				SkillChoiceElements[index].SetSkillType(skillType, skillSprite, skillGrade);
				index++;
			}
			else
			{
				break;
			}
		}

		foreach (var passive in RandomPassiveTypeDict)
		{
			if (index < SkillChoiceElements.Count)
			{
				var passiveType = passive.Key;
				var skillGrade = passive.Value;
				skillSprite = PassiveSpriteDict[passiveType];
				SkillChoiceElements[index].SetPassiveType(passiveType, skillSprite, skillGrade, null);
				index++;
			}
			else
			{
				break;
			}
		}
		RandomPassiveTypeDict.Clear();
		RandomSkillTypeDict.Clear();
	}

	public void OnSelectSkillType(SkillType skillType, byte skillGrade)
	{
		if (ChosenSkillTypeDict.ContainsKey(skillType))
		{
			if(ChosenSkillTypeDict[skillType] <= 5)
				ChosenSkillTypeDict[skillType] += 1;
		}
		else
		{
			ChosenSkillTypeDict.Add(skillType, skillGrade);
		}
		DisableAllButton();
		OnClosePopup();
	}

	public void OnSelectPassiveType(PassiveType skillType, byte skillGrade)
	{
		if (ChosenPassiveTypeDict.ContainsKey(skillType))
		{
			if (ChosenPassiveTypeDict[skillType] <= 5)
				 ChosenPassiveTypeDict[skillType] += 1;
		}
		else
		{
			ChosenPassiveTypeDict.Add(skillType, skillGrade);
		}
		DisableAllButton();
		OnClosePopup();
	}

	private void DisableAllButton()
	{
        foreach (var item in SkillChoiceElementButtons)
        {
			item.interactable = false;
        }
	}

	private void EnableAllButton()
	{
		foreach (var item in SkillChoiceElementButtons)
		{
			item.interactable = true;
		}
	}

	private void OnClosePopup()
	{
		SelfPopup.gameObject.SetActive(false);
	}
	private void OnOpenPopup()
	{
		SelfPopup.gameObject.SetActive(true);
	}

}
