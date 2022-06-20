using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public class Generator : MonoBehaviour
    {
        public Sequence Sequence => sequence;
        public bool IsInitilized { get; private set; }

        [SerializeField] private Settings settings;

        [Space(10)]
        [SerializeField] private Sequence sequence;
        [SerializeField] private bool playOnAwake = true;

        [Space(10)]
        [ReadOnly]
        [SerializeField]
        private float frequency;

        private PoolManager _poolManager = new PoolManager();
        private StageManager _stageManager = new StageManager();

        private GenerateProcess[] _processes;

        private void Awake()
        {
            Init(sequence);

            if (playOnAwake)
            {
                StartGeneration();
            }
        }

        public void Init(Sequence sequence)
        {
            this.sequence = sequence;

            _poolManager.Init(sequence, transform);

            _stageManager.OnChangeStage += InitGenerateProcesses;
            _stageManager.OnUpdateStageValues += UpdateGenerateProcesses;

            _stageManager.Init(settings, this, sequence);

            IsInitilized = true;
        }

        public void StartGeneration()
        {
            _stageManager.StartUpdateProcess();
        }

        public void PauseGeneration()
        {
            _stageManager.PauseUpdateProcess();
        }

        public void StopGeneration()
        {
            _stageManager.StopUpdateProcess();
        }

        public void Clear()
        {
            sequence = null;

            _poolManager.Clear();

            _stageManager.OnChangeStage -= InitGenerateProcesses;
            _stageManager.OnUpdateStageValues -= UpdateGenerateProcesses;

            IsInitilized = false;
        }

        private void InitGenerateProcesses(Stage stage)
        {
            if(_processes != null)
            {
                for (var i = 0; i < _processes.Length; i++)
                {
                    _processes[i].Stop();
                }
            }

            var objects = stage.GenerateObjects;
            _processes = new GenerateProcess[objects.Length];

            for (var i = 0; i < objects.Length; i++)
            {
                _processes[i] = new GenerateProcess(settings, this, objects[i], _poolManager.GetPoolContainer(objects[i].Instance));
                _processes[i].Start();
            }
        }

        private void UpdateGenerateProcesses(Stage stage, float process)
        {
            var objects = stage.GenerateObjects;

            for (var i = 0; i < objects.Length; i++)
            {
                var freq = (float) (objects[i].FrequencyCurve.Evaluate(process * GenerateObject.CurveRange) / settings.FrequencySecondsPerUnit);
                _processes[i].UpdateFrequency(freq);
            }
        }
    }
}
