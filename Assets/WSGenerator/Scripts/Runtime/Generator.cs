using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public class Generator : MonoBehaviour
    {
        public Sequence Sequence => sequence;
        public StageManager StageManager => _stageManager;
        public bool IsInitilized { get; private set; }

        [SerializeField] private Settings settings;

        [Space(10)]
        [SerializeField] private Sequence sequence;
        [SerializeField] private bool autoPlay = true;

        private PoolManager _poolManager = new PoolManager();
        private StageManager _stageManager = new StageManager();

        private GenerateProcess[] _processes;

        private void Start()
        {
            Init(sequence);

            if (autoPlay)
            {
                StartGeneration();
            }
        }

        public void Init(Sequence sequence)
        {
            this.sequence = sequence;

            _poolManager.Init(sequence, transform);

            _stageManager.OnStartStage += StartGenerateProcesses;
            _stageManager.OnEndStage += StopGenerateProcesses;
            _stageManager.OnUpdateStageValues += UpdateGenerateProcesses;

            _stageManager.Init(this, sequence);

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

            _stageManager.OnStartStage -= StartGenerateProcesses;
            _stageManager.OnEndStage -= StopGenerateProcesses;
            _stageManager.OnUpdateStageValues -= UpdateGenerateProcesses;

            IsInitilized = false;
        }

        private void StopGenerateProcesses(Stage stage)
        {
            if (_processes != null)
            {
                for (var i = 0; i < _processes.Length; i++)
                {
                    _processes[i].Stop();
                }
            }
        }

        private void StartGenerateProcesses(Stage stage)
        {
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
