using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Reflection;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    public class PreciosExporter: ExcelExporter
    {
        #region Factory Methods

        public PreciosExporter()
			: base() {}

        #endregion

        #region Business Methods

        public void ExportBalance(String empresa, BalanceInfo balance)
        {
            Excel.Workbook theWorkbook = NewWorkbook();

            Excel.Worksheet sheet = (Excel.Worksheet)theWorkbook.ActiveSheet;
            Excel.Range rango = null;

            sheet.Cells[2, 2] = "BALANCE";
            rango = sheet.get_Range("B2", "H2");
            rango.MergeCells = true;

            sheet.Cells[4, 2] = "SOLICITANTE: " + empresa;
            rango = sheet.get_Range("B4", "C4");
            rango.MergeCells = true;

            sheet.Cells[4, 8] = "FECHA: " + balance.Fecha.ToShortDateString();
            rango = sheet.get_Range("H4", "I4");
            rango.MergeCells = true;

            //Formato
            rango = sheet.get_Range("B2", "H4");
            rango.Font.Bold = true;

            int index = 6;
            int colPagos = 2;

            sheet.Cells[index, colPagos] = "PENDIENTE DE PAGO";
            sheet.Cells[index, colPagos + 1] = "FACTURADO";
            sheet.Cells[index, colPagos + 2] = "EFECTOS PTE. VTO.";
            sheet.Cells[index, colPagos + 3] = "ESTIMADO";
            
            //Formato
            rango = sheet.get_Range("B" + index.ToString(), "E" + index.ToString());
            rango.Font.Bold = true;
            
            index = index + 2;

			foreach (PaymentSummary item in balance.PendientesPago)
            {
                sheet.Cells[index, colPagos] = item.Nombre.ToUpper();
                sheet.Cells[index, colPagos + 1] = item.Pendiente + item.EfectosDevueltos;
                sheet.Cells[index, colPagos + 2] = item.EfectosPendientesVto;
                sheet.Cells[index++, colPagos + 3] = item.TotalEstimado;
            }
            index++;
            
            int fila_pago = index;

            sheet.Cells[fila_pago, colPagos] = "TOTAL:";
            
            //Formato
            rango = sheet.get_Range("B" + fila_pago.ToString(), "B" + fila_pago.ToString());
            rango.Font.Bold = true;

            //Formula
            rango = sheet.get_Range("C" + fila_pago.ToString(), "C" + fila_pago.ToString());
            rango.Formula = "=SUMA(C8:C" + (fila_pago - 2).ToString() + ")";

            //Formato
            rango = sheet.get_Range("C8", "C" + fila_pago.ToString());
            rango.NumberFormat = "#.##0,00";
            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            rango.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            //Formula
            rango = sheet.get_Range("D" + fila_pago.ToString(), "D" + fila_pago.ToString());
            rango.Formula = "=SUMA(D8:D" + (fila_pago - 2).ToString() + ")";

            //Formato
            rango = sheet.get_Range("D8", "D" + fila_pago.ToString());
            rango.NumberFormat = "#.##0,00";
            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            rango.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            //Formula
            rango = sheet.get_Range("E" + fila_pago.ToString(), "E" + fila_pago.ToString());
            rango.Formula = "=SUMA(E8:E" + (fila_pago - 2).ToString() + ")";

            //Formato
            rango = sheet.get_Range("E8", "E" + fila_pago.ToString());
            rango.NumberFormat = "#.##0,00";
            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            rango.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            index = 6;

            int colCobros = 7;

            sheet.Cells[index, colCobros] = "PENDIENTE DE COBRO";

            //Formato
            rango = sheet.get_Range("G" + index.ToString(), "G" + index.ToString());
            rango.Font.Bold = true;

            index = index + 2;
            sheet.Cells[index, colCobros] = "REA";
            sheet.Cells[index++, colCobros + 1] = balance.PendienteRea;
            sheet.Cells[index, colCobros] = "STOCK FORRAJES";
            sheet.Cells[index++, colCobros + 1] = balance.StockAlimentacionValorado;
            sheet.Cells[index, colCobros] = "STOCK MAQUINARIA";
            sheet.Cells[index++, colCobros + 1] = balance.StockMaquinariaValorado;
            sheet.Cells[index, colCobros] = "STOCK GANADO";
            sheet.Cells[index++, colCobros + 1] = balance.StockGanadoValorado;
            sheet.Cells[index, colCobros] = "MOLINO EXPEDITO";
            sheet.Cells[index++, colCobros + 1] = 0;
            sheet.Cells[index, colCobros] = "MOLINO Y COMEDEROS";
            sheet.Cells[index++, colCobros + 1] = 0;
            sheet.Cells[index, colCobros] = "OTROS";
            sheet.Cells[index++, colCobros + 1] = 0;
            sheet.Cells[index, colCobros] = "AYUDA GANADO";
            sheet.Cells[index++, colCobros + 1] = 0;
            sheet.Cells[index, colCobros] = "PENDIENTE CLIENTES";
            sheet.Cells[index++, colCobros + 1] = balance.PendienteClientes + balance.EfectosDevueltosClientes;
            sheet.Cells[index, colCobros] = "EFECTOS NEGOCIADOS";
            sheet.Cells[index++, colCobros + 1] = balance.EfectosNegociadosClientes;
            sheet.Cells[index, colCobros] = "EFECTOS PTE. VTO.";
            sheet.Cells[index++, colCobros + 1] = balance.EfectosPendienteVtoClientes;
            index++;

            int fila_cobro = index;

            //Formato
            rango = sheet.get_Range("H8", "H" + fila_cobro.ToString());
            rango.NumberFormat = "#.##0,00";
            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            rango.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            sheet.Cells[fila_cobro, colCobros] = "TOTAL:";

            //Formato
            rango = sheet.get_Range("G" + fila_cobro.ToString(), "G" + fila_cobro.ToString());
            rango.Font.Bold = true;
            //Formula
            rango = sheet.get_Range("H" + fila_cobro.ToString(), "H" + fila_cobro.ToString());
            rango.Formula = "=SUMA(H8:H" + (fila_cobro - 4).ToString() + ")";

            index = 6;

            int colBancos = 10;


            sheet.Cells[index, colBancos] = "BANCOS / CAJA";
            
            //Formato
            rango = sheet.get_Range("J" + index.ToString(), "J" + index.ToString());
            rango.Font.Bold = true;

            index = index + 2;

            sheet.Cells[index, colBancos] = "CAIXA";
            sheet.Cells[index++, colBancos + 1] = 0;
            sheet.Cells[index, colBancos] = "CAJA RURAL";
            sheet.Cells[index++, colBancos +1] = 0;
            sheet.Cells[index, colBancos] = "CUENTA 4020";
            sheet.Cells[index++, colBancos + 1] = 0;
            sheet.Cells[index, colBancos] = "CAJA";
            sheet.Cells[index++, colBancos + 1] = balance.SaldoCaja;
            index++;

            int fila_banco = index;

            sheet.Cells[fila_banco, colBancos] = "TOTAL:";

            //Formato
            rango = sheet.get_Range("J" + fila_banco.ToString(), "J" + fila_banco.ToString());
            rango.Font.Bold = true;
            
            //Formula
            rango = sheet.get_Range("K" + fila_banco.ToString(), "K" + fila_banco.ToString());
            rango.Formula = "=SUMA(K8:K" + (fila_banco - 2).ToString() + ")";

            //Formato
            rango = sheet.get_Range("K8", "K" + fila_banco.ToString());
            rango.NumberFormat = "#.##0,00";
            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            rango.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;            

            //TOTALES
            index = fila_pago + 2;
            long fila_totales = index;

            sheet.Cells[index, 2] = "TOTAL PENDIENTE PAGO (FACTURADO):";
            rango = sheet.get_Range("C" + index.ToString(), "C" + index.ToString());
            rango.Formula = "=-C" + fila_pago.ToString();
            index++;

            sheet.Cells[index, 2] = "TOTAL PENDIENTE PAGO (PTE. VTO.):";
            rango = sheet.get_Range("C" + index.ToString(), "C" + index.ToString());
            rango.Formula = "=-D" + fila_pago.ToString();
            index++;

            sheet.Cells[index, 2] = "TOTAL PENDIENTE PAGO (ESTIMADO):";
            rango = sheet.get_Range("C" + index.ToString(), "C" + index.ToString());
            rango.Formula = "=-E" + fila_pago.ToString();
            index++;

            sheet.Cells[index, 2] = "TOTAL PENDIENTE COBRO:";
            rango = sheet.get_Range("C" + index.ToString(), "C" + index.ToString());
            rango.Formula = "=H" + fila_cobro.ToString();
            index++;

            sheet.Cells[index, 2] = "TOTAL BANCOS / CAJA:";
            rango = sheet.get_Range("C" + index.ToString(), "C" + index.ToString());
            rango.Formula = "=" + "K" + fila_banco.ToString();
            index++;
            index++;

            sheet.Cells[index, 2] = "TOTAL BALANCE:";

            //Formato
            rango = sheet.get_Range("B" + fila_totales.ToString(), "B" + index.ToString());
            rango.Font.Bold = true;
            
            //Formula
            rango = sheet.get_Range("C" + index.ToString(), "C" + index.ToString());
            rango.Formula = "=SUMA(C" + fila_totales.ToString() + ":C" + (index - 2).ToString() + ")";

            //Formato
            rango = sheet.get_Range("C" + fila_totales.ToString(), "C" + index.ToString());
            rango.NumberFormat = "#.##0,00";
            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            rango.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            Close();
        }

        #endregion

        #region Business Methods Informe Precios

        private Excel.Range GetRange(Excel.Worksheet sheet, char start, int index, int count, bool porFilas)
        {
            char c = start.ToString().ToUpper()[0];
            string init, final;
            init = c.ToString() + index.ToString();

            if (!porFilas)
            {
                int aux = count == 0 ? 0 : count - 1;
                c = (char)((int)c + aux);
                final = c.ToString() + index.ToString();
            }
            else
            {
                int aux = index + count;
                final = c.ToString() + aux.ToString();
            }

            return sheet.get_Range(init, final);
        }

        public void ExportInformePrecios(System.Data.DataTable datos, ETipoTitular tipo, string filtro)
        {
            Excel.Workbook theWorkbook = NewWorkbook();
            Excel.Worksheet sheet = (Excel.Worksheet)theWorkbook.ActiveSheet;
            Excel.Range rango = null;
            string nombre = string.Empty;

            sheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
            sheet.Columns.Cells.Font.Name = "Arial";
            sheet.Columns.Cells.Font.Size = 7;
            sheet.Columns.Cells.Font.Bold = false;
            sheet.Columns.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            sheet.Columns.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            sheet.Columns.Cells.ShrinkToFit = false;

            int fila_inicial = 7;
            int cols_per_page = 10;

            int row_index = fila_inicial;
            int col_section = cols_per_page;
            int col_index = 1;            

            sheet.Cells[2, 1] = (tipo == ETipoTitular.Cliente) ? Resources.Labels.PRECIOS_CLIENTES_REPORT : Resources.Labels.PRECIOS_PROVEEDORES_REPORT;
            sheet.Cells[4, 1] = "Filtro: " + filtro;
            sheet.Cells[5, 1] = "Fecha de impresión: " + DateTime.Today.ToShortDateString();

            rango = GetRange(sheet, 'A', 4, 2, true);

            //Dar formato a las cabeceras mediante rangos -> negrita, alineación central, ajustar, sombrado y bordes
            rango = GetRange(sheet, 'A', 2, col_section + 1, false);
            rango.MergeCells = true;
            rango.Font.Bold = true;
            rango.Font.Size = 9;
            rango.Borders.Weight = Excel.XlBorderWeight.xlThin;

            string[] maxWords = new string[0];
            string maxWord = string.Empty;

            for (int i = 1; i < datos.Columns.Count; i++)
            {
                string cName = datos.Columns[i].ColumnName.Replace("\n", " ");
                string[] words = cName.Split(Convert.ToChar(" "));
                
                foreach (string word in words)
                    if (maxWord.Length < word.Length)
                        maxWord = word;
                
                if (words.Length > maxWords.Length)
                    maxWords = words;
            }

            //Numero de páginas en función de las columnas
            int resto;
            int pages = Math.DivRem(datos.Columns.Count - 1, cols_per_page, out resto);
            if (resto > 0) pages++;

            for (int page = 0; page < pages; page++)
            {
                try
                {
                    //Correccion de numero de columnas para la ultima pagina
                    if (col_index + col_section > datos.Columns.Count)
                        col_section = (datos.Columns.Count - col_index);

                    //Dar formato a las cabeceras mediante rangos -> alineación central, ajustar, sombreado y bordes
                    rango = GetRange(sheet, 'A', row_index++, col_section + 1, false);

                    int h = 10;
                    rango.RowHeight = h * maxWords.Length;
                    sheet.Columns.Cells.ShrinkToFit = true;
                    rango.WrapText = true;
                    rango.Borders.Weight = Excel.XlBorderWeight.xlThin;

                    //Cabeceras
                    string[] cabeceras = new string[col_section + 1];
                    double[] precios_productos = new double[col_section];

                    cabeceras[0] = (tipo == ETipoTitular.Cliente) ? "Cliente" : "Proveedor";

                    //Obtenemos los nombres de los productos y los precios de referencia
                    for (int i = 0; i < col_section; i++)
                    {
                        string cName = datos.Columns[col_index + i].ColumnName;
                        cabeceras[i + 1] = cName;
                        int pos_ini = cName.IndexOf("\nPP:") + 5;
                        int pos_fin = cName.IndexOf("\nPM:", pos_ini);
                        precios_productos[i] = Convert.ToDouble(cName.Substring(pos_ini, (pos_fin - pos_ini) + 1));
                    }

                    rango.Value2 = cabeceras;

                    //Rellenamos los datos
                    foreach (DataRow row in datos.Rows)
                    {
                        //Columna de titulares
                        string[] sTextos = new string[1];
                        sTextos[0] = row[0].ToString();

                        rango = GetRange(sheet, 'A', row_index, 1, false);
                        rango.ColumnWidth = 25;
                        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                        rango.Value2 = sTextos;

                        //Columnas de precios
                        double[] valores = new double[col_section];
                        for (int i = 0; i < col_section; i++)
                        {
                            try
                            {
                                double d = Double.Parse(row[col_index + i].ToString());
                                valores[i] = d;
                            }
                            catch {}
                        }

                        rango = GetRange(sheet, 'B', row_index, col_section, false);
                        rango.ColumnWidth = 9;

                        rango.Value2 = valores;

                        //Formateo de columnas de precios
                        for (int i = 0; i < col_section; i++)
                        {
                            if (row[col_index + i].ToString().Equals("-"))
                            {
                                sheet.Cells[row_index, i+2] = "--";
                                continue;
                            }

                            rango = GetRange(sheet, (char)((int)'B' + i), row_index, 1, false);

                            // Formateamos el color de la celda en función de su relación con el precio de referencia
                            if (precios_productos[i] > valores[i])
                            {
                                rango.Interior.Color = (tipo == ETipoTitular.Cliente) ? ColorTranslator.ToOle(Color.Red) : ColorTranslator.ToOle(Color.LightGreen);
                            }
                            else if (precios_productos[i] < valores[i])
                            {
                                rango.Interior.Color = (tipo == ETipoTitular.Cliente) ? ColorTranslator.ToOle(Color.LightGreen) : ColorTranslator.ToOle(Color.Red);
                            }
                            else
                            {
                                rango.Interior.Color = ColorTranslator.ToOle(Color.White);
                            }
                        }

                        row_index++;
                    }
                    
                    row_index += 2;
                    col_index += col_section;
                }
                catch (Exception ex)
                {
                    iQExceptionHandler.TreatException(ex);
                }
            }
        }

        #endregion

    }
}
