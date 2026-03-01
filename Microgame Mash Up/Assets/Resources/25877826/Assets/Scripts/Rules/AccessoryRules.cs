using System.Collections.Generic;

// Eyes rule
public class BanEyeAccessoryRule : IBlacklistRule
{
  private EnumEyesAccessoryType bannedEyeAccessory;
  private Dictionary<EnumEyesAccessoryType, string> eyesAccessoryList = new Dictionary<EnumEyesAccessoryType, string>
  {
    {EnumEyesAccessoryType.NONE, "" },
    {EnumEyesAccessoryType.GLASSES, "NORMAL GLASSES" },
    {EnumEyesAccessoryType.PIRATE_PATCH, "A PIRATE PATCH" },
    {EnumEyesAccessoryType.SUNGLASSES, "SUNGLASSES" }
  };

  public BanEyeAccessoryRule(EnumEyesAccessoryType accessoryToBan)
  {
    bannedEyeAccessory = accessoryToBan;
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (bannedEyeAccessory == EnumEyesAccessoryType.NONE) return false;
    return suspect.eyesAccessory == bannedEyeAccessory;
  }

  public string GetRuleDescription()
  {
    if (bannedEyeAccessory == EnumEyesAccessoryType.NONE) return "";
    return $"Anyone with {eyesAccessoryList[bannedEyeAccessory]}.";
  }

}

// Mouth rule
public class BanMouthAccessoryRule : IBlacklistRule
{
  private EnumMouthAccessoryType bannedMouthAccessory;
  private Dictionary<EnumMouthAccessoryType, string> mouthAccessoryList = new Dictionary<EnumMouthAccessoryType, string>
  {
    {EnumMouthAccessoryType.NONE, "" },
    {EnumMouthAccessoryType.BEARD, "BEARD" },
    {EnumMouthAccessoryType.LIPS, "CRAZY LIPS" },
    {EnumMouthAccessoryType.MUSTACHE, "A MUSTACHE" }
  };

  public BanMouthAccessoryRule(EnumMouthAccessoryType accessoryToBan)
  {
    bannedMouthAccessory = accessoryToBan;
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (bannedMouthAccessory == EnumMouthAccessoryType.NONE) return false;
    return suspect.mouthAccessory == bannedMouthAccessory;
  }

  public string GetRuleDescription()
  {
    if (bannedMouthAccessory == EnumMouthAccessoryType.NONE) return "";
    return $"Anyone with {mouthAccessoryList[bannedMouthAccessory]}.";
  }
}

// Head rule
public class BanHeadAccessoryRule : IBlacklistRule
{
  private EnumHeadAccessoryType bannedHeadAccessory;
  private Dictionary<EnumHeadAccessoryType, string> headAccessoryList = new Dictionary<EnumHeadAccessoryType, string>
  {
    {EnumHeadAccessoryType.NONE, "" },
    {EnumHeadAccessoryType.BASEBALL_HAT, "A BASEBALL HAT" },
    {EnumHeadAccessoryType.COWBOY_HAT, "COWBOY HAT" },
    {EnumHeadAccessoryType.RIBBON, "A RIBBON" }
  };

  public BanHeadAccessoryRule(EnumHeadAccessoryType accessoryToBan)
  {
    bannedHeadAccessory = accessoryToBan;
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (bannedHeadAccessory == EnumHeadAccessoryType.NONE) return false;
    return suspect.headAccessory == bannedHeadAccessory;
  }

  public string GetRuleDescription()
  {
    if (bannedHeadAccessory == EnumHeadAccessoryType.NONE) return "";
    return $"Anyone with {headAccessoryList[bannedHeadAccessory]}.";
  }
}

// Neck rule
public class BanNeckAccessoryRule : IBlacklistRule
{
  private EnumNeckAccessoryType bannedNeckAccessory;
  private Dictionary<EnumNeckAccessoryType, string> neckAccessoryList = new Dictionary<EnumNeckAccessoryType, string>
  {
    {EnumNeckAccessoryType.NONE, "" },
    {EnumNeckAccessoryType.NECKLACE, "A FANCY NECKLACE" },
    {EnumNeckAccessoryType.SCARF, "A BORING SCARF" },
    {EnumNeckAccessoryType.TIE, "AN OFFICE TIE" }
  };

  public BanNeckAccessoryRule(EnumNeckAccessoryType accessoryToBan)
  {
    bannedNeckAccessory = accessoryToBan;
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (bannedNeckAccessory == EnumNeckAccessoryType.NONE) return false;
    return suspect.neckAccessory == bannedNeckAccessory;
  }

  public string GetRuleDescription()
  {
    if (bannedNeckAccessory == EnumNeckAccessoryType.NONE) return "";
    return $"Anyone with {neckAccessoryList[bannedNeckAccessory]}.";
  }
}

// Pets rule
public class BanPetsAccessoryRule : IBlacklistRule
{
  private EnumPetsAccessoryType bannedPetsAccessory;
  private Dictionary<EnumPetsAccessoryType, string> petsAccessoryList = new Dictionary<EnumPetsAccessoryType, string>
  {
    {EnumPetsAccessoryType.NONE, "" },
    {EnumPetsAccessoryType.LEFT_PET, "A PET ON THE LEFT" },
    {EnumPetsAccessoryType.RIGHT_PET, "A PET ON THE RIGHT" },
    {EnumPetsAccessoryType.DOUBLE_PETS, "MORE THAN ONE PET" }
  };

  public BanPetsAccessoryRule(EnumPetsAccessoryType accessoryToBan)
  {
    bannedPetsAccessory = accessoryToBan;
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
    return $"Anyone with {petsAccessoryList[bannedPetsAccessory]}.";
  }
}
