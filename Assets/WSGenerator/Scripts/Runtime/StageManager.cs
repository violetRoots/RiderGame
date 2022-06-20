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

            if (isStartUpdateStage) StartUpdateProcess();

            OnChangeStage?.Invoke(_stage);
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
                var randomStageIndex = UnityEngine.Random.Range(0, _sequence.RandomStages.Length);
                res = _sequence.RandomStages[randomStageIndex];
            }

            return res;
        }

        private IEnumerator UpdateProcess()
        {
            while(_process < 1)
            {
                var updateInterval = _settings.UseCustomTimeInterval ? _settings.UpdateStageInterval : Time.deltaTime;
                var processStep = (float) (updateInterval / _stage.Duration);

                OnUpdateStageValues?.Invoke(_stage, _process);

                yield return new WaitForSeconds(updateInterval);

                _process = Mathf.Clamp01(_process + processStep);
            }

            SetNextStage();
        }
    }
}
