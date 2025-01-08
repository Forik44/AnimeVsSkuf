using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Application Settings/Application Settings")]
public class ApplicationSettings : ScriptableObject
{
    public int MusicVolume;
    public int SFXVolume;
}
