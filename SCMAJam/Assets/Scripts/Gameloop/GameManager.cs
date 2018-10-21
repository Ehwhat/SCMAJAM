using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public CharacterData[] characterData;
    public Transform levelHolder;

    public Vector2[] spawnPoints = new Vector2[4];
    public CharacterManager[] characterManagers;
    public UIPopupManager popupManager;

    private void Start()
    {
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        characterManagers = new CharacterManager[characterData.Length];
        for (int i = 0; i < characterData.Length; i++)
        {
            characterManagers[i] = Instantiate(characterData[i].characterPrefab, spawnPoints[i], Quaternion.identity);
            characterManagers[i].popupManager = popupManager;
        }
    }

}
