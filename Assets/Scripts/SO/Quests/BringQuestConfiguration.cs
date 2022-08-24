using UnityEngine;
using NaughtyAttributes;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "BringQuest_1", menuName = "RiderGame/Quests/BringQuest", order = 0)]
    public class BringQuestConfiguration : ScriptableObject
    {
        public GameObject CompleteQuestPrefab => completeQuestPrefab;
        public float Distance => Random.Range(distance.x, distance.y);

        [SerializeField] private GameObject completeQuestPrefab;
        [MinMaxSlider(0, 100)]
        [SerializeField] private Vector2 distance;

        [Space(10)]
        [SerializeField] private Sprite questIcon;
        [SerializeField] private Sprite wayPointIcon;
    }
}
