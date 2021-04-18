using DayTide.Models;
using DayTide.Repositories;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayTide.Controllers
{
    public class CheckoutController : Controller
    {
        OrderRequestRepository orderReqRepository = new OrderRequestRepository();
        CartRepository cartRepository = new CartRepository();

        CartBackupRepository cartBackupRepository = new CartBackupRepository();
        

        public ActionResult Index()
        {
            //orderReqRepository.Insert(orderRequest);

            return View();
        }

        [HttpPost]
        public ActionResult Confirm(OrderRequest orderRequest)
        {
            orderReqRepository.Insert(orderRequest);

            //cartRepository.

            string cid = Session["userid"].ToString();
            var mycart =    cartRepository.GetCartById(cid);

            int orderID = orderRequest.OrderId;

            //List<CartBackup> backupList = new List<CartBackup>();

            

            foreach(var item in mycart)
            {
                CartBackup backup = new CartBackup();
                backup.OrderId = orderID;
                backup.CustomerId = cid;
                backup.ProductId = item.ProductId;
                backup.Quantiry = item.Quantiry;
                backup.Price = Convert.ToDouble(item.Price_unit_);
                cartBackupRepository.Insert(backup);

                backup = null;
            }


            cartRepository.DeleteCustomerCart(cid);

                

            return View(orderReqRepository.GetProductById(orderRequest.OrderId));
        }


        public ActionResult GeneratePdf(string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");

            HtmlToPdf ohtmlToPdf = new HtmlToPdf();
            PdfDocument oPdfDocument = ohtmlToPdf.ConvertHtmlString(html);

            byte[] pdf = oPdfDocument.Save();
            oPdfDocument.Close();


            return File(
                pdf,
                "application/pdf",
                "Invoice.pdf"
                );

        }

        ///Please Edit web.config and add ---->
              // <system.webServer>
              //  <security>
              //    <requestFiltering>
              //      <requestLimits maxQueryString = "32768" />
              //    </requestFiltering >
              //  </security >
              //</system.webServer >



    }
}