using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {

        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }

        public void CleanUp();
        public GameObject CreateHero(Vector3 at);
        public GameObject CreateHUD();
        void Register(ISavedProgressReader progressReader);

        GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
    }
}