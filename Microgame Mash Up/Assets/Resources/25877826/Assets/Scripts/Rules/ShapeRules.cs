using System.Collections.Generic;

public class BanShapeRule : IBlacklistRule
{
  private EnumSuspectShapeType bannedShape;
  private Dictionary<EnumSuspectShapeType, string> shapesList = new Dictionary<EnumSuspectShapeType, string>
    {
      {EnumSuspectShapeType.CIRCLE, "CIRCULAR" },
      {EnumSuspectShapeType.SQUARE, "SQUARED" },
      {EnumSuspectShapeType.TRIANGLE, "TRIANGULAR" }
    };

  public BanShapeRule()
  {
    bannedShape = GameUtils.GetRandomEnumValue<EnumSuspectShapeType>();
  }

  public bool IsBanned(SuspectProfileData suspect) => suspect.profileData.suspectShapeType == bannedShape;
  public string GetRuleDescription() => $"Nobody with {shapesList[bannedShape]} face.";
}

public class BanShapeUnlessOccupationRule : IBlacklistRule
{
  private EnumSuspectShapeType bannedShape;
  private EnumOccupationType allowedOccupation;

  public BanShapeUnlessOccupationRule()
  {
    bannedShape = GameUtils.GetRandomEnumValue<EnumSuspectShapeType>();
    allowedOccupation = GameUtils.GetRandomEnumValue<EnumOccupationType>();
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (suspect.occupation == allowedOccupation) return false;
    if (suspect.profileData.suspectShapeType == bannedShape) return true;
    return false;
  }

  public string GetRuleDescription() => $"Nobody with {bannedShape} face UNLESS they are {GameUtils.GetOccupationText(allowedOccupation)}.";

}