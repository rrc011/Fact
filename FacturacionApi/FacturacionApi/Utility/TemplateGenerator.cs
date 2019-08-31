using System.Collections.Generic;
using System.Text;
using FacturacionApi.Dto;

namespace FacturacionApi.Utility
{
    public class TemplateGenerator
    {
        public static string GetOrderHTMLString(List<OrderDto> model)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>¡Este es el informe PDF generado!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Factura Nº</th>
                                        <th>Cliente</th>
                                        <th>Fecha</th>
                                        <th>Total</th>
                                    </tr>");

            foreach (var o in model)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", o.OrderId, o.PersonName, o.Date, o.Total);
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
