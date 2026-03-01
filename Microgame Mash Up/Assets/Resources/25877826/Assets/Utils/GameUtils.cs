using System;
using Random = UnityEngine.Random;

public static class GameUtils
{
  public static string GetOccupationText(EnumOccupationType occupation)
  {
    switch (occupation)
    {
      case EnumOccupationType.CHEF: return "Chef";
      case EnumOccupationType.ENGINEER: return "Engineer";
      case EnumOccupationType.HERO: return "Hero";
      case EnumOccupationType.PHYSICIAN: return "Physician";
      case EnumOccupationType.STREAMER: return "Streamer";
      case EnumOccupationType.STUDENT: return "Student";
      case EnumOccupationType.UBER_DRIVER: return "Uber Driver";
      default: return "";
    }
  }

  public static T GetRandomEnumValue<T>()
  {
    var values = Enum.GetValues(typeof(T));
    int random = Random.Range(0, values.Length);
    return (T)values.GetValue(random);
  }
}