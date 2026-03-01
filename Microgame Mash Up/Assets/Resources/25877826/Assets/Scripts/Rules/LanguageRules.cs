using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BanLanguageRule : IBlacklistRule
{
  private EnumLanguageRuleType languageType;
  private Dictionary<EnumLanguageRuleType, string> languageList = new Dictionary<EnumLanguageRuleType, string>
  {
    {EnumLanguageRuleType.NONE, "" },
    {EnumLanguageRuleType.FULL_ENGLISH, "A FULL ENGLISH NAME" },
    {EnumLanguageRuleType.FULL_SPANISH, "A FULL SPANISH NAME" },
    {EnumLanguageRuleType.PARTIAL_ENGLISH, "A PARTIAL ENGLISH NAME" },
    {EnumLanguageRuleType.PARTIAL_SPANISH, "A PARTIAL SPANISH NAME" }
  };

  public BanLanguageRule()
  {
    languageType = GameUtils.GetRandomEnumValue<EnumLanguageRuleType>();
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (languageType == EnumLanguageRuleType.NONE) return false;

    switch (languageType)
    {
      case EnumLanguageRuleType.FULL_ENGLISH: return suspect.isEnglishPrefix && suspect.isEnglishName;
      case EnumLanguageRuleType.FULL_SPANISH: return !suspect.isEnglishPrefix && !suspect.isEnglishName;
      case EnumLanguageRuleType.PARTIAL_ENGLISH: return suspect.isEnglishPrefix || suspect.isEnglishName;
      case EnumLanguageRuleType.PARTIAL_SPANISH: return !suspect.isEnglishPrefix || !suspect.isEnglishName;
      default: return false;
    }
  }

  public string GetRuleDescription()
  {
    if (languageType == EnumLanguageRuleType.NONE) return "";
    return $"Nobody with {languageList[languageType]}.";
  }
}

public class BanUnlessLanguageRule : IBlacklistRule
{
  private EnumLanguageRuleType languageType;
  private int allowedAge;
  private bool isOlder;

  private Dictionary<EnumLanguageRuleType, string> languageList = new Dictionary<EnumLanguageRuleType, string>
  {
    {EnumLanguageRuleType.NONE, "" },
    {EnumLanguageRuleType.FULL_ENGLISH, "A FULL ENGLISH NAME" },
    {EnumLanguageRuleType.FULL_SPANISH, "A FULL SPANISH NAME" },
    {EnumLanguageRuleType.PARTIAL_ENGLISH, "A PARTIAL ENGLISH NAME" },
    {EnumLanguageRuleType.PARTIAL_SPANISH, "A PARTIAL SPANISH NAME" }
  };

  public BanUnlessLanguageRule()
  {
    languageType = GameUtils.GetRandomEnumValue<EnumLanguageRuleType>();
    allowedAge = Random.Range(15, 80);
    isOlder = Random.value > 0.5f;
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (languageType == EnumLanguageRuleType.NONE) return false;
    if (isOlder && suspect.age > allowedAge) return false;
    if (!isOlder && suspect.age < allowedAge) return false;

    switch (languageType)
    {
      case EnumLanguageRuleType.FULL_ENGLISH: return suspect.isEnglishPrefix && suspect.isEnglishName;
      case EnumLanguageRuleType.FULL_SPANISH: return !suspect.isEnglishPrefix && !suspect.isEnglishName;
      case EnumLanguageRuleType.PARTIAL_ENGLISH: return suspect.isEnglishPrefix || suspect.isEnglishName;
      case EnumLanguageRuleType.PARTIAL_SPANISH: return !suspect.isEnglishPrefix || !suspect.isEnglishName;
      default: return false;
    }
  }

  public string GetRuleDescription()
  {
    string edgeCaseText = isOlder ? "is OLDER" : "is YOUNGER";

    if (languageType == EnumLanguageRuleType.NONE) return "";
    return $"Nobody with {languageList[languageType]} UNLESS {edgeCaseText} than {allowedAge}.";
  }
}