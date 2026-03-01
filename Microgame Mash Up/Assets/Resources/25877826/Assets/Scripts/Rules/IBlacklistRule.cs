public interface IBlacklistRule
{
  bool IsBanned(SuspectProfileData suspect);
  string GetRuleDescription();
}
