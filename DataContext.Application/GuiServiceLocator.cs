namespace DataContext.Gui
{
    using DataContext.Core;

    public class GuiServiceLocator : AutofacServiceLocator
    {
        protected override void ApplySpecialBindings()
        {
            // Real context = 0
            var currentContext = new CurrentContext(0); // or read settings / registry...

            RebindToConstant(currentContext);
        }
    }
}
