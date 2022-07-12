using UnityEngine;

namespace SkyCrush.Utility
{
    [CreateAssetMenu(fileName = "SpriteAnimation_1", menuName = "Utility/SpriteAnimation")]
    public class SpriteAnimation : ScriptableObject
    {
        public int fps;
        public Sprite[] frames;

        [NonReorderable]
        public AnimationTrigger[] triggers;
    }

    [System.Serializable]
    public class AnimationTrigger
    {
        public int frame;
        public string name;
    }
}
