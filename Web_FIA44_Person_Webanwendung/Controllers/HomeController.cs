using Microsoft.AspNetCore.Mvc;
using Web_FIA44_Person_Webanwendung.DAL;
using Web_FIA44_Person_Webanwendung.ViewModels;

namespace Web_FIA44_Person_Webanwendung.Controllers
{
    public class HomeController : Controller
    {
        #region DAL-Connection
        //DAL-Connection
        private readonly IAccessable dal;
        public HomeController(IConfiguration conf)
        {
            //der connectionString wird aus der appsettings.json Datei gelesen und in die Variable connectionString geschrieben
            //"ConnectionStrings": {
            // "SqlServer": "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=DalDemo;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=True"
            //die ist der server der Datenbank, der Name der Datenbank, die Art der Authentifizierung und ob die Datenbank verschlüsselt ist
            string connString = conf.GetConnectionString("SqlServer");
            dal = new SqlDal(connString);

        }
        #endregion
        #region Alle Personen anzeigen oder nach Personen filtern
        //TODO: Implmentieren einer Methode um Personen nach Vor- und Nachname zu filtern
        
        [HttpGet]
        public IActionResult Index(string searchString)
        {
            //Neue Liste von Personen erstellen
            List<Person> AllPersons = new List<Person>();
            //solange der Suchstring nicht leer ist, wird die Liste AllPersons mit den Personen gefüllt, die den Suchstring enthalten
            if (!string.IsNullOrEmpty(searchString))
            {
                AllPersons = dal.GetPersonBySearchIndex(searchString);
                return View(AllPersons);
            }
            //wenn der Suchstring leer ist, werden alle Personen angezeigt
            else
            {
                AllPersons = dal.GetAllPersons();
                
            }
            return View(AllPersons);
        }

        #endregion
        #region Person hinzufügen
        [HttpGet]
        //Methode um eine Person hinzuzufügen
        //wenn die Methode aufgerufen wird, wird die View Create aufgerufen also das leere Formular
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public IActionResult Create(Person person)
        {
            //wenn die Eingaben nicht korrekt sind, wird die View Create aufgerufen
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            //wenn die Eingaben korrekt sind, wird die Person in die Datenbank eingefügt und die Index-View aufgerufen
            dal.InsertPerson(person);
            return RedirectToAction("Index");
        }
        #endregion
        #region Person bearbeiten
        [HttpGet]
        //Methode um eine Person zu bearbeiten
        public IActionResult Update(int Pid)
        {
            //die Person wird anhand der ID aus der Datenbank geholt
            Person person = dal.GetPersonById(Pid);
            return View(person);
        }
        [HttpPost]
        public IActionResult Update(Person person)
        {
            //wenn die Eingaben nicht korrekt sind, wird die View Update aufgerufen
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            //wenn die Eingaben korrekt sind, wird die Person in der Datenbank aktualisiert und die Index-View aufgerufen
            dal.UpdatePerson(person);
            return RedirectToAction("Index");
        }
        #endregion
        #region Person löschen
        public IActionResult Delete(int Pid)
        {
            //die Person wird anhand der ID aus der Datenbank gelöscht
            dal.DeletePerson(Pid);
            return RedirectToAction("Index");
        }
        #endregion
        #region Personen detailiert anzeigen
        [HttpGet]
        public IActionResult Details(int Pid)
        {
            //die Person wird anhand der ID aus der Datenbank geholt
            //und die View Details aufgerufen
            Person person = dal.GetPersonById(Pid);
            return View(person);
        }
        #endregion
    }
      
}

