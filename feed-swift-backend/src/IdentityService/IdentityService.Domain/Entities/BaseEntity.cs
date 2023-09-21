using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nest;

namespace IdentityService.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public bool IsDeleted { get; private set; }
    public bool IsActive { get; private set; }
    
    [Timestamp]
    public uint RowVersion { get; private set; }

    protected BaseEntity(int id) => Id = id;

    public void SetId(int id) => Id = id;

    public void Delete() => IsDeleted = true;

    public void UnDelete() => IsDeleted = false;
    
}

public abstract class BaseElasticSearchEntity
{
    public int Id { get; private set; }

    public CompletionField Suggest { get; set; }
    
    public string SearchingArea { get; set; }
    
    public double? Score { get; set; }
}