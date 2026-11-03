using UnityEngine;

// Mémoire du personnage.
//
// Vous aurez à compléter cette classe en lui ajoutant toute valeur que vous considérez importante à conserver sur le
// long terme. Si la valeur est modifiable, créez une propriété avec un "get" et un "set" directement, sans validation.
//
// Le blackboard contient aussi des informations comme la liste de toutes les épiceries et de tous les cafés. Il s'agit
// des mêmes informations contenues dans le GameController.
//
// Votre blackboard devrait faire la maintenance des informations qu'il contient. Par exemple, si un déchet ayant été
// aperçu récemment est ramassé par un autre personnage, le blackboard devrait le retirer de sa mémoire.
//
// Vous avez à modifier cette classe pour le travail.
public class CharacterBlackboard : MonoBehaviour
{
    [Header("Knowledge")]
    [field:SerializeField] public Building House { get; private set; }
    [field:SerializeField] public Building Workplace { get; private set; }
    [field:SerializeField] public Character[] Friends { get; private set; } = { };
    public Building[] HouseBuildings => gameController.CityObjects.HouseBuildings;
    public Building[] WorkplaceBuildings => gameController.CityObjects.WorkplaceBuildings;
    public Building[] FoodBuildings => gameController.CityObjects.FoodBuildings;
    public Building[] SocialBuildings => gameController.CityObjects.SocialBuildings;
    
    [Header("Memory")]
    [field:SerializeField] public Trash LastSeenTrash { get; set; }
    [field:SerializeField] public Character LastSeenFriend { get; set; }

    private GameController gameController;
    
    // Initialization.
    private void Awake()
    {
        gameController = Finder.GameController;
    }
    
    // Blackboard maintenance.
    private void Update()
    {
        if (LastSeenTrash != null && !LastSeenTrash.IsAvailable) LastSeenTrash = null;
        if (LastSeenFriend != null && !LastSeenFriend.IsAvailable) LastSeenFriend = null;
    }
}