using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Animation;
using GalaSoft.MvvmLight;

namespace TableCrib.Models
{
    public class PegAnimationModel : ObservableObject
    {

        private DoubleAnimation animation;

        public DoubleAnimation Animation
        {
            get
            {
                return animation;
            }
            set
            {
                Set(() => Animation, ref animation, value);
            }
        }
        

    }
}
