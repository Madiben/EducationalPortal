using EducationalPortal.DataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPortal.DataAccess.Models.Entities
{
    public class Book : Material
    {
        [NotMapped]
        public List<string> Authors { get; set; }

        public int Pages { get; set; }

        public string Format { get; set; }

        public int YearOfPublication { get; set; }

        public Book() : base(MaterialType.Book) { }

        public Book(int id, string name, string description, List<string> authors, int pages, string format, int yearOfPublication)
        : base(MaterialType.Book)
        {
            MaterialId = id;
            Name = name;
            Description = description;
            Authors = authors;
            Pages = pages;
            Format = format;
            YearOfPublication = yearOfPublication;
        }

        public Book(int id, string name, string description, List<string> authors, int pages, string format, int yearOfPublication, ICollection<Course> courses)
        : base(MaterialType.Book)
        {
            MaterialId = id;
            Name = name;
            Description = description;
            Authors = authors;
            Pages = pages;
            Format = format;
            YearOfPublication = yearOfPublication;
            Courses = courses;
        }

        public override string ToString()
        {
            var authorsString = string.Join(", ", Authors);
            return $"{base.ToString()} : Authors: [{authorsString}], Page Count: {Pages}, Format: {Format}, Publication Date: {YearOfPublication}";
        }
    }
}
