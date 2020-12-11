namespace HomeWork1.Models.EntityRule
{
    public interface IIsDelete
    {
         bool IsDeleted { set; get; }
    }


    public interface IDateModified
    {
        System.DateTime DateModified { set; get; }
    }
}