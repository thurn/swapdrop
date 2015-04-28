using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using SwapDrop.Views;

namespace SwapDrop {
  /// <summary>
  /// The Context is responsible for configuring injection bindings for the rest of the app.
  /// </summary>
  public class SwapDropContext : MVCSContext {
    public SwapDropContext(MonoBehaviour view) : base(view) {
    }

    protected override void addCoreComponents() {
      // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
      base.addCoreComponents();
      injectionBinder.Unbind<ICommandBinder>();
      injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }

    protected override void mapBindings() {
      mediationBinder.Bind<Grid>().To<GridMediator>();
    }
  }
}