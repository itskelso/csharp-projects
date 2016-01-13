using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;
namespace TableCrib.Models
{
    public class CardScoreAnimationModel : ObservableObject
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

       public CardScoreAnimationModel()
       {
           Animation = new DoubleAnimation() { Duration = TimeSpan.FromSeconds(1), AutoReverse = true, EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } }; 
       }
        
        


        
    }
}
