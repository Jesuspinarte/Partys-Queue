using System;
using UnityEngine;

[Serializable]
public struct EyesAccessoryPair
{
  public EnumEyesAccessoryType type;
  public GameObject accessoryObj;
}

[Serializable]
public struct HeadAccessoryPair
{
  public EnumHeadAccessoryType type;
  public GameObject accessoryObj;
}

[Serializable]
public struct MouthAccessoryPair
{
  public EnumMouthAccessoryType type;
  public GameObject accessoryObj;
}

[Serializable]
public struct NeckAccessoryPair
{
  public EnumNeckAccessoryType type;
  public GameObject accessoryObj;
}

[Serializable]
public struct PetsAccessoryPair
{
  public EnumPetsAccessoryType type;
  public GameObject accessoryObj;
}
