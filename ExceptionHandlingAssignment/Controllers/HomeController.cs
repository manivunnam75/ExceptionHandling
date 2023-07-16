
using ExceptionHandlingAssignment.Interfaces;
using ExceptionHandlingAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace ExceptionHandlingAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileProcessing fileProcessing;
        private readonly IConfiguration configuration;
        private readonly IExceptionRepo exceptionRepo;

        public HomeController(ILogger<HomeController> logger, IFileProcessing _fileProcessing, IConfiguration _configuration, IExceptionRepo _exceptionRepo)
        {
            _logger = logger;
            fileProcessing = _fileProcessing;
            configuration = _configuration;
            exceptionRepo = _exceptionRepo;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormFile excelfile, bool isEditable)
        {

            string response = "SuccessFully file uploaded";
            if (excelfile != null && excelfile.Length > 0)
            {
                try
                {
                    if (!isEditable)
                    {
                        if (fileProcessing.IsExcelFile(excelfile))
                        {

                            string tempFilepath = Path.GetTempPath();
                            string dateTime = DateTime.Now.ToString();
                            string filePath = Path.Combine(tempFilepath, dateTime);
                            try
                            {
                                if (!fileProcessing.IsFileFound(filePath))
                                {
                                    throw new FileNotFoundException();
                                }
                            }
                            catch (FileNotFoundException ex)
                            {
                                response = ex.Message;
                            }

                        }
                        else
                        {
                            throw new FileFormatException();
                        }
                    }
                    else
                    {
                        try
                        {
                            if (!fileProcessing.InsufficientFilePermissions(excelfile))
                            {
                                throw new UnauthorizedAccessException();

                            }
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            response = ex.Message;
                        }

                    }
                }
                catch (FileFormatException ex)
                {
                    response = ex.Message;
                }

            }
            else
            {
                response = "please upload the excel file (xlsx)";
            }
            ViewBag.message = response;
            return View();

        }
        [HttpGet]
        public IActionResult Database()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Database(string Option)
        {
            string response = string.Empty;


            if (Option == Constants.ImproperSql)
            {
                try
                {
                    if (!exceptionRepo.CheckSqlSyntax())
                    {
                        throw new Exception();
                    }


                }
                catch (Exception ex)
                {
                    response = "invalid Sql Query" + " " + ex.Message;
                }
            }
            else
            {
                try
                {
                    if (!exceptionRepo.CheckSqlConnection())
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    response = "Connection failed";
                }
              
            }
            ViewBag.Message = response;
            return View();
        }
        [HttpGet]
        public IActionResult API()
        {
            return View();
        }
        [HttpPost]
        public IActionResult API(bool isCheck)
        {
            string message = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1);
                    HttpResponseMessage response = client.GetAsync("https://api.publicapis.org/entries").Result;


                }
                return View();
            }

            catch (Exception ex)
            {
                message = "Connection Timeout " + ex.Message;
            }
            ViewBag.Message = message;
            return View();
        }
        public IActionResult RegistrationForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegistrationForm(UserDetails userDetails)
        {
            if (!ModelState.IsValid)
            {

                return View(userDetails);
            }
            return View(userDetails);
        }

    }
}





