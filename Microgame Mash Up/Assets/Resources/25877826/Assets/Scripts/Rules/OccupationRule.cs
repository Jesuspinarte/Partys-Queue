public class BanOccupationRule : IBlacklistRule
{
  private EnumOccupationType bannedOccupation;

  public BanOccupationRule()
  {
    bannedOccupation = GameUtils.GetRandomEnumValue<EnumOccupationType>();
  }

  public bool IsBanned(SuspectProfileData suspect) => bannedOccupation == suspect.occupation;
  public string GetRuleDescription() => $"Nobody with the occupation of {bannedOccupation}.";
}
