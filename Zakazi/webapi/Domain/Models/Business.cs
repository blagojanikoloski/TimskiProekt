using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace webapi.Domain.Models
{
    public class Business
    {
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public int OwnerId { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ZakaziUser> Employees { get; set; } = new Collection<ZakaziUser>();
        public virtual ICollection<Post> Posts { get; set; } = new Collection<Post>();
        public virtual ICollection<Request> Requests { get; set; } = new Collection<Request>();
    }
}
