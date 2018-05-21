﻿using System.Runtime.CompilerServices;
using Sakuno.ING.Composition;

[assembly: InternalsVisibleTo("Sakuno.ING.Game.Logger.Design")]

namespace Sakuno.ING.Game.Logger
{
    internal class Module : IExposableModule
    {
        public void Expose(IBuilder builder)
        {
            builder.RegisterType<LoggerContext>();
            builder.RegisterType<Logger>();
        }
        public void Initialize(IResolver resolver) => throw new System.NotImplementedException();
    }
}
