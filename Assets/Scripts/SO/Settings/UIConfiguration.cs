using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "UIConfigs", menuName = "RiderGame/UIConfigs", order = 0)]
    public class UIConfiguration : SingletonConfiguration<GameConfiguration>
    {
        public float YWayPointOffset => yWayPointOffset;
        public GameObject WayPointIcon => wayPointIcon;
        public GameObject QuestIcon => questIcon;

        [Header("Gameplay")]
        [Header("WayPointPanel")]
        [SerializeField]
        private float yWayPointOffset = 2.5f;
        [SerializeField]
        private GameObject wayPointIcon;
        [SerializeField]
        private GameObject questIcon;
    }
}
