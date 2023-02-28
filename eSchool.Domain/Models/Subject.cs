﻿namespace eSchool.Domain.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<GradeSubject>? GradeSubjects { get; set; }
    }
}
