using Synapse.Config;
using System.ComponentModel;

namespace RaimbowWaitingMessages
{
    public class Config : AbstractConfigSection
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("The hint in Waiting For Players screen (use <color=%raimbow%> for a raimbow color (remember to close it with </color>))")]
        public string WaitingText { get; set; } = "\n\n\n\n<b>discord.gg/yourdiscord</b>\n<color=%raimbow%><b>My Pretty\nRaimbow Server Name</b></color>";
        }
    }