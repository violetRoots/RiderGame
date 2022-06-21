using System;
using System.Collections;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public class StageManager
    {
        public bool IsUpdateInProcess { get; private set; }
        public Stage CurrentStage => _stage;
        public float Process => _process;

        public event Action<Stage, float> OnUpdateStageValues;
        public event Action<Stage> OnChangeStage;

        private Settings _settings;
        private Generator _generator;
        private Sequence _sequence;
        private Stage _stage;
        private float _process;

        private int _fixedStageIndex;

        public void Init(Settings settings, Generator generator, Sequence sequence)
        {
            _settings = settings;
            _generator = generator;
            _sequence = sequence;

            SetNextStage(false);
        }

        public void StartUpdateProcess()
        {
            if (IsUpdateInProcess) return;

            IsUpdateInProcess = true;
            _generator.StartCoroutine(UpdateProcess());
        }

        public void PauseUpdateProcess()
        {
            IsUpdateInProcess = false;
            _generator.StopCoroutine(UpdateProcess());
        }

        public void StopUpdateProcess()
        {
            IsUpdateInProcess = false;
            _generator.StopCoroutine(UpdateProcess());

            _fixedStageIndex = 0;
            SetNextStage();
        }

        private void SetNextStage(bool isStartUpdateStage = true)
        {
            PauseUpdateProcess();

            _stage = GetNextStage();
            _process = 0;
            OnChangeStage?.Invoke(_stage);

            if (isStartUpdateStage && _stage != null) StartUpdateProcess();
        }

        private Stage GetNextStage()
        {
            Stage res;

            if (_fixedStageIndex < _sequence.FixedStages.Length)
            {
                res = _sequence.FixedStages[_fixedStageIndex];
                _fixedStageIndex++;
            }
            else
            {
                if (_sequence.RandomStages.Length < 1) return null;

                var randomStageIndex = UnityEngine.Random.Range(0, _sequence.RandomStages.Length);
                res = _sequence.RandomStages[randomStageIndex];
            }

            return res;
        }

        private IEnumerator UpdateProcess()
        {
            var processTime = 0.0f;
            while(_process < 1)
            {
                OnUpdateStageValues?.Invoke(_stage, _process);

                yield return null;

                processTime += Time.deltaTime;
                _process = Mathf.Clamp01((float)(processTime / _stage.Duration));
            }

            SetNextStage();
        }
    }
}
