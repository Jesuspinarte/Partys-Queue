using UnityEngine;

[CreateAssetMenu(fileName = "Suspect Profile", menuName = "Fast Please/Suspect Profile")]
public class SuspectProfile : ScriptableObject
{
  [Header("Suspect Data")]
  public string suspectName;
  public ESuspect suspectShape;
  public ESuspect claimedShape;
  public EColor suspectColor;
  public EColor claimedColor;

  [Header("Suspect Assets")]
  public GameObject prefab;
  public Material material;
}
