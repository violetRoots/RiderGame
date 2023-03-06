using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

namespace RiderGame.SO
{
    [Serializable]
    public class ContainerElementsController<V, S> where V : DropdownSOContainer<S>, new() where S : ContainerElement 
    {
        protected List<S> Elements => elements;

        [SerializeField]
        [HideInInspector]
        private List<S> elements;

        private List<S> editorElements;

        public ContainerElementsController(List<V> containers)
        {
            elements = new List<S>();

            var newElements = containers.Select(c => c.Value).ToArray();
            for(var i = 0; i < newElements.Length; i++)
            {
                if (elements.Contains(newElements[i])) continue;

                elements.Add(newElements[i]);
            }

            editorElements = new List<S>(elements);

            if (Application.isPlaying) SetRuntimeMode();
        }

        public void SetRuntimeMode()
        {
            if (elements == null || elements.Count == 0) return;

            elements = new List<S>();

            foreach(var editorElement in editorElements)
            {
                var runtimeElement = ScriptableObject.Instantiate(editorElement);
                runtimeElement.hideFlags = HideFlags.HideAndDontSave;

                elements.Add(runtimeElement);
            }
        }

        public int GetCount()
        {
            return elements != null ? elements.Count : 0;
        }

        public bool TryGet<T>(out T value) where T : ContainerElement
        {
            var element = Get<T>();

            if (element != null && element is T res)
            {
                value = res;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public bool Has<T>() where T : ContainerElement
        {
            var element = Get<T>();

            return element != null;
        }

        protected S Get<T>() where T : ContainerElement
        {
            return elements != null ? elements.Where(element => element is T).FirstOrDefault() : null;
        }
    }
}
