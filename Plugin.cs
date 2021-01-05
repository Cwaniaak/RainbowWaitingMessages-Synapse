using MEC;
using System.Collections.Generic;
using Synapse.Api;
using Synapse.Api.Plugin;

namespace RaimbowWaitingMessages
{
    [PluginInformation(
        Name = "RainbowWaitingMessages", //The Name of Your Plugin
        Author = "Cwaniak U.G", // Your Name
        Description = "RainbowWaitingMessages ported from EXILED", // A Description for your Plugin
        LoadPriority = int.MinValue, //When your Plugin should get loaded (use int.MinValue if you don't know how to use it)
        SynapseMajor = 2, //The Synapse Version for which this Plugin was created for (SynapseMajor.SynapseMinor.SynapsePatch => 2.0.0)
        SynapseMinor = 0,
        SynapsePatch = 0,
        Version = "v.1.0.0" //The Current Version of your Plugin
        )]
    public class PluginClass : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "RainbowWaitingMessages")]
        public Config Config { get; set; }

        internal static PluginClass Singleton;

        internal string ID = "";

        int i = 0;
        string[] colors = { "#f54242", "#f56042", "#f57e42", "#f59c42", "#f5b942", "#f5d742", "#f5f542", "#d7f542", "#b9f542", "#9cf542", "#7ef542", "#60f542", "#42f542", "#42f560", "#42f57b", "#42f599", "#42f5b6", "#42f5d4", "#42f5f2", "#42ddf5", "#42bcf5", "#429ef5", "#4281f5", "#4263f5", "#4245f5", "#5a42f5", "#7842f5", "#9642f5", "#b342f5", "#d142f5", "#ef42f5", "#f542dd", "#f542c2", "#f542aa", "#f5428d", "#f5426f", "#f54251" };
        private static List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

        public override void Load()
        {
            SynapseController.Server.Events.Round.RoundRestartEvent += OnRestarting;
            SynapseController.Server.Events.Round.WaitingForPlayersEvent += OnWaiting;
        }

        public void OnEnabled()
        {
            SynapseController.Server.Events.Round.RoundRestartEvent += OnRestarting;
            SynapseController.Server.Events.Round.WaitingForPlayersEvent += OnWaiting;
        }

        public void OnDisabled()
        {
            SynapseController.Server.Events.Round.RoundRestartEvent -= OnRestarting;
            SynapseController.Server.Events.Round.WaitingForPlayersEvent -= OnWaiting;
        }

        public void OnWaiting()
        {
            coroutines.Add(Timing.RunCoroutine(RaimbowHint()));
        }

        public void OnRestarting()
        {
            foreach (CoroutineHandle coroutine in coroutines)
                Timing.KillCoroutines(coroutine);
            coroutines.Clear();
        }

        IEnumerator<float> RaimbowHint()
        {
            for (; ; )
            {
                if (Synapse.Server.Get.Map.Round.RoundIsActive) yield break;
                foreach (var ply in Synapse.Server.Get.Players)
                    ply.GiveTextHint(Config.WaitingText.Replace("%raimbow%", colors[i++ % colors.Length]), 2);
                yield return Timing.WaitForSeconds(0.5f);
            }
        }
    }
}