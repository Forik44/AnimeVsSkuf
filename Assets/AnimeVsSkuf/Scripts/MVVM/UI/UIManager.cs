using DI;

namespace MVVM.UI
{
    public abstract class UIManager
    {
        protected readonly DIContainer Container; // чтобы вытаскивать барахло, чтобы собирать вьюмодели окошек

        protected UIManager(DIContainer container)
        {
            Container = container;
        }
    }
}