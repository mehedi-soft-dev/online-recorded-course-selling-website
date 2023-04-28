using RecordedCourseSellingApp.DataAccess.Identity.Entities;

namespace RecordedCourseSellingApp.DataAccess.Entities
{
    public class CartItem : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Course Course { get; set; }

        public virtual int Price { get; set; }
    }
}
