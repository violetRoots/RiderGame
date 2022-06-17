using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public partial class WSGenerator
    {
        public Sequence Sequence => sequence;

        private Stage _stage;
        private float _processTime;

        public void Init(Sequence sequence)
        {
            this.sequence = sequence;

            InitPools();
        }

        public void StartGeneration()
        {

        }

        public void PauseGeneration()
        {

        }

        public void StopGeneration()
        {

        }

        public void Clear()
        {
            ClearPools();
        }
    }
}
