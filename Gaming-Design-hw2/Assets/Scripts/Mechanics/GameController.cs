using Platformer.Core;
using Platformer.Model;
using Platformer.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        [SerializeField] private GameObject UI;

        [SerializeField] public TMP_Text _passTime;

        public static float passTime = 0.0f;

        public static bool gameover = false;

        private void Start()
        {
            passTime = 0.0f;
            gameover = false;
            Time.timeScale = 1;
        }

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();

            passTime += Time.deltaTime;
            _passTime.SetText(string.Format("{0}:{1}:{2}", ((int)passTime/60).ToString("D2"),
                ((int)passTime%60).ToString("D2"), ((int)(passTime*100)%100).ToString("D2")));

            if (gameover)
            {
                Time.timeScale = 0;
                UI.SetActive(true);
            }
        }
    }
}