using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace kin4.notsynched
{
    internal class Main
    {
        private readonly Dispatcher dispatcher;

        public Main(Dispatcher dis)
        {
            this.dispatcher = dis;
        }

        public void Main1(TextBox outBox)
        {
            Dorothy dorothy = new Dorothy();
            var threads = new List<Thread>();
            var cancellationSource = new CancellationTokenSource();

            for (int i = 0; i < 100; i++)
            {
                AddThread(threads, dorothy, "Tin Man", CharacterColor.Silver);
                AddThread(threads, dorothy, "Scarecrow", CharacterColor.Brown);
                AddThread(threads, dorothy, "Cowardly Lion", CharacterColor.Yellow);
            }


            var monitor = new Thread(() => MonitorState(dorothy, outBox, cancellationSource.Token));
            monitor.Start();

            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());

            cancellationSource.Cancel();
            monitor.Join();
        }

        private void AddThread(List<Thread> threads, Dorothy d, string character, CharacterColor color)
        {
            var worker = new myThread(d, character, color);
            threads.Add(new Thread(worker.ThreadProc));
        }

        private void MonitorState(Dorothy dorothy, TextBox outBox, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var state = dorothy.GetFavorite();
                dispatcher.Invoke(() =>
                {
                    if ((state.Contains("Tin Man") && !state.Contains("Silver")) ||
                        (state.Contains("Scarecrow") && !state.Contains("Brown")) ||
                        (state.Contains("Cowardly Lion") && !state.Contains("Yellow")))
                    {
                        outBox.AppendText($"\nCORRUPTED: {state}");
                    }
                    else
                    {
                        outBox.AppendText($"\nCurrent: {state}");
                    }
                });
                Thread.Sleep(1);
            }
        }
    }
}