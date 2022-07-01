using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public partial class Generator
    {
        public Sequence Sequence => sequence;
        public StageManager StageManager => _stageManager;
        public PoolManager PoolManager => _poolManager;
        public bool IsInitilized { get; private set; }

        private PoolManager _poolManager = new PoolManager();
        private StageManager _stageManager = new StageManager();
        private AreaManager _areaManager = new AreaManager();

        private GenerateProcess[] _processes;

        private void Start()
        {
            Init(sequence);

            if (autoPlay)
            {
                _stageManager.StartUpdateProcess();
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            UpdateInspectorValues();
        }
#endif

        public void Init(Sequence sequence)
        {
            this.sequence = sequence;

            _areaManager.Init(settings, this);
            _poolManager.Init(sequence, transform);

            _stageManager.OnStartStage += StartGenerateProcesses;
            _stageManager.OnEndStage += StopGenerateProcesses;
            _stageManager.OnUpdateStageValues += UpdateGenerateProcesses;

            _stageManager.Init(this, sequence);

            IsInitilized = true;
        }

        public override string ToString()
        {
            if(StageManager.CurrentStage == null)
            {
                return "stage: null";
            }

            var res = $"stage: {StageManager.CurrentStage.Name}\n";
            res += $"process: {(StageManager.Process * 100.0f).ToString("0.00")}%\n";

            for(var i = 0; i < _processes.Length; i++)
            {
                var process = _processes[i];
                res += $"object: {process.GenerateObjectInfo.InstanceName} area: {process.GenerateObjectInfo.AreaIndex} frequency: {process.Frequency.ToString("0.00")}\n";
            }

            return res;
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
                _processes[i] = new GenerateProcess(settings, this, objects[i], _poolManager.GetPoolContainer(objects[i].InstanceName), _areaManager.GetArea(objects[i].AreaIndex));
                _processes[i].Start();
            }
        }

        private void UpdateGenerateProcesses(Stage stage, float process)
        {
            var objects = stage.GenerateObjects;

            for (var i = 0; i < objects.Length; i++)
            {
                var freq = (float) (objects[i].FrequencyCurve.Evaluate(process * GenerateObjectInfo.CurveRange) / settings.FrequencySecondsPerUnit);
                _processes[i].UpdateFrequency(freq);
            }
        }

        private void UpdateInspectorValues()
        {
            if (_stageManager.CurrentStage == null) return;

            stageName = StageManager.CurrentStage.Name;
            stageProcess = StageManager.Process * 100.0f;
        }
    }
}
