using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLROBS;
using Starboard;
using Starboard.ViewModel;
using Starboard.Model;
using Starboard.View;
using System.Windows;

namespace CSharpSamplePlugin
{
    class StarboardPlugin : AbstractPlugin
    {
        public StarboardPlugin()
        {
            // Setup the default properties
            Name = "Starboard OBS Plugin";
            Description = "Shows a starboard thingy";
        }
        
        public override bool LoadPlugin()
        {
            // No idea if this is how you are supposed to do it

            if (Application.Current == null)
            {
                new Application();
            }
            Application.ResourceAssembly = typeof(MainWindowView).Assembly;

            DataTemplateManager manager = new DataTemplateManager();
            manager.RegisterDataTemplate(typeof(Player), typeof(PlayerView));
            manager.RegisterDataTemplate(typeof(ScoreboardControlPanelViewModel), typeof(ScoreboardControlPanelView));
            manager.RegisterDataTemplate(typeof(SettingsPanelViewModel), typeof(SettingsPanelView));
            manager.RegisterDataTemplate(typeof(ScoreboardControlViewModel), typeof(ScoreboardControl));
            
            API.Instance.AddImageSourceFactory(new StarboardImageSourceFactory());

            return true;
        }
    }
}
