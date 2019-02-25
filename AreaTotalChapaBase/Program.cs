using Area;
using SldWorks;
using SwConst;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AreaTotalChapaBase
{
    class Program
    {
        static void Main(string[] args)
        {
            SldWorks.SldWorks swApp;
            ModelDoc2 swModel;
            ModelDocExtension swExt;
            Component2 swComp;
            AssemblyDoc swAssem;
            Feature swFeature;
            CustomPropertyManager swCustProp;

            swApp = SW.Get_swApp();
            swModel = swApp.ActiveDoc;
            swAssem = (AssemblyDoc)swModel;

            var componentes = swAssem.GetComponents(false); // Pega todos os componenetes em todos os níveis.
            var ListaDePecaTipoChapa = new List<Component2>();

            // Testa se cada componente é uma chapa. Se for do tipo chapa vai para uma lista de components.
            foreach (var item in componentes)
            {

                var comp = (Component2)item;
                var suprimido = comp.GetSuppression(); // Se estiver suprimido nem testa se é chapa, vai para o próximo.
                if (suprimido == 0)
                {
                    continue;
                }

                // Traversse feature manager para achar uma feature de chapa.
                swFeature = comp.FirstFeature(); 
                // Executa até que next feature for nula.
                while (swFeature != null)
                {
                    var tipo = swFeature.GetTypeName2();
                    if (tipo.Equals("SheetMetal"))
                    {
                        ListaDePecaTipoChapa.Add(comp); // Lista de componenets que são chapa;.
                        break;
                    }
                    swFeature = swFeature.GetNextFeature();
                }
            }

            int contador = 0;
            double area = 0.0;
            double total = 0.0;
            foreach (var item in ListaDePecaTipoChapa)
            {
                contador++;
                SetUnidadeParaMetro(item); // Altera unidade para metro

                // Traversse para achar e pegar a feature Cut list
                swFeature = item.FirstFeature();
                while (swFeature != null)
                {
                    var tipo = swFeature.GetTypeName2();
                    if (tipo.Equals("CutListFolder"))
                    {
                        string valOutArea;
                        string valorResolvidoDaArea;
                        string nomeDaProp;
                        swCustProp = swFeature.CustomPropertyManager; // Aqui swFeature e a "Cut list" que tem custom property manager.

                        var names = swCustProp.GetNames(); // Pega o nome de todas as props.
                        var listaDeNomes = (string[])names; // Converte um object para array de string.

                        // Como tem peça com o nome da propriedade em português outras em inglês tem que verificas ambas.
                        // Vou passar para o Get2 um ou outro nome.
                        var propEN = listaDeNomes.Contains("Bounding Box Area");
                        var propPT = listaDeNomes.Contains("Área da Caixa delimitadora");

                        if (propEN)
                        {
                            nomeDaProp = "Bounding Box Area";
                        }
                        else if (propPT)
                        {
                            nomeDaProp = "Área da Caixa delimitadora";
                        }
                        else
                        {
                            break;
                        }
                        swCustProp.Get2(nomeDaProp, out valOutArea, out valorResolvidoDaArea);

                        if (valorResolvidoDaArea == "")
                        {
                            Console.WriteLine(item.Name);
                            break;
                        }
                        // Converte o valor resolvido para double.
                        area = Double.Parse(valorResolvidoDaArea, System.Globalization.CultureInfo.InvariantCulture);

                        // Soma a área de todas as peças.
                        total += area; 
                        SetUnidadeParaMilimetro(item);
                    }
                    swFeature = swFeature.GetNextFeature();
                }
                SetUnidadeParaMilimetro(item);
            }
            Console.WriteLine(total);
            Console.WriteLine("Total peças = " + contador);
            Console.ReadKey();
        }

        private static void SetUnidadeParaMetro(Component2 componente)
        {
            ModelDoc2 swModel;
            ModelDocExtension swExt;
            swModel = componente.GetModelDoc2();
            swExt = swModel.Extension;
            var retbool = swExt.SetUserPreferenceInteger((int)swUserPreferenceIntegerValue_e.swUnitSystem,
                (int)swUserPreferenceOption_e.swDetailingNoOptionSpecified, (int)swUnitSystem_e.swUnitSystem_MKS);

            // Set casa decimal para 8 casas.
            retbool = swExt.SetUserPreferenceInteger((int)swUserPreferenceIntegerValue_e.swUnitsLinearDecimalPlaces,
                (int)swUserPreferenceOption_e.swDetailingLinearDimension, 8);
        }

        private static void SetUnidadeParaMilimetro(Component2 componente)
        {
            ModelDoc2 swModel;
            ModelDocExtension swExt;
            swModel = componente.GetModelDoc2();
            swExt = swModel.Extension;
            var retbool = swExt.SetUserPreferenceInteger((int)swUserPreferenceIntegerValue_e.swUnitSystem,
                (int)swUserPreferenceOption_e.swDetailingNoOptionSpecified, (int)swUnitSystem_e.swUnitSystem_MMGS);

            // Set casa decimal para 2 casas.
            retbool = swExt.SetUserPreferenceInteger((int)swUserPreferenceIntegerValue_e.swUnitsLinearDecimalPlaces,
               (int)swUserPreferenceOption_e.swDetailingLinearDimension, 2);
        }
    }
}
