using Microsoft.AspNetCore.Mvc;
using Repo;
using PDFReader;
using MVCPhoneServiceWeb.Utils;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MVCPhoneServiceWeb.Controllers
{
    public class InvoiceStorerController: Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHostingEnvironment _hostingEnviroment;
        public InvoiceStorerController(ApplicationDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnviroment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewData["statusMessage"] = "";
            return View();
        }
        [HttpPost, ActionName("Index")]
        public  IActionResult IndexPost()
        {
            string webRootPath = _hostingEnviroment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var uploads = Path.Combine(webRootPath, "Facturas");
            var path = "";
            if (files.Count > 0)
            {

                //files has been uploaded
                var extension = Path.GetExtension(files[0].FileName);
                path = Path.Combine(uploads, files[0].FileName);
                using (var filesStream = new FileStream(path, FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                var invoice = Parser.ReadPdfFile(path);
                var storer = new InvoiceStorer.InvoiceStorer(_dbContext);
                try
                {
                    storer.Store(invoice);
                    ViewData["statusMessage"] = "Bill Uploaded Successfuly";
                }
                catch (System.Exception)
                {
                    ViewData["statusMessage"] = "Error, Bill was already uploaded";
                }
                //return RedirectToAction("Index");
                return View();

            }
            else
            {
                ViewBag["statusMessage"] = "Error, Select a File";
                return View();
            }
        } 
    }
}