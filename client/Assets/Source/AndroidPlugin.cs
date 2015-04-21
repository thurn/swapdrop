using UnityEngine;
using System.Collections;

namespace SwapDrop {

  public class AndroidPlugin {
    
    private AndroidJavaObject plugin = null;
    private AndroidJavaObject activity = null;
    
    public void Init() {
      if(plugin == null) {
        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
          activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
   
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("ca.thurn.swapdrop.AndroidPlugin")) {
          if(pluginClass != null) {
            plugin = pluginClass.CallStatic<AndroidJavaObject>("instance");
            plugin.Call("setActivity", activity);
          }
        }
      }
    }

    public void ShowStatusBar() {
      activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
        plugin.Call("showStatusBar");
      }));
    }

    public int GetStatusBarHeight() {
      return plugin.Call<int>("getStatusBarHeight");
    }
  }

}