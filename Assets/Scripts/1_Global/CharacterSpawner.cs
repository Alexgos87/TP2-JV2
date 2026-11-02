using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private void Start()
    {
        var cityObjects = Finder.GameController.CityObjects;
        var characters = cityObjects.Characters;
        var spawnPoints = cityObjects.CharacterSpawnPoints.Shuffle();

        for (var i = 0; i < characters.Length; i++)
        {
            characters[i].PlaceAt(spawnPoints[i].Position);
        }
    }
}