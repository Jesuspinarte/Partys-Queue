using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BanLanguageRule : IBlacklistRule
{
  private EnumLanguageRuleType languageType;
  private Dictionary<EnumLanguageRuleType, string> languageList = new Dictionary<EnumLanguageRuleType, string>
  {
    {EnumLanguageRuleType.NONE, "" },
    {EnumLanguageRuleType.FULL_ENGLISH, "100% ENGLISH (Title & Name)" },
    {EnumLanguageRuleType.FULL_SPANISH, "100% SPANISH (Title & Name)" },
    {EnumLanguageRuleType.PARTIAL_ENGLISH, "with MIXED LANGUAGES (Title ≠ Name)" },
    {EnumLanguageRuleType.PARTIAL_SPANISH, "with MIXED LANGUAGES (Title ≠ Name)" }
  };

  public BanLanguageRule()
  {
    do
      languageType = GameUtils.GetRandomEnumValue<EnumLanguageRuleType>();
    while (languageType == EnumLanguageRuleType.NONE);
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (languageType == EnumLanguageRuleType.NONE) return false;

    switch (languageType)
    {
      case EnumLanguageRuleType.FULL_ENGLISH: return suspect.isEnglishPrefix && suspect.isEnglishName;
      case EnumLanguageRuleType.FULL_SPANISH: return !suspect.isEnglishPrefix && !suspect.isEnglishName;
      case EnumLanguageRuleType.PARTIAL_ENGLISH:
      case EnumLanguageRuleType.PARTIAL_SPANISH:
        return !suspect.isEnglishPrefix && suspect.isEnglishName || suspect.isEnglishPrefix && !suspect.isEnglishName;

      default: return false;
    }
  }

  public string GetRuleDescription()
  {
    if (languageType == EnumLanguageRuleType.NONE) return "";
    return $"Nobody {languageList[languageType]}.";
  }
}

public class BanLanguageUnlessAgeRule : IBlacklistRule
{
  private EnumLanguageRuleType languageType;
  private int allowedAge;
  private bool isOlder;

  private Dictionary<EnumLanguageRuleType, string> languageList = new Dictionary<EnumLanguageRuleType, string>
  {
    {EnumLanguageRuleType.NONE, "" },
    {EnumLanguageRuleType.FULL_ENGLISH, "100% ENGLISH (Title & Name)" },
    {EnumLanguageRuleType.FULL_SPANISH, "100% SPANISH (Title & Name)" },
    {EnumLanguageRuleType.PARTIAL_ENGLISH, "with MIXED LANGUAGES (Title ≠ Name)" },
    {EnumLanguageRuleType.PARTIAL_SPANISH, "with MIXED LANGUAGES (Title ≠ Name)" }
  };

  public BanLanguageUnlessAgeRule()
  {
    allowedAge = Random.Range(30, 61);
    isOlder = Random.value > 0.5f;

    do
      languageType = GameUtils.GetRandomEnumValue<EnumLanguageRuleType>();
    while (languageType == EnumLanguageRuleType.NONE);
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
      case EnumLanguageRuleType.PARTIAL_ENGLISH:
      case EnumLanguageRuleType.PARTIAL_SPANISH:
        return !suspect.isEnglishPrefix && suspect.isEnglishName || suspect.isEnglishPrefix && !suspect.isEnglishName;
      default: return false;
    }
  }

  public string GetRuleDescription()
  {
    string edgeCaseText = isOlder ? "is OLDER" : "is YOUNGER";

    if (languageType == EnumLanguageRuleType.NONE) return "";
    return $"Nobody {languageList[languageType]} UNLESS {edgeCaseText} than {allowedAge}.";
  }
}