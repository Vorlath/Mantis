using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;

namespace Mantis.Engine.Services
{
    public class SystemService(IEnumerable<ISystem> systems) : ISystemService
    {
        private readonly List<ISystem> _systems = systems.ToList();

        public IEnumerable<ISystem> GetAll()
        {
            return this._systems;
        }

        public ISystem GetSystem<TSystem>() where TSystem : ISystem
        {
            foreach (var system in systems)
            {
                if (system is TSystem requestedSystem)
                {
                    return requestedSystem;
                }
            }
            throw new KeyNotFoundException(nameof(TSystem));
        }

        public IEnumerable<TSystem> GetSystems<TSystem>()
        {
            return this._systems.OfType<TSystem>();
        }

        public bool HasSystem<TSystem>() where TSystem : ISystem
        {
            foreach (var system in systems)
            {
                if (system is TSystem)
                {
                    return true;
                }
            }
            return false;
        }
    }
}