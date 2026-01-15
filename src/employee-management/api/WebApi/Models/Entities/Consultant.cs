using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities
{
    public class Consultant
    {
        public required Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? EmailAddress { get; set; }

        public string? ImageUrl { get; set; }

        public string? ImageFileName { get; set; }

        public virtual HashSet<ConsultantTask> ConsultantTasks { get; set; }
    }
}
