using WebDisassembler.DataStorage.Models.Identity;

namespace WebDisassembler.DataStorage.Models.Projects;

public class ProjectMember
{    
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
