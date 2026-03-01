using TMPro;
using UnityEngine;

public class RuleController : MonoBehaviour
{
  [SerializeField] private TMP_Text ruleText;

  public void UpdateRuleText(string text) => ruleText.text = text;
}
