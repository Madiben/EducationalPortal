using EducationalPortal.DataAccess.Enums;

namespace EducationalPortal.DataAccess.Models.Entities
{
    public class Article : Material
    {
        public string Source { get; set; }

        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }

        public Article() : base(MaterialType.Article) { }


        public Article(int id, string name, string description, string source, string author, DateTime publicationDate, ICollection<Course> courses = null)
        : base(MaterialType.Article)
        {
            MaterialId = id;
            Name = name;
            Description = description;
            Source = source;
            Author = author;
            PublicationDate = publicationDate;
            Courses = courses ?? new List<Course>();
        }

        public override string ToString()
        {
            return $"{base.ToString()} : Source: {Source}, Author: {Author}, Publication Date: {PublicationDate.ToShortDateString()}";
        }
    }
}


