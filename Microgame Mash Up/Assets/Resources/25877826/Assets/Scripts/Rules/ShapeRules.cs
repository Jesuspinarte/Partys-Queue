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

  public BanShapeRule(EnumSuspectShapeType shapeToBan)
  {
    bannedShape = shapeToBan;
  }

  public bool IsBanned(SuspectProfileData suspect) => suspect.profileData.suspectShapeType == bannedShape;
  public string GetRuleDescription() => $"Nobody with {shapesList[bannedShape]} face.";
}

public class BanShapeUnlessOccupationRule : IBlacklistRule
{
  private EnumSuspectShapeType bannedShape;
  private EnumOccupationType allowedOccupation;

  public BanShapeUnlessOccupationRule(EnumSuspectShapeType shape, EnumOccupationType occupation)
  {
    bannedShape = shape;
    allowedOccupation = occupation;
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (suspect.profileData.suspectShapeType == bannedShape && suspect.occupation != allowedOccupation) return true;
    return false;
  }

  public string GetRuleDescription() => $"Nobody with {bannedShape} face. UNLESS they are a {GameUtils.GetOccupationText(allowedOccupation)}.";

}