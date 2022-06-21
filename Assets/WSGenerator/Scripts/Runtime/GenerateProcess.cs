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

                var clampFrequency = Mathf.Clamp(Frequency, _settings.MinFrequencyGenerationValue, _settings.MaxFrequencyGenerationValue);
                var pastTime = 0.0f;
                var waitTime = (float) (1 / clampFrequency);
                while(pastTime < waitTime)
                {
                    waitTime = (float)(1 / clampFrequency);

                    yield return null;

                    pastTime += Time.deltaTime;
                }
            }
        }

        private void Generate()
        {
            var generatedObject = PoolContainer.Get();

            Vector3 pos = new Vector3();
            pos.x = Random.Range(GenerateObjectInfo.Area.point1.x, GenerateObjectInfo.Area.point2.x);
            pos.y = Random.Range(GenerateObjectInfo.Area.point1.y, GenerateObjectInfo.Area.point2.y);

            generatedObject.transform.position = pos;
        }
    }
}
