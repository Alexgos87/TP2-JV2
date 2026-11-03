using System.Linq;
using UnityEngine;

public class CharacterSensor : MonoBehaviour
{
    private Character character;
    private CharacterBlackboard blackboard;

    private void Awake()
    {
        character = GetComponent<Character>();
        blackboard = GetComponent<CharacterBlackboard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Trash trash))
        {
            if (character.TrashBehaviour == CityCharacterTrashBehaviour.PickUp) blackboard.LastSeenTrash = trash;
        }
        else if (!other.isTrigger && other.TryGetComponent(out Character otherCharacter))
        {
            if (blackboard.Friends.Contains(otherCharacter)) blackboard.LastSeenFriend = otherCharacter;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Trash trash))
        {
            if (blackboard.LastSeenTrash == trash) blackboard.LastSeenTrash = null;
        }
        else if (!other.isTrigger && other.TryGetComponent(out Character otherCharacter))
        {
            if (blackboard.LastSeenFriend == otherCharacter) blackboard.LastSeenFriend = null;
        }
    }
}