using System.Collections;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public class GenerateProcess
    {
        public GenerateObject GenerateObjectInfo { get; private set; }
        public PoolContainer PoolContainer { get; private set; }
        public float Frequency { get; private set; }

        private Settings _settings;
        private Generator _generator;
        private bool _isPaused;
        private bool _isStopped;
        private bool _isFrequencyAboveZero;

        public GenerateProcess(Settings settings, Generator generator, GenerateObject objectInfo, PoolContainer poolContainer)
        {
            _settings = settings;
            _generator = generator;
            GenerateObjectInfo = objectInfo;
            PoolContainer = poolContainer;
        }

        public void UpdateFrequency(float frequency)
        {
            Frequency = frequency;
        }

        public void Start()
        {
            _generator.StartCoroutine(Generation());
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Stop()
        {
            _isStopped = true;
        }

        private IEnumerator Generation()
        {
            while (!_isStopped)
            {
                if (_isPaused)
                {
                    yield return new WaitUntil(() => !_isPaused);
                }

                if(Frequency < _settings.MinFrequencyGenerationValue)
                {
                    yield return new WaitUntil(() => Frequency > _settings.MinFrequencyGenerationValue);
                }

                Generate();

                var waitTime = (float) (1 / Frequency);
                yield return new WaitForSeconds(waitTime);
            }
        }

        private void Generate()
        {
            PoolContainer.Get();
        }
    }
}
