using System.ComponentModel.DataAnnotations;

namespace Web_FIA44_Person_Webanwendung.ViewModels
{
	public class Person
	{
		[Display(Name = "Personen ID")]
		public int PiD { get; set; }
        [Display(Name = "Vorname")]
        [Required(ErrorMessage = "Bitte geben Sie einen Vornamen ein")]
        public string Firstname { get; set; }
        [Display(Name = "Nachname")]
        [Required(ErrorMessage = "Bitte geben Sie einen Nachnamen ein")]
        public string Lastname { get; set; }
        [Display(Name = "Geburtsdatum")]
        [Required(ErrorMessage = "Bitte geben Sie ein Geburtsdatum ein")]
        public DateTime Birthday { get; set; }
		[Display(Name = "Geschlecht")]
        [Required(ErrorMessage = "Bitte geben Sie ein Geschlecht ein")]
        public string Gender { get; set; }
        [Display(Name = "Brillenträger?")]
        [Required(ErrorMessage = "Bitte bestimmen Sie, ob Sie Brillen tragen")]
        public bool Glasses { get; set; }
        [Display(Name = "Bemerkung")]
        public string Remark { get; set; }

	}
}
