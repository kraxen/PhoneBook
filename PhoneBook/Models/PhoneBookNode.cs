using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.Models
{
    public class PhoneBookNode
    {
        public int Id { get; set; }
        public string Suname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
    }
}
