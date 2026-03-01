using System.Collections.Generic;

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

  public BanLanguageRule(EnumLanguageRuleType languageToBan)
  {
    languageType = languageToBan;
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