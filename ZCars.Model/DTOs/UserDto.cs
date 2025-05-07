using ZhooCars.Common;
public class UserDetailDto
{
    #region Properties

    public string? Area { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactPhone { get; set; }

    public string FirstName { get; set; } = null!;

    public string? Gender { get; set; }

    public string? Language { get; set; }

    public string LastName { get; set; } = null!;

    public string? PostalCode { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string? State { get; set; }

    public string? Street1 { get; set; }

    public string? Street2 { get; set; }

    public string? Timezone { get; set; }

    public DateTime? UpdatedAt { get; set; }

    #endregion
}

public class UsersDto
{
    #region Properties

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string FirstName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? LastLogin { get; set; }

    public string LastName { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }

    #endregion
}
