using UnityEngine;
using System.Collections;
using NaughtyAttributes;

namespace SkyCrush.Utility
{
    public class SpriteAnimator : MonoBehaviour
    {
        public bool Playing { get; private set; }
        public SpriteAnimation[] Animations => animations;
        public SpriteAnimation CurrentAnimation { get; private set; }
        public int CurrentFrame { get; private set; }

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [ReorderableList]
        [SerializeField]
        private SpriteAnimation[] animations;

        [SerializeField] 
        private SpriteAnimation playAnimationOnStart;
        [SerializeField] 
        private bool loop;

        void Awake()
        {
            if (!spriteRenderer)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnEnable()
        {
            if (playAnimationOnStart != null)
                Play(playAnimationOnStart);
        }

        void OnDisable()
        {
            Playing = false;
            CurrentAnimation = null;
        }

        public void Play(SpriteAnimation animation, bool loop = true, bool continueFrame = false, int startFrame = 0)
        {
            if (animation != null)
            {
                if (animation != CurrentAnimation)
                {
                    ForcePlay(animation, loop, continueFrame, startFrame);
                }
            }
            else
            {
                Debug.LogWarning("could not find animation: " + name);
            }
        }

        public void ForcePlay(SpriteAnimation animation, bool loop = true, bool continueFrame = false, int startFrame = 0)
        {
            if (animation != null)
            {
                this.loop = loop;
                CurrentAnimation = animation;
                Playing = true;
                if (!continueFrame)
                {
                    CurrentFrame = startFrame;
                }
                else
                {
                    NextFrame(animation);
                }
                spriteRenderer.sprite = animation.frames[CurrentFrame];
                StopAllCoroutines();
                StartCoroutine(PlayAnimation(CurrentAnimation));
            }
        }

        public void SlipPlay(SpriteAnimation animation, int wantFrame, params string[] otherNames)
        {
            for (int i = 0; i < otherNames.Length; i++)
            {
                if (CurrentAnimation != null && CurrentAnimation.name == otherNames[i])
                {
                    Play(animation, true, false, CurrentFrame);
                    break;
                }
            }
            Play(animation, true, false, wantFrame);
        }

        public bool IsPlaying(SpriteAnimation animation)
        {
            return (CurrentAnimation != null && CurrentAnimation == animation);
        }

        IEnumerator PlayAnimation(SpriteAnimation animation)
        {
            float timer = 0f;
            float delay = 1f / (float)animation.fps;
            while (loop || CurrentFrame < animation.frames.Length - 1)
            {

                while (timer < delay)
                {
                    timer += Time.deltaTime;
                    yield return 0f;
                }
                while (timer > delay)
                {
                    timer -= delay;
                    NextFrame(animation);
                }

                spriteRenderer.sprite = animation.frames[CurrentFrame];
            }

            CurrentAnimation = null;
        }

        void NextFrame(SpriteAnimation animation)
        {
            CurrentFrame++;
            foreach (AnimationTrigger animationTrigger in CurrentAnimation.triggers)
            {
                if (animationTrigger.frame == CurrentFrame)
                {
                    gameObject.SendMessageUpwards(animationTrigger.name);
                }
            }

            if (CurrentFrame >= animation.frames.Length)
            {
                if (loop)
                    CurrentFrame = 0;
                else
                    CurrentFrame = animation.frames.Length - 1;
            }
        }

        public void SetAnimations(SpriteAnimation[] animations)
        {
            this.animations = animations;
        }

        public void FlipTo(float dir)
        {
            if (dir < 0f)
                spriteRenderer.transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void FlipTo(Vector3 position)
        {
            float diff = position.x - transform.position.x;
            if (diff < 0f)
                spriteRenderer.transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}