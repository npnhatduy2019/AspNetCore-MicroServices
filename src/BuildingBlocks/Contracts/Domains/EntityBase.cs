using Contracts.Domains.Interfaces;

namespace Contracts.Domains
{
    public abstract class EntityBase<Tkey>: IEntityBase<Tkey>
    {
        public Tkey Id {get;set;}
    }
}