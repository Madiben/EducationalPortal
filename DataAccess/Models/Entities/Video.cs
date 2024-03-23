using System;
using EducationalPortal.DataAccess.Enums;

namespace EducationalPortal.DataAccess.Models.Entities
{
    public class Video : Material
    {
        public int DurationInMinutes { get; set; }

        public string Quality { get; set; }

        public Video() : base(MaterialType.Video) { }

        public Video(int id, string name, string description, int durationInMinutes, string quality)
        : base(MaterialType.Video)
        {
            MaterialId = id;
            Name = name;
            Description = description;
            DurationInMinutes = durationInMinutes;
            Quality = quality;
        }

        public Video(int id, string name, string description, int durationInMinutes, string quality, ICollection<Course> courses)
        : base(MaterialType.Video)
        {
            MaterialId = id;
            Name = name;
            Description = description;
            DurationInMinutes = durationInMinutes;
            Quality = quality;
            Courses = courses;
        }

        public override string ToString()
        {
            return $"{base.ToString()} : Duration: {DurationInMinutes} Min, Quality: {Quality}";
        }
    }
}
