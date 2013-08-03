
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLROBS;
using Starboard.Model;
using Starboard.View;
using Starboard.ViewModel;


namespace Starboard
{
    public class StarboardImageSourceFactory : AbstractImageSourceFactory
    {
        public StarboardImageSourceFactory()
        {
            ClassName = "StarboardImageSourceClass";
            DisplayName = "Starboard";
        }

        public override ImageSource Create(XElement data)
        {
            return new StarboardImageSource(data);
        }

        public override bool ShowConfiguration(XElement data)
        {
            //StarboardConfigurationDialog dialog = new StarboardConfigurationDialog(data);
            //return dialog.ShowDialog().GetValueOrDefault(false);

            //MainWindowView m = new MainWindowView();
            
            //return m.ShowDialog().GetValueOrDefault();
            return true;
        }
    }
}
