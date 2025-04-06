using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;

namespace Mantis.Engine.Services
{
    public class SystemService<TSystem>(IEnumerable<TSystem> systems) : ISystemService<TSystem>
        where TSystem : ISystem
    {
        private readonly List<TSystem> _systems = [.. systems];

        public IEnumerable<TSystem> GetAll()
        {
            return this._systems;
        }

        public T GetSystem<T>()
            where T : ISystem
        {
            foreach (var system in systems)
            {
                if (system is T requestedSystem)
                {
                    return requestedSystem;
                }
            }

            throw new KeyNotFoundException(nameof(TSystem));
        }

        public IEnumerable<T> GetSystems<T>()
        {
            return this._systems.OfType<T>();
        }

        public bool HasSystem<T>()
            where T : ISystem
        {
            foreach (var system in systems)
            {
                if (system is not null)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class SystemService(IEnumerable<ISceneSystem> systems) : SystemService<ISceneSystem>(systems), ISystemService
    {
    }
}