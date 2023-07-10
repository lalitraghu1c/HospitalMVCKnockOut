using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Studentmanagment.Models;
using System.Web.Script.Serialization;
using System.Web.Helpers;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.Text;
using iTextSharp.tool.xml;


namespace Studentmanagment.Controllers
{
    public class StudentinfoesController : Controller
    {
        private studentEntities db = new studentEntities();

        // GET: Studentinfoes
        public JsonResult Index()
        { 
            return Json(db.Studentinfoes.ToList(), JsonRequestBehavior.AllowGet);  
        }

        // GET: Studentinfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentinfo studentinfo = db.Studentinfoes.Find(id);
            if (studentinfo == null)
            {
                return HttpNotFound();
            }
            return View(studentinfo);
        }

        // GET: Studentinfoes/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Studentinfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create([Bind(Include = "Id,Name,City,Address,Department,Gender,Date")] Studentinfo studentinfo)
        {
            if (ModelState.IsValid)
            {
                db.Studentinfoes.Add(studentinfo);
                db.SaveChanges();
                return "Student Created";
            }

            return "Creation Failed";
        }



       
    public FileResult Export(string id)
    {
       
        List<object> customers = (from customer in db.Studentinfoes.ToList()
                                   select new [] {
                                      customer.Name,
                                      customer.City,
                                      customer.Address,
                                      customer.Department,
                                      customer.Gender,
                                      customer.Date
                                 }).ToList<object>();
 
        //Building an HTML string.
        StringBuilder sb = new StringBuilder();
 
        //Table start.
        sb.Append("<table border='1' cellpadding='5' cellspacing='0'  style='border: 1px solid #ccc; font-family: Arial; font-size: 10pt;'>");
 
        //Building the Header row.
        sb.Append("<tr>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Name</th>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>City</th>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Address</th>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Department</th>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Gender</th>");
        sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Date</th>");
        sb.Append("</tr>");
 
        //Building the Data rows.
        for (int i = 0; i < customers.Count; i++)
        {
            string[] customer = (string[])customers[i];
            sb.Append("<tr>");
            for (int j = 0; j < customer.Length; j++)
            {
                //Append data.
                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(customer[j]);
                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
 
        //Table end.
        sb.Append("</table>");

        if (id == "excel")
        {
            return File(Encoding.ASCII.GetBytes(sb.ToString()), "application/vnd.ms-excel", "Grid.xls");

        }

        using (MemoryStream stream = new MemoryStream())
        {
            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();
            return File(stream.ToArray(), "application/pdf", "Grid.pdf");
        }
    }


        // GET: Studentinfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentinfo studentinfo = db.Studentinfoes.Find(id);
            if (studentinfo == null)
            {
                return HttpNotFound();
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var Data = serializer.Serialize(studentinfo);
            ViewData["create"] = "hidden";
            ViewData["Save"] = "button";
            return Json(studentinfo, JsonRequestBehavior.AllowGet);
        }

        // POST: Studentinfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Edit([Bind(Include = "Id,Name,City,Address,Department,Gender,Date")] Studentinfo studentinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentinfo).State = EntityState.Modified;
                db.SaveChanges();
                return "Student Edited";
            }
            return "Edited failed";
        }


        //public ActionResult ()
        //{
            //var  emp = db.Studentinfoes.ToList();
            // WebGrid grid = new WebGrid(source: emp, canPage: false, canSort: false);
            // string gridHtml = grid.GetHtml(
            //        columns: grid.Columns(
            //                 grid.Column("Id", "Id"),
            //                 grid.Column("Name", "Name"),
            //                 grid.Column("City", "City"),
            //                 grid.Column("Address", "Address"),
            //                 grid.Column("Department", "Department"),
            //                  grid.Column("Date", "Date")
            //                )
            //         ).ToString();
            // string exportData = String.Format("{0}{1}", "", gridHtml);
            // var bytes = System.Text.Encoding.UTF8.GetBytes(exportData);
            // using (var input = new MemoryStream(bytes))
            // {
            //     var output = new MemoryStream();
            //     var document = new iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50);
            //     var writer = PdfWriter.GetInstance(document, output);
            //     writer.CloseStream = false;
            //     document.Open();
            //     var xmlWorker = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance();
            //     xmlWorker.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);
            //     document.Close();
            //     output.Position = 0;
            //     return File(output, "application/pdf","EmployeePDF.pdf");
            // }

