using Platformer.Core;
using Platformer.Model;
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

        [SerializeField] public TMP_Text _passTime;

        public static float passTime = 0.0f;

        private void Start()
        {
            passTime = 0.0f;
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
            //_passTime.text = string.Format("{0}", passTime.ToString("F2"));
        }
    }
}