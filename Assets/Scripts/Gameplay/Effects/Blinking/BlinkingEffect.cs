using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace RiderGame.Gameplay
{
    public static class BlinkingEffect
    {
        public static void AddToEntity(EcsEntity entity, float duration, float blinkInterval, SpriteRenderer renderer)
        {
            var sequence = InitSpriteBlinkingSequence(blinkInterval, renderer);

            BaseEffect.AddEffect(entity, new Blinking(), duration, () =>
            {
                sequence?.Kill();
                sequence = null;
            });
        }

        private static Sequence InitSpriteBlinkingSequence(float blinkInterval, SpriteRenderer renderer)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(renderer.DOFade(0, blinkInterval));
            sequence.Append(renderer.DOFade(1, blinkInterval));

            sequence.SetLoops(-1);
            return sequence;
        }
    }

    public struct Blinking { }
}
