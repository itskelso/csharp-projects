using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;

namespace TableCrib.Models
{
    public class ScoreFlyoutAnimationModel : ObservableObject
    {
        private DoubleAnimation animation;

        public DoubleAnimation Animation
        {
            get { return animation; }
            set
            {
                Set(() => Animation, ref animation, value);
            }
        }

        public ScoreFlyoutAnimationModel()
        {
            this.Animation = new DoubleAnimation() { By = 100, EasingFunction = new BounceEase() { EasingMode = EasingMode.EaseIn, Bounces = 2 } };
            
        }


        
    }
}
