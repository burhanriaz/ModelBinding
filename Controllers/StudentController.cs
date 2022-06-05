using Microsoft.AspNetCore.Mvc;
using ModelBinding.Data;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    //[Route("api/[Controller]")]
    //public class StudentController : ControllerBase
    //{

    // I have test with broswer and Postman 
    public class StudentController : Controller
    {
        private readonly AppDbContext _DbContext;

        public StudentController(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        //FromFrom
        //FromBody
        //FromQuery
        //FromHeader
        //FromRoute
        public IActionResult Index()
        {
            var model = _DbContext.StudentInfo.ToList();
            return Ok(model);
        }
        // Read data by using parameter in Method Argumet Id will pass from url as a query string
        //  https://localhost:44395/Student/ReadData?id=7
        [HttpPost]
        public async Task<IActionResult> ReadData(int id)
        {
            var model = await _DbContext.StudentInfo.FindAsync(id);
            if (model == null)
                return NotFound("Not Found");
            return Ok(model);
        }
        //Query string Create And Update both Operation apply with
        //Parameter Query string And FromQuery Attribute
        //https://localhost:44395/Student/CreateWithParameterQuerryString?id=7&Name=Jn&Email=burhanriaz@gmail.com&Contact=03203567535
        // Querry string data pass fro url then save in model Create and edit both apply
        [HttpPost]
        public async Task<IActionResult> CreateWithParameterQuerryString(int? Id, string Name, string Email, string Contact)
        {
            if (ModelState.IsValid)
            {
                var student = new StudentInfo();
                student.Id = Id;
                student.Name = Name;
                student.Email = Email;
                student.Contact = Contact;
                if (student.Id == null)
                {
                    await _DbContext.StudentInfo.AddAsync(student);
                }
                else
                {
                    var model = await _DbContext.StudentInfo.FindAsync(Id);
                    if (model == null)
                        return NotFound();
                    model.Name = Name;
                    model.Email = Email;
                    model.Contact = Contact;
                    _DbContext.StudentInfo.Update(model);
                }
                await _DbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");

        }

        //https://localhost:44395/Student/CreateWithQuerryString?id=7&Name=Jn&Email=burhanriaz@gmail.com&Contact=03203567535
        // Querry string  by using [FromQuery] Attribute data pass from url then save in model Create and edit both apply
        [HttpPost]
        public async Task<IActionResult> CreateWithQuerryString([FromQuery] StudentInfo student)
        {

            if (ModelState.IsValid)
            {
                if (student.Id == null)
                {
                    await _DbContext.StudentInfo.AddAsync(student);

                }
                else
                {
                    var model = await _DbContext.StudentInfo.FindAsync(student.Id);
                    if (model == null)
                        return NotFound("Not Found");
                    model.Name = student.Name;
                    model.Email = student.Email;
                    model.Contact = student.Contact;
                    _DbContext.StudentInfo.Update(model);

                }
                await _DbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // return View(student);
            return RedirectToAction("Index");

        }


        //  https://localhost:44395/Student/DeleteWithQuerryString?id=7
        // delete with [FromQuery] Attribute using 
        [HttpPost]
        public async Task<IActionResult> DeleteWithQuerryString([FromQuery] StudentInfo student)
        {
            if (student.Id != null)
            {
                var model = await _DbContext.StudentInfo.FindAsync(student.Id);
                if (model == null)
                    return NotFound("Not Found");
                _DbContext.StudentInfo.Remove(model);
                await _DbContext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            // return View(student);
            return RedirectToAction("Index");
        }

        //https://localhost:44395/Student/DeleteWithParameterQuerryString?id=7
        // delete with using parameter in Mentod Argument
        [HttpPost]
        public async Task<IActionResult> DeleteWithParameterQuerryString(int Id)
        {
            if (Id != null)
            {
                var model = await _DbContext.StudentInfo.FindAsync(Id);
                if (model == null)
                    return NotFound("Not Found");
                _DbContext.StudentInfo.Remove(model);
                await _DbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // return View(student);
            return RedirectToAction("Index");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [Route("Student/Edit")]
        public async Task<IActionResult> Create(int? id)
        {
            if (id is null)
                return NotFound("Not Found");
            var student = await _DbContext.StudentInfo.FindAsync(id);
            return View(student);
        }

        //https://localhost:44395/Student/Create
        // Querry string  by using [FromForm] Attribute data pass data from Form then save in model Create and edit both apply


        [HttpPost]
        [Route("Student/Create")]
        public async Task<IActionResult> Createpost([FromForm] StudentInfo student)
        {
            if (ModelState.IsValid)
            {
                if (student.Id == null)
                {
                    await _DbContext.StudentInfo.AddAsync(student);
                }
                else
                {
                    var model = await _DbContext.StudentInfo.FindAsync(student.Id);
                    if (model == null)
                        return NotFound("Not Found");
                    model.Name = student.Name;
                    model.Email = student.Email;
                    model.Contact = student.Contact;
                    _DbContext.StudentInfo.Update(model);

                }
                await _DbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create", student);

        }

        [HttpGet]
        [Route("Student/Delete")]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null)
                return NotFound("Not Found");
            var student = await _DbContext.StudentInfo.FindAsync(Id);
            return View(student);
        }


        [HttpPost]
        [Route("Student/Delete")]
        public async Task<IActionResult> Deletepost([FromForm] int? id)
        {
            if (id is null)
                return NotFound("Not Found");
            var model = await _DbContext.StudentInfo.FindAsync(id);
            if (model == null)
                return NotFound("Not Found");
            _DbContext.StudentInfo.Remove(model);
            await _DbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // Create with [FromBody] Attribute
        //https://localhost:44395/Student/CreateWithFrombody
        // This link post in postman Url
        // I have test this by using postman
        // we  have to pass data from postman in body option 
        // Select the text format as json And send jason data

        //{"name":"Burhan",
        //"Email":burhanriaz35@gmail.com,
        //"Contact":"03204567890"
        //}
        [HttpPost]
        public async Task<IActionResult> CreateWithFrombody([FromBody] StudentInfo student)
        {

            if (ModelState.IsValid)
            {
                if (student.Id == null)
                {
                    await _DbContext.StudentInfo.AddAsync(student);
                }
                else
                {
                    var model = await _DbContext.StudentInfo.FindAsync(student.Id);
                    if (model == null)
                        return NotFound("Id does Not Found");
                    model.Name = student.Name;
                    model.Email = student.Email;
                    model.Contact = student.Contact;
                    _DbContext.StudentInfo.Update(model);

                }
                await _DbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create", student);

        }

        // Create with [FromHeader] Attribute
        // This link post in postman Url I have test this by using postman
        //https://localhost:44395/Student/CreateWithFromHeader
        // we  have to pass data from postman Header in option 
        // Name : Burhan
        // Email: burhan35@gmaoil.com
        // Contact : 03204547568
        [HttpPost]
        public async Task<IActionResult> CreateWithFromHeader([FromHeader] int? Id, [FromHeader] string Name, [FromHeader] string Email, [FromHeader] string Contact)
        {
            var student = new StudentInfo();
            student.Id = Id;
            student.Name = Name;
            student.Email = Email;
            student.Contact = Contact;

            if (ModelState.IsValid)
            {
                if (student.Id == null)
                {
                    await _DbContext.StudentInfo.AddAsync(student);
                }
                else
                {
                    var model = await _DbContext.StudentInfo.FindAsync(student.Id);
                    if (model == null)
                        return NotFound("Id does Not Found");
                    model.Name = student.Name;
                    model.Email = student.Email;
                    model.Contact = student.Contact;
                    _DbContext.StudentInfo.Update(model);

                }
                await _DbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create", student);

        }

        // Create with [FromRoute] Attribute
        // This link post in postman Url I have test this by using postman
        //https://localhost:44395/api/Student/CreateWithFromRoute/45/Burhan/burhan35@gmail.com/03203456678
        [HttpPost]
        [Route("CreateWithFromRoute/{Id?}/{Name}/{Email}/{Contact}")]
        public async Task<IActionResult> CreateWithFromRoute([FromRoute] StudentInfo student)
        {
            if (ModelState.IsValid)
            {
                if (student.Id == null)
                {
                    await _DbContext.StudentInfo.AddAsync(student);
                }
                else
                {
                    var model = await _DbContext.StudentInfo.FindAsync(student.Id);
                    if (model == null)
                        return NotFound("Id does Not Found");
                    model.Name = student.Name;
                    model.Email = student.Email;
                    model.Contact = student.Contact;
                    _DbContext.StudentInfo.Update(model);

                }
                await _DbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");

        }

    }
}
