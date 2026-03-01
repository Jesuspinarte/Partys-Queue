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
    occupation = GameUtils.GetRandomEnumValue<EnumOccupationType>();

    // Demographics
    prefix = isEnglishPrefix ? GetRandomEnglishPrefix() : GetRandomSpanishPrefix();
    activeName = isEnglishName ? _profileData.englishName : _profileData.spanishName;

    // Assign random accessories
    eyesAccessory = GameUtils.GetRandomEnumValue<EnumEyesAccessoryType>();
    headAccessory = GameUtils.GetRandomEnumValue<EnumHeadAccessoryType>();
    mouthAccessory = GameUtils.GetRandomEnumValue<EnumMouthAccessoryType>();
    neckAccessory = GameUtils.GetRandomEnumValue<EnumNeckAccessoryType>();
    petsAccessory = GameUtils.GetRandomEnumValue<EnumPetsAccessoryType>();
  }

  private string GetRandomEnglishPrefix()
  {
    EnumEnglishPrefix prefix = GameUtils.GetRandomEnumValue<EnumEnglishPrefix>();

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
    EnumSpanishPrefix prefix = GameUtils.GetRandomEnumValue<EnumSpanishPrefix>();
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