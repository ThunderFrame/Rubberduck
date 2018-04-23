using Interop.SldWorks.Types;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;

// ReSharper disable once CheckNamespace - Special dispensation due to conflicting file vs namespace priorities
namespace Rubberduck.VBEditor.SafeComWrappers.VBA
{
    public class SolidWorksApp : HostApplicationBase<Interop.SldWorks.Extensibility.Application>
    {
        public SolidWorksApp() : base("SolidWorks") { }
        public SolidWorksApp(IVBE vbe) : base(vbe, "SolidWorks") { }

        public override void Run(dynamic declaration)
        {
            var qualifiedMemberName = declaration.QualifiedName;
            var projectFileName = qualifiedMemberName.QualifiedModuleName.ProjectPath;
            if (Application == null || string.IsNullOrEmpty(projectFileName))
            {
                return;
            }

            var moduleName = qualifiedMemberName.QualifiedModuleName.ComponentName;
            var memberName = qualifiedMemberName.MemberName;

            var runner = (SldWorks)Application.SldWorks;
            runner.RunMacro(projectFileName, moduleName, memberName);
        }
    }
}