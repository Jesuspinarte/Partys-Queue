using System;

using Random = UnityEngine.Random;

public class SuspectProfileData
{
  public SuspectProfile profileData;

  // Demographics
  public int age;
  public string prefix;
  public string activeName;
  public EnumOccupationType occupation;

  // Selected languages
  public bool isEnglishName;
  public bool isEnglishPrefix;

  // Accessories
  public EnumEyesAccessoryType eyesAccessory;
  public EnumHeadAccessoryType headAccessory;
  public EnumMouthAccessoryType mouthAccessory;
  public EnumNeckAccessoryType neckAccessory;
  public EnumPetsAccessoryType petsAccessory;

  public SuspectProfileData(SuspectProfile _profileData)
  {
    profileData = _profileData;

    // Random Demographics
    age = Random.Range(15, 80);
    isEnglishName = Random.value > 0.5f;
    isEnglishPrefix = Random.value > 0.5f;
    occupation = GetRandomEnumValue<EnumOccupationType>();

    // Demographics
    prefix = isEnglishPrefix ? GetRandomEnglishPrefix() : GetRandomSpanishPrefix();
    activeName = isEnglishName ? _profileData.englishName : _profileData.spanishName;

    // Assign random accessories
    eyesAccessory = GetRandomEnumValue<EnumEyesAccessoryType>();
    headAccessory = GetRandomEnumValue<EnumHeadAccessoryType>();
    mouthAccessory = GetRandomEnumValue<EnumMouthAccessoryType>();
    neckAccessory = GetRandomEnumValue<EnumNeckAccessoryType>();
    petsAccessory = GetRandomEnumValue<EnumPetsAccessoryType>();
  }

  // Helpers
  private T GetRandomEnumValue<T>()
  {
    var values = Enum.GetValues(typeof(T));
    int random = Random.Range(0, values.Length);
    return (T)values.GetValue(random);
  }

  private string GetRandomEnglishPrefix()
  {
    EnumEnglishPrefix prefix = GetRandomEnumValue<EnumEnglishPrefix>();

    switch (prefix)
    {
      case EnumEnglishPrefix.DR: return "Dr.";
      case EnumEnglishPrefix.MR: return "Mr.";
      case EnumEnglishPrefix.MS: return "Ms.";
      case EnumEnglishPrefix.SUPER: return "Super";
      default: return "";
    }
  }

  private string GetRandomSpanishPrefix()
  {
    EnumSpanishPrefix prefix = GetRandomEnumValue<EnumSpanishPrefix>();
    switch (prefix)
    {
      case EnumSpanishPrefix.DON: return "Don";
      case EnumSpanishPrefix.DONA: return "Doña";
      case EnumSpanishPrefix.SENOR: return "Señor";
      case EnumSpanishPrefix.SENORA: return "Señora";
      case EnumSpanishPrefix.VECI: return "Veci";
      default: return "";
    }
  }

  // Public

}