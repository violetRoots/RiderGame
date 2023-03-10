using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    public partial class NpcConfiguration
    {
        private bool HasStateDublicates => HasDublicateContainers<StateContainer, State>(states, out hasStateDublicatesMessage);
        private bool HasModifierDublicates => HasDublicateContainers<ModifierContainer, Modifier>(modifiers, out hasModifierDublicatesMessage);

        private void OnValidate()
        {
#if UNITY_EDITOR
            var path = AssetDatabase.GetAssetPath(this);
            path = path.Substring(0, path.LastIndexOf('/'));

            states = UpdateContainers<StateContainer, State>(states, path);
            UpdateStartState();

            modifiers = UpdateContainers<ModifierContainer, Modifier>(modifiers, path);
#endif
        }

        private static List<V> UpdateContainers<V, S>(List<V> containers, string path) where V : DropdownSOContainer<S> where S : ContainerElement
        {
            foreach (var container in containers)
                container.InitValue(path);

            return containers;
        }

        private static bool HasDublicateContainers<V, S>(List<V> containers, out string errMess) where V : DropdownSOContainer<S>, new() where S : ContainerElement
        {
            if (containers == null)
            {
                errMess = string.Empty;
                return false;
            }

            errMess = $"List of {typeof(S).Name} has dublicates\n";

            bool hasDublicates = false;
            for (var i = 0; i < containers.Count; i++)
            {
                var dublicates = containers.Where(container => container.Value == containers[i].Value).ToArray();

                if (dublicates.Length < 2) continue;

                hasDublicates = true;
                errMess += $"{containers[i].Value.GetType().Name}\n";
            }

            return hasDublicates;
        }

        private void UpdateStartState()
        {
            var container = states.FirstOrDefault();
            StartState = container != null ? container.Value : null;
            startStateName = StartState != null ? StartState.GetType().Name : string.Empty;
        }
    }
}
