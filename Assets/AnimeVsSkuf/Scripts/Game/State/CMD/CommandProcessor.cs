using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Game.State.CMD
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly Dictionary<Type, object> _handlersMap = new ();

        public CommandProcessor(IGameStateProvider gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
        }
        
        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            _handlersMap[typeof(TCommand)] = handler;
        }

        public bool Process<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (_handlersMap.TryGetValue(typeof(TCommand), out var handler))
            {
                var typedHandler = (ICommandHandler<TCommand>)handler;
                var result = typedHandler.Handle(command);

                if (result)
                {
                    _gameStateProvider.SaveGameState();
                }
                
                return result;
            }
            
            return false;
        }
    }
}