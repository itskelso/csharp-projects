using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;




namespace TableCrib
{
   public static class StoryboardExtensions 
    {


       public static Task BeginAsync( Storyboard storyboard)
       {
           System.Threading.Tasks.TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
           if (storyboard == null)
               tcs.SetException(new ArgumentNullException("Storyboard null"));
           else
           {
               EventHandler<object> onComplete = null;
               onComplete = (s, e) =>
               {
                   storyboard.Completed -= onComplete;
                   tcs.SetResult(true);
               };
               storyboard.Completed += onComplete;
               storyboard.Begin();
           }
           return tcs.Task;
       }
    }
}
