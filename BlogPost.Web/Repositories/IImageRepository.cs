namespace BlogPost.Web.Repositories
{
    public interface IImageRepository
    {
        Task<string> UplaodAsync(IFormFile file);
    }
}
