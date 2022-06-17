using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public partial class WSGenerator : MonoBehaviour
    {
        [SerializeField] private Settings settings;

        [Space(10)]
        [SerializeField] private Sequence sequence;
        [SerializeField] private bool playOnAwake = true;

        private void Awake()
        {
            if (playOnAwake)
                Init(sequence);
        }
    }
}
