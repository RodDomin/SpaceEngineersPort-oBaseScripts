using System;
using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
    class Console
    {
        IMyTextPanel lcd;

        public Console(IMyTextPanel panel)
        {
            lcd = panel;
        }

        private void Write(string text, string type)
        {
            string time = string.Format("{0:dd/MM - hh:mm:ss}", DateTime.Now);

            lcd.WriteText($"{time} | [{type}] {text}\n", true);
        }

        public void Log(string text)
        {
            Write(text, "Log");
        }

        public void Clear()
        {
            lcd.WriteText("");
        }

        public void Error(string text)
        {
            Write(text, "Error");
        }

        public void Info(string text)
        {
            Write(text, "Info");
        }
    }
}
