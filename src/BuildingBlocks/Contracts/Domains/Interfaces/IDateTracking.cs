namespace Contracts.Domains.Interfaces
{
    public interface IDateTracking
    {
        DateTimeOffset CreatedDate{get;set;}
        DateTimeOffset? lastModifiedDate{get;set;}
    }
}