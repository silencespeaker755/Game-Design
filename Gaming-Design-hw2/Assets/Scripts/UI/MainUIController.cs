using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

namespace Platformer.UI
{
    /// <summary>
    /// A simple controller for switching between UI panels.
    /// </summary>
    public class MainUIController : MonoBehaviour
    {
        public GameObject[] panels;

        public int score;

        public void SetActivePanel(int index)
        {
            for (var i = 0; i < panels.Length; i++)
            {
                var active = i == index;
                var g = panels[i];
                if (g.activeSelf != active) g.SetActive(active);
            }
        }

        void OnEnable()
        {
            SetActivePanel(0);
        }
    }
}