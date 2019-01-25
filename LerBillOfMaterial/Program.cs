using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwConst;

namespace LerBillOfMaterial
{
    public class Program
    {
        static List<string> listaDeCodigos = new List<string>();

        static void Main(string[] args)
        {
            SldWorks.SldWorks swApp;
            ModelDoc2 swModel;

            swApp = SW.Get_swApp();
            //swApp.SendMsgToUser("teste conectado!");

            IEnumerable<int> codigos = Enumerable.Range(4020001, 256);
            string fullPath;
            try
            {
                foreach (var codigo in codigos)
                {
                    int erro = 0, aviso = 0;
                    fullPath = $@"C:\ELETROFRIO\ENGENHARIA SMR\PRODUTOS FINAIS ELETROFRIO\MECÂNICA\RACK PADRAO\RACK PADRAO TESTE\{codigo}.SLDDRW";
                    //swApp.DocumentVisible(false, (int)swDocumentTypes_e.swDocDRAWING);
                    swModel = swApp.OpenDoc6(fullPath, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_RapidDraft, "", erro, aviso);
                    if (swModel==null)
                    {
                        Console.WriteLine("Erro: " + erro);
                        Console.WriteLine("Aviso: " + aviso);
                        continue;
                    }
                    
                    DrawingDoc swDraw = (DrawingDoc)swModel;
                    View swActiveView;
                    View swSheetView;

                    //A first view é a folha, e não a vista propriamente dita
                    swSheetView = swDraw.GetFirstView();
                    swActiveView = swSheetView.GetNextView();
                    var nomeDaVista = swActiveView.GetName2();

                    //ModelDoc2 swModelDaVista;
                    //swModelDaVista = swActiveView.ReferencedDocument;
                    //var nomeDo3DdaVista = swModelDaVista.GetPathName();

                    //Object[] anotacoesDaVista = swActiveView.GetAnnotations();

                    //foreach (var a in anotacoesDaVista)
                    //{
                    //    Annotation annot = (Annotation)a;
                    //    Console.WriteLine(annot.GetName());
                    //}

                    BomFeature swBomFeature;
                    swBomFeature = GetBOM(swModel);
                    Feature swFeat;
                    swFeat = swBomFeature.GetFeature();
                    object[] vTableArr = swBomFeature.GetTableAnnotations();
                    BomTableAnnotation tableAnn = null;
                    foreach (BomTableAnnotation v in vTableArr)
                    {
                        tableAnn = (BomTableAnnotation)v;
                    }

                    if (tableAnn != null)
                    {
                        LerBOM(tableAnn, swModel);
                    }
                    else
                    {
                        Console.WriteLine("Não pegou a table annotation");
                    }
                    EscreveTXT(listaDeCodigos);
                    swApp.CloseAllDocuments(true);
                    Console.WriteLine(codigo);
                }
            }
            finally
            {
                EscreveTXT(listaDeCodigos);
            }
            Console.ReadLine();
        }

        private static void LerBOM(BomTableAnnotation swBomTable, ModelDoc2 swModel)
        {
            TableAnnotation tabela = (TableAnnotation)swBomTable;
            long coluna = tabela.ColumnCount;
            long linha = tabela.RowCount;

            listaDeCodigos.Add(Path.GetFileNameWithoutExtension(swModel.GetPathName()) + ";");

            for (int i = 0; i < linha - 1; i++)
            {
                listaDeCodigos.Add((tabela.Text[i, 1] + ";" + tabela.Text[i, 5]));
            }
            listaDeCodigos.Add(";");
        }

        private static BomFeature GetBOM(ModelDoc2 swModel)
        {
            BomFeature swBomFeat;
            Feature swFeat = swModel.FirstFeature();

            while (swFeat != null)
            {
                if ("BomFeat" == swFeat.GetTypeName())
                {
                    swBomFeat = swFeat.GetSpecificFeature2();
                    return swBomFeat;
                }
                swFeat = swFeat.GetNextFeature();
            }
            return null;
        }


        static void EscreveTXT(List<string> listaDeCodigos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:\RELATORIO\bom.txt"))
                {
                    foreach (var codigo in listaDeCodigos)
                    {
                        sw.WriteLine(codigo);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
