using System.Windows;
using CryptoInfo.Infrastructure.Commands.Base;

namespace CryptoInfo.Infrastructure.Commands
{
    internal class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
