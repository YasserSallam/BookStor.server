using Microsoft.AspNetCore.Http;

namespace BookStore.Infrastructure.Services.Contracts
{
  public  interface IFileService
    {
        string Save(IFormFile file);
    }
}
