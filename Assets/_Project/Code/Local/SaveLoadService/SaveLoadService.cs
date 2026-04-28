using System.Collections.Generic;
using _Project.Code.Core.Abstractions;
using _Project.Code.Infrastructure;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Code.Local
{
    public sealed class SaveLoadService : ISaveLoadService
    {
        private readonly IReadOnlyList<ISaveLoader> _saveLoaders;
        private readonly ISaveRepository _saveRepository;
        
        public SaveLoadService(
            [InjectOptional] List<ISaveLoader> saveLoaders,
            ISaveRepository saveRepository)
        {
            _saveLoaders = saveLoaders;
            _saveRepository = saveRepository;
        }
        
        public void Save()
        {
            for (int i = 0; i < _saveLoaders.Count; i++)
                _saveLoaders[i].SaveData();
            _saveRepository.Save().Forget();
        }

        public void Load()
        {
            for (int i = 0; i < _saveLoaders.Count; i++)
                _saveLoaders[i].LoadData();
        }
    }
}