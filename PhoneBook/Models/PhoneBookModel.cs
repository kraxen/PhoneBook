using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PhoneBook.Models
{
    public interface IPhoneBookModel
    {
        public IList<PhoneBookNode> Nodes { get; set; }
        public void Add(PhoneBookNode node);
        public void Remove(PhoneBookNode node);
        public void Change(PhoneBookNode newNode);
    }
    public class DefaultPhoneBookModel : IPhoneBookModel
    {
        public DefaultPhoneBookModel()
        {
            Nodes = new List<PhoneBookNode>();
            Nodes.Add(GetRandom(1));
            Nodes.Add(GetRandom(2));
            Nodes.Add(GetRandom(3));
            LastId = 3;
        }

        public IList<PhoneBookNode> Nodes { get; set; }
        public int LastId { get; set; }

        static PhoneBookNode GetRandom(int id)
        {
            string[] names = { "Павел", "Иван", "Денис" };
            string[] surnames = { "Иванов", "Петров", "Сидоров", };
            string[] patronymics = { "Дмитриевич", "Иванович", "Евгеньевич" };
            string[] phones = { "+79995486325", "+75624553685", "+79999999955" };
            string[] adresses = { "г. Москва, ул Кировская, д 15, кв 10", "г. Москва, ул Кировская, д 15, кв 15", "г. Москва, ул Жуковская, д 13, кв 10" };
            string description = "Какой-то текст";

            return new()
            {
                Id = id,
                Name = names[id % 3],
                Suname = surnames[id % 3],
                Patronymic = patronymics[id % 3],
                PhoneNumber = phones[id % 3],
                Adress = adresses[id % 3],
                Description = description
            };
        }

        public void Add(PhoneBookNode node)
        {
            LastId++;
            node.Id = LastId;
            Nodes.Add(node);
        }

        public void Change(PhoneBookNode node)
        {
            var old = Nodes.FirstOrDefault(n => n.Id == node.Id);
            Nodes.Remove(old);
            Nodes.Add(node);
        }

        public void Remove(PhoneBookNode node)
        {
            Nodes.Remove(node);
        }
    }
    public class DbPhoneBookModel : DbContext, IPhoneBookModel
    {
        public DbPhoneBookModel(DbContextOptions<DbPhoneBookModel> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<PhoneBookNode> NodesDb { get; set; }
        public IList<PhoneBookNode> Nodes { get => NodesDb.ToList(); set { NodesDb = (DbSet<PhoneBookNode>)value; } }

        public void Add(PhoneBookNode node)
        {
            NodesDb.Add(node);
            SaveChanges();
        }

        public void Change(PhoneBookNode newNode)
        {
            var old = NodesDb.Update(newNode);
            SaveChanges();
        }

        public void Remove(PhoneBookNode node)
        {
            NodesDb.Remove(node);
            SaveChanges();
        }
    }
}
