using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        readonly Console console;
        readonly Timer timer;
        readonly bool debug;

        List<Action> actions = new List<Action>();

        readonly Portao portoesHangarBaseParteDeFora;
        readonly Portao portoesHangarBaseParteDeDentro;

        public Program()
        {
            debug = true;
            console = new Console(GridTerminalSystem.GetBlockWithName("console") as IMyTextPanel);
            portoesHangarBaseParteDeFora = new Portao(GridTerminalSystem.GetBlockGroupWithName("Portões Hangar base Parte de fora"));
            portoesHangarBaseParteDeDentro = new Portao(GridTerminalSystem.GetBlockGroupWithName("Portoes Hangar base Parte de dentro"));

            Runtime.UpdateFrequency = UpdateFrequency.Update100;
            timer = new Timer(7);
        }

        public void Save()
        {
        }

        public void Main(string argument, UpdateType updateSource)
        {
            DebugCall(argument, updateSource);

            if (!portoesHangarBaseParteDeDentro.Exist() && !portoesHangarBaseParteDeFora.Exist())
            {
                console.Error("grupo do portao de dentro e de fora não encontrado");
                return;
            }

            if (!portoesHangarBaseParteDeDentro.Exist() || !portoesHangarBaseParteDeFora.Exist())
            {
                string qualPortao = !portoesHangarBaseParteDeDentro.Exist() ? "de dentro" : "de fora";

                console.Error("grupo do portao " + qualPortao + " não encontrado");
                return;
            }

            if (updateSource == UpdateType.Trigger)
            {
                ProcessTrigger(argument);
            }
            else
            {
                ProcessUpdate();
            }
        }

        private void DebugCall(string argument, UpdateType updateSource)
        {
            if (debug && updateSource == UpdateType.Trigger)
            {
                console.Log("Argument: " + argument + " UpdateType: " + updateSource);
            }
        }

        private void ProcessTrigger(string argument)
        {
            if (argument == "Abrir-fora" || argument == "Abrir-dentro")
            {
                timer.Start();

                actions = argument == "Abrir-fora" 
                        ? MontarAberturaPortaoPorFora() 
                        : MontarAberturaPortaoPorDentro();
            }
            else
            {
                console.Error("Argumento inválido, Argumento: " + argument);
            }
        }

        private void ProcessUpdate()
        {
            if (!timer.IsRunning())
            {
                return;
            }

            timer.Tick();

            if (!timer.Finished())
            {
                return;
            }

            Action action = actions[0];

            action();

            actions.RemoveAt(0);

            if (actions.Count == 0)
            {
                timer.Stop();
            }

            timer.Reset();
        }

        private List<Action> MontarAberturaPortaoPorDentro()
        {
            List<Action> actions = new List<Action>();

            if (portoesHangarBaseParteDeFora.IsOpen())
            {
                portoesHangarBaseParteDeFora.Fechar();
                actions.Add(() => portoesHangarBaseParteDeDentro.Abrir());
            }
            else
            {
                portoesHangarBaseParteDeDentro.Abrir();
            }

            actions.Add(() => portoesHangarBaseParteDeDentro.Fechar());
            actions.Add(() => portoesHangarBaseParteDeFora.Abrir());
            actions.Add(() => portoesHangarBaseParteDeFora.Fechar());

            return actions;
        }

        private List<Action> MontarAberturaPortaoPorFora()
        {
            List<Action> actions = new List<Action>();

            if (portoesHangarBaseParteDeDentro.IsOpen())
            {
                portoesHangarBaseParteDeDentro.Fechar();
                actions.Add(() => portoesHangarBaseParteDeFora.Abrir());
            }
            else
            {
                portoesHangarBaseParteDeFora.Abrir();
            }

            actions.Add(() => portoesHangarBaseParteDeFora.Fechar());
            actions.Add(() => portoesHangarBaseParteDeDentro.Abrir());
            actions.Add(() => portoesHangarBaseParteDeDentro.Fechar());

            return actions;
        }
    }
}
