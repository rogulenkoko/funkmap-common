﻿
using System.Threading.Tasks;

namespace Funkmap.Cqrs.Abstract
{
    /// <summary>
    /// Handler for executing commands that were sent to a bus.
    /// </summary>
    public interface ICommandHandler
    {
    }

    /// <summary>
    /// Handler for executing commands that were sent to a bus.
    /// </summary>
    /// <typeparam name="TCommand">Command type.</typeparam>
    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : class
    {
        /// <summary>
        /// Executes a command that was sent to a bus.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        Task Execute(TCommand command);
    }
}
