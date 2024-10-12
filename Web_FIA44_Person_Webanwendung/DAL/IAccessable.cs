using Web_FIA44_Person_Webanwendung.ViewModels;

namespace Web_FIA44_Person_Webanwendung.DAL
{
    //Interface IAccessable wird erstellt und die Methoden InsertPerson, GetPersonById, GetAllPersons, UpdatePerson, DeletePerson und GetPersonBySearchIndex werden erstellt
    public interface IAccessable
	{
		int InsertPerson(Person person);

		Person GetPersonById(int PiD);

		List<Person> GetAllPersons();
		bool UpdatePerson(Person person);
		bool DeletePerson(int PiD);
		List<Person> GetPersonBySearchIndex(string searchString);
        
    }
}
