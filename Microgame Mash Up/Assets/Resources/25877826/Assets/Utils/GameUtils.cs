using System;
using Random = UnityEngine.Random;

public static class GameUtils
{
  public static string GetOccupationText(EnumOccupationType occupation)
  {
    switch (occupation)
    {
      case EnumOccupationType.CHEF: return "a CHEF";
      case EnumOccupationType.ENGINEER: return "an ENGINEER";
      case EnumOccupationType.HERO: return "a HERO";
      case EnumOccupationType.PHYSICIAN: return "a PHYSICIAN";
      case EnumOccupationType.STREAMER: return "a STREAMER";
      case EnumOccupationType.STUDENT: return "a STUDENT";
      case EnumOccupationType.UBER_DRIVER: return "an UBER DRIVER";
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