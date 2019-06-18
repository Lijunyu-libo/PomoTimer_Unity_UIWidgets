using QFramework.UIWidgets.ReduxPersist;
using Unity.UIWidgets;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PomoTimerApp
{
    public class App : UIWidgetsPanel
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            FontManager.instance.addFont(Resources.Load<Font>("MaterialIcons-Regular"), familyName: "Material Icons");

        }

        protected override Widget createWidget()
        {
            //Redux配置store
            var store = new Store<AppState>(
                //reduce中判断action类型返回state
                reducer:AppReducer.Reduce,
                //initialState: new AppState());
                //使用本地存储
                initialState: AppState.Load(),
                ReduxPersistMiddleware.create<AppState>());

            //Redux通过provider使用store
            return new StoreProvider<AppState>(
                store:store,
                child: new MaterialApp(
                    theme: new ThemeData(
                        primarySwatch: Colors.teal
                    ),
                    home: new HomePage()
                ));
        }
        
    }

}

