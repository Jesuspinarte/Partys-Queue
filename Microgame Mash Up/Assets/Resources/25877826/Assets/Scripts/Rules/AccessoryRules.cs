using System.Collections.Generic;

// Eyes rule
public class BanEyeAccessoryRule : IBlacklistRule
{
  private EnumEyesAccessoryType bannedEyeAccessory;
  private Dictionary<EnumEyesAccessoryType, string> eyesAccessoryList = new Dictionary<EnumEyesAccessoryType, string>
  {
    {EnumEyesAccessoryType.NONE, "" },
    {EnumEyesAccessoryType.GLASSES, "NORMAL GLASSES" },
    {EnumEyesAccessoryType.PIRATE_PATCH, "a PIRATE PATCH" },
    {EnumEyesAccessoryType.SUNGLASSES, "SUNGLASSES" }
  };

  public BanEyeAccessoryRule()
  {
    do
      bannedEyeAccessory = GameUtils.GetRandomEnumValue<EnumEyesAccessoryType>();
    while (bannedEyeAccessory == EnumEyesAccessoryType.NONE);
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (bannedEyeAccessory == EnumEyesAccessoryType.NONE) return false;
    return suspect.eyesAccessory == bannedEyeAccessory;
  }

  public string GetRuleDescription()
  {
    if (bannedEyeAccessory == EnumEyesAccessoryType.NONE) return "";
    return $"Anyone WEARING {eyesAccessoryList[bannedEyeAccessory]}.";
  }

}

// Mouth rule
public class BanMouthAccessoryRule : IBlacklistRule
{
  private EnumMouthAccessoryType bannedMouthAccessory;
  private Dictionary<EnumMouthAccessoryType, string> mouthAccessoryList = new Dictionary<EnumMouthAccessoryType, string>
  {
    {EnumMouthAccessoryType.NONE, "" },
    {EnumMouthAccessoryType.BEARD, "a BEARD" },
    {EnumMouthAccessoryType.LIPS, "CRAZY LIPS" },
    {EnumMouthAccessoryType.MUSTACHE, "A MUSTACHE" }
  };

  public BanMouthAccessoryRule()
  {
    do
      bannedMouthAccessory = GameUtils.GetRandomEnumValue<EnumMouthAccessoryType>();
    while (bannedMouthAccessory == EnumMouthAccessoryType.NONE);
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (bannedMouthAccessory == EnumMouthAccessoryType.NONE) return false;
    return suspect.mouthAccessory == bannedMouthAccessory;
  }

  public string GetRuleDescription()
  {
    if (bannedMouthAccessory == EnumMouthAccessoryType.NONE) return "";
    return $"Any FACE with {mouthAccessoryList[bannedMouthAccessory]}.";
  }
}

// Head rule
public class BanHeadAccessoryRule : IBlacklistRule
{
  private EnumHeadAccessoryType bannedHeadAccessory;
  private Dictionary<EnumHeadAccessoryType, string> headAccessoryList = new Dictionary<EnumHeadAccessoryType, string>
  {
    {EnumHeadAccessoryType.NONE, "" },
    {EnumHeadAccessoryType.BASEBALL_HAT, "a BASEBALL HAT" },
    {EnumHeadAccessoryType.COWBOY_HAT, "a COWBOY HAT" },
    {EnumHeadAccessoryType.RIBBON, "a RIBBON" }
  };

  public BanHeadAccessoryRule()
  {
    do
      bannedHeadAccessory = GameUtils.GetRandomEnumValue<EnumHeadAccessoryType>();
    while (bannedHeadAccessory == EnumHeadAccessoryType.NONE);
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (bannedHeadAccessory == EnumHeadAccessoryType.NONE) return false;
    return suspect.headAccessory == bannedHeadAccessory;
  }

  public string GetRuleDescription()
  {
    if (bannedHeadAccessory == EnumHeadAccessoryType.NONE) return "";
    return $"Anyone WEARING {headAccessoryList[bannedHeadAccessory]}.";
  }
}

// Neck rule
public class BanNeckAccessoryRule : IBlacklistRule
{
  private EnumNeckAccessoryType bannedNeckAccessory;
  private Dictionary<EnumNeckAccessoryType, string> neckAccessoryList = new Dictionary<EnumNeckAccessoryType, string>
  {
    {EnumNeckAccessoryType.NONE, "" },
    {EnumNeckAccessoryType.NECKLACE, "a FANCY NECKLACE" },
    {EnumNeckAccessoryType.SCARF, "a BORING SCARF" },
    {EnumNeckAccessoryType.TIE, "an OFFICE TIE" }
  };

  public BanNeckAccessoryRule()
  {
    do
      bannedNeckAccessory = GameUtils.GetRandomEnumValue<EnumNeckAccessoryType>();
    while (bannedNeckAccessory == EnumNeckAccessoryType.NONE);
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (bannedNeckAccessory == EnumNeckAccessoryType.NONE) return false;
    return suspect.neckAccessory == bannedNeckAccessory;
  }

  public string GetRuleDescription()
  {
    if (bannedNeckAccessory == EnumNeckAccessoryType.NONE) return "";
    return $"Anyone WEARING {neckAccessoryList[bannedNeckAccessory]}.";
  }
}

// Pets rule
public class BanPetsAccessoryRule : IBlacklistRule
{
  private EnumPetsAccessoryType bannedPetsAccessory;
  private Dictionary<EnumPetsAccessoryType, string> petsAccessoryList = new Dictionary<EnumPetsAccessoryType, string>
  {
    {EnumPetsAccessoryType.NONE, "" },
    {EnumPetsAccessoryType.LEFT_PET, "a PET on the LEFT" },
    {EnumPetsAccessoryType.RIGHT_PET, "a PET on the RIGHT" },
    {EnumPetsAccessoryType.DOUBLE_PETS, "one PET or MORE" }
  };

  public BanPetsAccessoryRule()
  {
    do
      bannedPetsAccessory = GameUtils.GetRandomEnumValue<EnumPetsAccessoryType>();
    while (bannedPetsAccessory == EnumPetsAccessoryType.NONE);
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (bannedPetsAccessory == EnumPetsAccessoryType.NONE) return false;
    // If double pets rule is enable and the player has a pet
    if (bannedPetsAccessory == EnumPetsAccessoryType.DOUBLE_PETS && suspect.petsAccessory != EnumPetsAccessoryType.NONE) return true;
    return suspect.petsAccessory == bannedPetsAccessory;
  }

  public string GetRuleDescription()
  {
    if (bannedPetsAccessory == EnumPetsAccessoryType.NONE) return "";
    return $"Anyone COMMING with {petsAccessoryList[bannedPetsAccessory]}.";
  }
}
