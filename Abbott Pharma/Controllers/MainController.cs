using Abbott_Pharma.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace Abbott_Pharma.Controllers
{
    public class MainController : Controller
    {
        private readonly DBModel context;
        private readonly IWebHostEnvironment env;

        public MainController(DBModel context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public IActionResult AdminAddTb()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminAddTb(Tablet t, IFormFile ImageName)
        {
            var filename = $"{Guid.NewGuid()}_{ImageName.FileName}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TabletImages", filename);

            using (FileStream str = new FileStream(path, FileMode.Create))
            {
                ImageName.CopyTo(str);
            }
            t.Image = filename;
            context.Tablets.Add(t);
            TempData["Success"] = "Product Uploaded successfully!";
            context.SaveChanges();

            return RedirectToAction("Admin_tablet");
        }

        public IActionResult AdminUpdateTb(int id)
        {
            var tablet = context.Tablets.Find(id);

            return View(tablet);
        }

        [HttpPost]
        public IActionResult AdminUpdateTb(Tablet tb, IFormFile ImageName)
        {
            if (ImageName != null) 
            {
                var filename = $"{Guid.NewGuid()}_{ImageName.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TabletImages", filename);

                using (FileStream str = new FileStream(path, FileMode.Create))
                {
                    ImageName.CopyTo(str);
                }

                tb.Image = filename; 
            }
            else
            {
                var existingTablet = context.Tablets.AsNoTracking().FirstOrDefault(t => t.Id == tb.Id);
                if (existingTablet != null)
                {
                    tb.Image = existingTablet.Image;
                }
            }

            context.Tablets.Update(tb);
            TempData["Update"] = "Product Updated successfully!";
            context.SaveChanges();

            return RedirectToAction("Admin_Tablet");
        }

        public IActionResult AdminAddCp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminAddCp(Capsule t, IFormFile ImageName)
        {
            var filename = $"{Guid.NewGuid()}_{ImageName.FileName}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CapsuleImages", filename);

            using (FileStream str = new FileStream(path, FileMode.Create))
            {
                ImageName.CopyTo(str);
            }
            t.Image = filename;
            context.Capsules.Add(t);
            TempData["Success"] = "Product Uploaded successfully!";
            context.SaveChanges();

            return RedirectToAction("Admin_Capsules");
        }

        public IActionResult AdminUpdateCp(int id)
        {
            var capsule = context.Capsules.Find(id);

            return View(capsule);
        }

        [HttpPost]
        public IActionResult AdminUpdateCp(Capsule tb, IFormFile ImageName)
        {
            if (ImageName != null) 
            {
                var filename = $"{Guid.NewGuid()}_{ImageName.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CapsuleImages", filename);

                using (FileStream str = new FileStream(path, FileMode.Create))
                {
                    ImageName.CopyTo(str);
                }

                tb.Image = filename; 
            }
            else
            {
                var existingTablet = context.Capsules.AsNoTracking().FirstOrDefault(t => t.Id == tb.Id);
                if (existingTablet != null)
                {
                    tb.Image = existingTablet.Image;
                }
            }

            context.Capsules.Update(tb);
            TempData["Update"] = "Product Updated successfully!";
            context.SaveChanges();

            return RedirectToAction("Admin_Capsules");
        }


        public IActionResult AdminAddLq()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminAddLq(Liquid t, IFormFile ImageName)
        {
            var filename = $"{Guid.NewGuid()}_{ImageName.FileName}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/LiquidImages", filename);

            using (FileStream str = new FileStream(path, FileMode.Create))
            {
                ImageName.CopyTo(str);
            }
            t.Image = filename;
            context.Liquids.Add(t);
            TempData["Success"] = "Product Uploaded successfully!";
            context.SaveChanges();

            return RedirectToAction("Admin_Liquid");
        }

        public IActionResult AdminUpdateLq(int id)
        {
            var Liquid = context.Liquids.Find(id);

            return View(Liquid);
        }

        [HttpPost]
        public IActionResult AdminUpdateLq(Liquid tb, IFormFile ImageName)
        {
            if (ImageName != null)  
            {
                var filename = $"{Guid.NewGuid()}_{ImageName.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/LiquidImages", filename);

                using (FileStream str = new FileStream(path, FileMode.Create))
                {
                    ImageName.CopyTo(str);
                }

                tb.Image = filename; 
            }
            else
            {
                var existingTablet = context.Liquids.AsNoTracking().FirstOrDefault(t => t.Id == tb.Id);
                if (existingTablet != null)
                {
                    tb.Image = existingTablet.Image;
                }
            }

            context.Liquids.Update(tb);
            TempData["Update"] = "Product Updated successfully!";
            context.SaveChanges();

            return RedirectToAction("Admin_Liquid");
        }

        public IActionResult Tablets()
        {
            var tdata = context.Tablets.ToList(); 
            return View(tdata);
        }


        public IActionResult Home()
        {
           return View();
        }
        
        public IActionResult About()
        {
            return View();
        }
        
        public IActionResult Capsules()
        {
            var cdata = context.Capsules.ToList();
            return View(cdata);
        }
        
        public IActionResult Liquid()
        {
            var ldata = context.Liquids.ToList();
            return View(ldata);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitForm(Contact model)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", model); 
            }

            try
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(model.Email);
                }
                catch
                {
                    TempData["ErrorMessage"] = "Please enter a valid email address!";
                    return RedirectToAction("Contact");
                }

                // SMTP Configuration
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("abbottpharma971@gmail.com", "tmkn xwsv fyak velv"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(model.Email), 
                    Subject = model.Subject,
                    Body = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Message}",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("abbottpharma971@gmail.com"); 

                smtpClient.Send(mailMessage);

                TempData["SuccessMessage"] = "Form submitted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error sending email: " + ex.Message;
            }

            return RedirectToAction("Contact");
        }

        public IActionResult Career()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Career(Career s, IFormFile resume)
        {
            string userEmail = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(userEmail))
            {
                TempData["ErrorMessage"] = "Please login to continue.";
                return RedirectToAction("Career");
            }

            if (resume == null || resume.Length == 0)
            {
                TempData["ErrorMessage"] = "Resume is required.";
                return RedirectToAction("Career");
            }

            ModelState.Remove("ResumePath"); 
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill all the fields.";
                return RedirectToAction("Career");
            }

            if (context.Careers.Any(c => c.Email == userEmail))
            {
                TempData["ErrorMessage"] = "You have already applied for a job.";
                return RedirectToAction("Career");
            }

            var filename = $"{Guid.NewGuid()}_{Path.GetFileName(resume.FileName)}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarrerDocs", filename);

            using (FileStream str = new FileStream(path, FileMode.Create))
            {
                resume.CopyTo(str);
            }

            s.Email = userEmail;
            s.ResumePath = filename;

            context.Add(s);
            context.SaveChanges();

            TempData["SuccessMessage"] = "Your application has been submitted successfully!";
            return RedirectToAction("Career");
        }

        public IActionResult Quote(int? productId, string category)
        {
            Quote quote = new Quote();

            if (productId.HasValue && !string.IsNullOrEmpty(category))
            {
                if (category == "Tablet")
                {
                    var product = context.Tablets.FirstOrDefault(t => t.Id == productId.Value);
                    if (product != null)
                    {
                        quote.ModelName = product.ModelNumber;
                    }
                }
                else if (category == "Liquid")
                {
                    var product = context.Liquids.FirstOrDefault(l => l.Id == productId.Value);
                    if (product != null)
                    {
                        quote.ModelName = product.Name;
                    }
                }
                else if (category == "Capsule")
                {
                    var product = context.Capsules.FirstOrDefault(c => c.Id == productId.Value);
                    if (product != null)
                    {
                        quote.ModelName = product.Name;
                    }
                }
            }

            return View(quote);
        }

        [HttpPost]
        public IActionResult Quote(Quote quote)
        {
            if (!ModelState.IsValid)
            {
                return View(quote);
            }

            context.Quotes.Add(quote);
            context.SaveChanges(); 

            TempData["SuccessMessage"] = "Your request has been submitted successfully!";
            return RedirectToAction("Quote");
        }



        public IActionResult Admin()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role == "Admin")
            {
                var quotes = context.Quotes.ToList(); 
                return View(quotes);
            }

            return RedirectToAction("Login");
        }


        public IActionResult Admin_Job()
        {
            var application = context.Careers.ToList(); 
            return View(application);
        }
        
        public IActionResult Admin_Capsules()
        {
            var data = context.Capsules.ToList();
            return View(data);
        }
        public IActionResult Admin_Tablet()
        {
            var data = context.Tablets.ToList();
            return View(data);
        }
        public IActionResult Admin_Liquid()
        {
            var data = context.Liquids.ToList();
            return View(data);
        }
        
        public IActionResult Admin_Users()
        {
            var users = context.Users.ToList(); 
            return View(users); 
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                
                var existingUser = context.Users.FirstOrDefault(u => u.Email == user.Email || u.FullName == user.FullName);

                if (existingUser != null)
                {
                    
                    TempData["Error"] = "This name or email is already registered!";
                    return View(user);
                }

                
                context.Users.Add(user);
                context.SaveChanges();


                TempData["Success"] = "Registration successful!";
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            var userEmail = HttpContext.Session.GetString("Email");
            if (!string.IsNullOrEmpty(userEmail))
            {
                var role = HttpContext.Session.GetString("Role");
                if (role == "Admin")
                {
                    return RedirectToAction("Admin");
                }
                return RedirectToAction("Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(User users)
        {

          var existingUser = context.Users
         .FirstOrDefault(u => u.Email == users.Email && u.Password == users.Password);

            if (existingUser != null)
            {
              
                HttpContext.Session.SetString("Email", existingUser.Email);
                HttpContext.Session.SetString("FullName", existingUser.FullName);

                if (existingUser.Email == "admin@gmail.com" && existingUser.Password == "admin123")
                {
                    HttpContext.Session.SetString("Role", "Admin");
                    return RedirectToAction("Admin");  
                }

                HttpContext.Session.SetString("Role", "User");
            TempData["Success"] = "Login Successfull";
                return RedirectToAction("Home");
            }

            TempData["Error"] = "Invalid email or password!";
            return View(users);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return RedirectToAction("Login"); 
        }

        public IActionResult DeleteUser(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                TempData["Success"] = "User deleted successfully!";
            }
            return RedirectToAction("Admin_Users");
        }
        
        public IActionResult DeleteQuote(int id)
        {
            var quote = context.Quotes.FirstOrDefault(u => u.Id == id);
            if (quote != null)
            {
                context.Quotes.Remove(quote);
                context.SaveChanges();
                TempData["SuccessQuote"] = "Quote deleted successfully!";
            }
            return RedirectToAction("Admin");
        }


        public IActionResult DeleteCareer(int id)
        {
            var career = context.Careers.FirstOrDefault(u => u.Id == id);
            if (career != null)
            {
                context.Careers.Remove(career);
                context.SaveChanges();
                TempData["Success"] = "Application deleted successfully!";
            }
            return RedirectToAction("Admin_job");
        }

            public IActionResult deleteTablet(int id)
        {
            var tablet = context.Tablets.FirstOrDefault(u => u.Id == id);
            if (tablet != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TabletImages", tablet.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                context.Tablets.Remove(tablet);
                context.SaveChanges();
                TempData["Del"] = "Product deleted successfully!";
            }
            return RedirectToAction("Admin_Tablet");
        }
        
        public IActionResult deleteCapsule(int id)
        {
            var capsule = context.Capsules.FirstOrDefault(u => u.Id == id);
            if (capsule != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CapsuleImages", capsule.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                context.Capsules.Remove(capsule);
                context.SaveChanges();
                TempData["Del"] = "Product Delete successfully!";
            }
            return RedirectToAction("Admin_Capsules");
        }
        public IActionResult deleteLiquid(int id)
        {
            var liquid = context.Liquids.FirstOrDefault(u => u.Id == id);
            if (liquid != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/LiquidImages", liquid.Image);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                context.Liquids.Remove(liquid);
                context.SaveChanges();
                TempData["Del"] = "Product Deleted successfully!";
            }
            return RedirectToAction("Admin_Liquid");
        }


        public IActionResult SendEmail(int id)
        {
            var quote = context.Quotes.FirstOrDefault(q => q.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            string email = quote.Email;
            string subject = "Regarding Your Quote";
            string body = $"Dear {quote.FullName}, we are following up on your quote.";

            string mailtoLink = $"mailto:{email}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";

            return Redirect(mailtoLink);
        }
        

        public IActionResult SendJobEmail(int id)
        {
            var job = context.Careers.FirstOrDefault(q => q.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            string email = job.Email;
            string subject = "Regarding Your Job Request";
            string body = $"Dear {job.FullName}, we are following up on your request.";

            string mailtoLink = $"mailto:{email}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";

            return Redirect(mailtoLink);
        }

    }
}
