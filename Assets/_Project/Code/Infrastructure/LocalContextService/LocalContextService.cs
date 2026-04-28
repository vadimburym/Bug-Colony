using System;
using Zenject;

namespace _Project.Code.Infrastructure
{
    public sealed class LocalContextService : ILocalContextService
    {
        public DiContainer Context => _context;
        private DiContainer _context;

        private readonly DiContainer _projectContext;
        
        public LocalContextService(DiContainer projectContext)
        {
            _projectContext = projectContext;
        }

        public void CleanUp() => _context = null;

        public void WarmUp(Action<DiContainer> installation)
        {
            _context = new(_projectContext);
            installation.Invoke(_context);
        }
        
        public void Inject(object obj) => _context.Inject(obj);
    }
}