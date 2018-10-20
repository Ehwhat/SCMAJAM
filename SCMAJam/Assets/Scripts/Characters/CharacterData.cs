using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/New Character Data")]
public class CharacterData : ScriptableObject {

    public string characterName;
    [TextArea]
    public string characterDescription;

    public CharacterManager characterPrefab;

    [System.NonSerialized]
    public float points = 0;
}
