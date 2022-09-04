using System.Collections;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public class GenerateProcess
    {
        private const int MaxSpawnAttempts = 1000;
        public GenerateObjectInfo GenerateObjectInfo { get; private set; }
        public PoolContainer PoolContainer { get; private set; }
        public float Frequency { get; private set; }

        private Settings _settings;
        private Generator _generator;
        private Stage _stage;
        private Area2D _area;
        private SpawnPointer2D _pointer;
        private bool _isPaused;
        private bool _isStopped;
        private int _spawnAttempts;

        public GenerateProcess(Settings settings, Generator generator, Stage stage, GenerateObjectInfo objectInfo, PoolContainer poolContainer, Area2D area)
        {
            _settings = settings;
            _generator = generator;
            _stage = stage;
            GenerateObjectInfo = objectInfo;
            PoolContainer = poolContainer;
            _area = area;

            var spawnBounds = objectInfo.Instance.GetComponentsInChildren<SpawnBounds2D>();
            if (spawnBounds.Length > 0)
            {
                _pointer = GameObject.Instantiate(_settings.SpawnPointer2D, _area.transform);
                _pointer.name = $"Spawn Pointer [{objectInfo.InstanceName}]";
                _pointer.Init(spawnBounds[0], SetPointerPosition);
                SetPointerPosition();
            }
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
            var processTime = 0.0f;

            while (!_isStopped)
            {
                while (_isPaused) yield return null;

                processTime += Time.deltaTime;

                while (Frequency < _settings.MinFrequencyGenerationValue)
                {
                    if (processTime < _stage.Duration) yield return null;
                    else
                    {
                        DestroyPointer();
                        yield break;
                    }
                }

                if (_pointer == null)
                {
                    Generate();
                }
                else
                {
                    _generator.StartCoroutine(GenerateByPointer());
                }

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

            DestroyPointer();
        }

        private void DestroyPointer()
        {
            if (_pointer != null)
            {
                GameObject.Destroy(_pointer.gameObject);
            }
        }

        private void Generate()
        {
            var generatedObject = PoolContainer.Get();

            generatedObject.transform.position = GetRandomPointInsideArea(_area);
        }

        private IEnumerator GenerateByPointer()
        {
            yield return new WaitUntil(() => _pointer != null && (_pointer.ReadyToSpawn || _spawnAttempts > MaxSpawnAttempts));

            if(_spawnAttempts > MaxSpawnAttempts)
            {
                Debug.LogWarning($"Object {GenerateObjectInfo.InstanceName} cannot be spawn in area {_area.name}: doesn't free area enough");
                yield break;
            }

            var generatedObject = PoolContainer.Get();

            generatedObject.transform.position = _pointer.transform.position;
        }

        private void SetPointerPosition()
        {
            _pointer.transform.position = GetRandomPointInsideArea(_area);

            _spawnAttempts++;
        }

        private static Vector3 GetRandomPointInsideArea(Area2D area)
        {
            Vector3 pos = new Vector3();
            pos.x = Random.Range(area.Center.x - (area.Size.x / 2.0f), area.Center.x + (area.Size.x / 2.0f));
            pos.y = Random.Range(area.Center.y - (area.Size.y / 2.0f), area.Center.y + (area.Size.y / 2.0f));
            return pos;
        }
    }
}
