using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Develop.Scripts.Controllers
{
    public static class ControllersFactory
    {
        public static AppController AppController() => new AppController();

        public static ObjectController ObjectController() => new ObjectController();

        public static IronTypeController IronTypeController() => new IronTypeController();

        public static ScreenshootController ScreenshootController() => new ScreenshootController();

        public static ProductTypeController ProductTypeController() => new ProductTypeController();
    }
}
