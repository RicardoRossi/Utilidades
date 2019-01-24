using System;

namespace LerBillOfMaterial
{
    internal class SW
    {
        private static SldWorks.SldWorks swApp;

        // Tornar a classe private para não poder ser instanciada.
        private SW()
        {
        }

        internal static SldWorks.SldWorks Get_swApp()
        {
            if (swApp == null)
            {
                swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks.SldWorks;
                swApp.Visible = true;
                return swApp;
            }
            return swApp;
        }

        internal static void Dispose()
        {
            if (swApp != null)
            {
                swApp.ExitApp();
                swApp = null;
            }
        }
    }
}
