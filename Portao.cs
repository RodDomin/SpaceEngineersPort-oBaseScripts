using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using System.Collections.Generic;

namespace IngameScript
{
    class Portao
    {
        IMyBlockGroup portaoBlockGroup;

        public Portao(IMyBlockGroup portaoBlockGroup)
        {
            this.portaoBlockGroup = portaoBlockGroup;
        }

        public void Abrir()
        {
            List<IMyAirtightHangarDoor> portoesList = new List<IMyAirtightHangarDoor>();

            portaoBlockGroup.GetBlocksOfType(portoesList);

            portoesList.ForEach(portao => portao.OpenDoor());
        }

        public void Fechar()
        {
            List<IMyAirtightHangarDoor> portoesList = new List<IMyAirtightHangarDoor>();

            portaoBlockGroup.GetBlocksOfType(portoesList);

            portoesList.ForEach(portao => portao.CloseDoor());
        }

        public bool IsOpen()
        {
            List<IMyAirtightHangarDoor> portoesList = new List<IMyAirtightHangarDoor>();

            portaoBlockGroup.GetBlocksOfType(portoesList);

            return portoesList.Find(portao => portao.GetValueBool("Open")) != null;
        }

        public bool Exist()
        {
            return portaoBlockGroup != null;
        }
    }
}
