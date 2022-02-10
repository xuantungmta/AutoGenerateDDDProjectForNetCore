using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace [$project_name].utils.domain_event
{
    /// <summary>
    /// http://www.udidahan.com/2009/06/14/domain-events-salvation/
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate>? actions;

        private static IWindsorContainer Container;

        public static void Init(IWindsorContainer container)
        {
            Container = container;
        }

        /// <summary>
        /// Registers a callback for the given domain event, used for testing only
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public static void Register<T>(Action<T> callback) where T : DomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        /// <summary>
        /// Clears callbacks passed to Register on the current thread
        /// </summary>
        public static void ClearCallbacks()
        {
            actions = null;
        }

        /// <summary>
        /// Raises the given domain event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        public static void Raise<T>(T args) where T : DomainEvent
        {
            if (Container != null)
                foreach (var handler in Container.ResolveAll<Handles<T>>())
                    handler.Handle(args);

            if (actions != null)
                foreach (var action in actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(args);
        }
    }
}
