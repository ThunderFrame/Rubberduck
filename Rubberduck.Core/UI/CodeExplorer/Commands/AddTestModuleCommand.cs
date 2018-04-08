using System.Runtime.InteropServices;
using NLog;
using Rubberduck.Navigation.CodeExplorer;
using Rubberduck.Parsing.Symbols;
using Rubberduck.UI.Command;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;

namespace Rubberduck.UI.CodeExplorer.Commands
{
    [CodeExplorerCommand]
    public class AddTestModuleCommand : CommandBase
    {
        private readonly IVBE _vbe;
        private readonly Command.AddTestModuleCommand _newUnitTestModuleCommand;

        public AddTestModuleCommand(IVBE vbe, Command.AddTestModuleCommand newUnitTestModuleCommand) : base(LogManager.GetCurrentClassLogger())
        {
            _vbe = vbe;
            _newUnitTestModuleCommand = newUnitTestModuleCommand;
        }

        protected override bool EvaluateCanExecute(object parameter)
        {
            try
            {
                return GetDeclaration(parameter)?.Project != null || _vbe.ProjectsCount == 1;
            }
            catch (COMException)
            {
                return false;
            }
        }

        protected override void OnExecute(object parameter)
        {
            var parameterProject = GetDeclaration(parameter)?.Project;
            if (parameter != null && parameterProject == null)
            {
                return; //The project selected module is not available.
            }

            _newUnitTestModuleCommand.Execute(parameter != null
                ? parameterProject
                : _vbe.ActiveVBProject);
        }

        private Declaration GetDeclaration(object parameter)
        {
            var node = parameter as CodeExplorerItemViewModel;
            while (node != null && !(node is ICodeExplorerDeclarationViewModel))
            {
                node = node.Parent;
            }

            return ((ICodeExplorerDeclarationViewModel) node)?.Declaration;
        }
    }
}