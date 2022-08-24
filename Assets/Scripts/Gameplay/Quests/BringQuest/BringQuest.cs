using UnityEngine;
using RiderGame.SO;
using Leopotam.Ecs;
using UniRx;
using System;

namespace RiderGame.Gameplay
{
    public class BringQuest : IQuest
    {
        public ReactiveProperty<QuestStatus> Status { get; set; } = new ReactiveProperty<QuestStatus>();

        public BringQuestConfiguration Configs { get; private set; }
        public GameObject StartObject { get; private set; }
        public Transform Target { get; private set; }

        public BringQuest(BringQuestConfiguration configs, GameObject startObject, Transform target)
        {
            Configs = configs;
            StartObject = startObject;
            Target = target;
        }
    }
}
