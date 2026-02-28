using UnityEngine;

[CreateAssetMenu(fileName = "Suspect Profile", menuName = "Fast Please/Suspect Profile")]
public class SuspectProfile : ScriptableObject
{
  [Header("Suspect Data")]
  public string englishName;
  public string spanishName;
  public EnumSuspectShapeType suspectShapeType;

  [Header("Suspect Assets")]
  public GameObject prefab;
}
