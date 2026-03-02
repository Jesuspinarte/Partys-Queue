using Random = UnityEngine.Random;

public class BanAgeRule : IBlacklistRule
{
  private int allowedAge;
  private bool isOlder;

  public BanAgeRule()
  {
    allowedAge = Random.Range(35, 55);
    isOlder = Random.value > 0.5f;
  }

  public bool IsBanned(SuspectProfileData suspect)
  {
    if (isOlder && suspect.age > allowedAge) return true;
    if (!isOlder && suspect.age < allowedAge) return true;
    return false;
  }

  public string GetRuleDescription()
  {
    string edgeCaseText = isOlder ? "OLDER" : "YOUNGER";
    return $"Nobody {edgeCaseText} than {allowedAge}.";
  }
}
