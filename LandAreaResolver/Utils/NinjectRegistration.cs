using LandAreaResolver.Repository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandAreaResolver.Utils
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<ICoordinatesRepository>().To<CoordinatesRepository>();
        }
    }
}