            //Guid newID = Guid.NewGuid();
            //StringReader sr = new StringReader(Request.Form[hfdContent.UniqueID]);
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //pdfDoc.Close();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=" + newID + "HTML.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Write(pdfDoc);
            //Response.End();

            //DataTable data = new DataTable("Grid");
            //data.Columns.AddRange(new DataColumn[5]
            //    {
            //        new DataColumn("Name"),
            //        new DataColumn("City"),
            //        new DataColumn("Address"),
            //        new DataColumn("Gender"),
            //         new DataColumn("Department"),
            //        new DataColumn("Date"),
            //    });
            //var some = from som in db.Studentinfoes.ToList() select som;
            //foreach (var som in some)
            //{
            //    data.Rows.Add(som.Name, som.City, som.Address, som.Gender, som.Department, som.Date);
            //}
            //using (XLWorkbook wb = new XLWorkbook())
            //{
            //    wb.Worksheets.Add(data);
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        wb.SaveAs(ms);
            //        return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
            //    }
            //}

            //    Document doc = new Document();
            //    doc.Pages.Add();
                    

            //    // Initializes a new instance of the Table
            //    Aspose.Pdf.Table table = new Aspose.Pdf.Table();

            //    // Set column widths of the table
            //    table.ColumnWidths = "40 100 100 100";

            //    // Set the table border color as LightGray
            //    table.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray));

            //    // Set the border for table cells
            //    table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray));

            //    // Import data
            //    table.ImportDataTable(dt, true, 0, 1, 3, 3);

            //    // Add table object to first page of input document
            //    doc.Pages[1].Paragraphs.Add(table);

            //    // Save updated document containing table object
            //    doc.Save("output.pdf");
            //}

            //     var customers =  db.Studentinfoes.ToList();

            ////Building an HTML string.
            //StringBuilder sb = new StringBuilder();

            ////Table start.
            //sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");

            ////Building the Header row.
            //sb.Append("<tr>");
            //sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>CustomerID</th>");
            //sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>ContactName</th>");
            //sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>City</th>");
            //sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Country</th>");
            //sb.Append("</tr>");

            ////Building the Data rows.
            //for (int i = 0; i < customers.Count; i++)
            //{
            //    var customer = customers[i];
            //    sb.Append("<tr>");
            //    for (int j = 0; j < customers.Count; j++)
            //    {
            //        //Append data.
            //        sb.Append("<td style='border: 1px solid #ccc'>");
            //        sb.Append(customers[j]);
            //        sb.Append("</td>");
            //    }
            //    sb.Append("</tr>");
            //}

            ////Table end.
            //sb.Append("</table>");

            //using (MemoryStream stream = new MemoryStream())
            //{
            //    StringReader sr = new StringReader(sb.ToString());
            //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            //    pdfDoc.Open();
            //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //    pdfDoc.Close();
            //    return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            //}

        //}
    

        // GET: Studentinfoes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Studentinfo studentinfo = db.Studentinfoes.Find(id);
        //    if (studentinfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(studentinfo);
        //}

        // POST: Studentinfoes/Delete/5
        [ActionName("Delete")]
        public string Delete(int Id)
        {
            try
            {
            Studentinfo studentinfo = db.Studentinfoes.Find(Id);
            if (studentinfo != null)
            {
            db.Studentinfoes.Remove(studentinfo);
            db.SaveChanges();
            return "Student Deleted";
            }
            return "Delete failed";
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
