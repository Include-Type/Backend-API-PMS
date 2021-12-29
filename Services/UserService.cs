namespace IncludeTypeBackend.Services;

public class UserService
{
    private readonly PostgreSqlContext _db;

    public UserService(PostgreSqlContext db) => _db = db;

    public async Task<List<User>> GetAllUsersAsync() => await _db.User.ToListAsync();

    public async Task AddUserAsync(User user)
    {
        ProfessionalProfile proProfile = new()
        {
            UserId = user.Id
        };

        Privacy privacyProfile = new()
        {
            UserId = user.Id
        };

        await _db.User.AddAsync(user);
        await _db.ProfessionalProfile.AddAsync(proProfile);
        await _db.Privacy.AddAsync(privacyProfile);
        await _db.SaveChangesAsync();
    }

    public async Task<User> GetUserByIdAsync(string userId) =>
        await _db.User.FirstOrDefaultAsync(user => user.Id == userId);

    public async Task<User> GetUserAsync(string key) =>
        await _db.User.FirstOrDefaultAsync(user => (user.Id == key || user.Email == key || user.Username == key));

    public async Task<ProfessionalProfile> GetUserProfessionalProfileAsync(string userId) =>
        await _db.ProfessionalProfile.FirstOrDefaultAsync(profile => profile.UserId == userId);

    public async Task<Privacy> GetUserPrivacyProfileAsync(string userId) =>
        await _db.Privacy.FirstOrDefaultAsync(privacy => privacy.UserId == userId);

    public async Task<CompleteUserDto> GetCompleteUserAsync(string key)
    {
        User user = await _db.User.FirstOrDefaultAsync(user => (user.Id == key || user.Email == key || user.Username == key));
        ProfessionalProfile professionalProfile = await GetUserProfessionalProfileAsync(user.Id);
        Privacy privacy = await GetUserPrivacyProfileAsync(user.Id);
        return new CompleteUserDto()
        {
            User = user,
            ProfessionalProfile = professionalProfile,
            Privacy = privacy
        };
    }

    public async Task UpdateUserAsync(User existingUser, User updatedUser)
    {
        existingUser.FirstName = updatedUser.FirstName;
        existingUser.LastName = updatedUser.LastName;
        existingUser.Bio = updatedUser.Bio;
        existingUser.Username = updatedUser.Username;
        existingUser.Email = updatedUser.Email;
        existingUser.Password = updatedUser.Password;
        existingUser.Address = updatedUser.Address;
        existingUser.Country = updatedUser.Country;
        existingUser.City = updatedUser.City;
        existingUser.State = updatedUser.State;
        existingUser.Pincode = updatedUser.Pincode;
        existingUser.Contact = updatedUser.Contact;
        existingUser.Picture = updatedUser.Picture;
        await _db.SaveChangesAsync();
    }

    public async Task UpdateUserProfessionalProfileAsync(ProfessionalProfile existingPro, ProfessionalProfile updatedPro)
    {
        existingPro.Education = updatedPro.Education;
        existingPro.Companies = updatedPro.Companies;
        existingPro.Skills = updatedPro.Skills;
        existingPro.ExperienceYears = updatedPro.ExperienceYears;
        existingPro.ExperienceMonths = updatedPro.ExperienceMonths;
        existingPro.Projects = updatedPro.Projects;
        await _db.SaveChangesAsync();
    }

    public async Task UpdateUserPrivacyProfileAsync(Privacy existingPrivacy, Privacy updatedPrivacy)
    {
        existingPrivacy.Name = updatedPrivacy.Name;
        existingPrivacy.Bio = updatedPrivacy.Bio;
        existingPrivacy.Picture = updatedPrivacy.Picture;
        existingPrivacy.Email = updatedPrivacy.Email;
        existingPrivacy.Contact = updatedPrivacy.Contact;
        existingPrivacy.Address = updatedPrivacy.Address;
        existingPrivacy.Education = updatedPrivacy.Education;
        existingPrivacy.Companies = updatedPrivacy.Companies;
        existingPrivacy.Skills = updatedPrivacy.Skills;
        existingPrivacy.Experience = updatedPrivacy.Experience;
        existingPrivacy.Projects = updatedPrivacy.Projects;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        ProfessionalProfile proProfile = await GetUserProfessionalProfileAsync(user.Id);
        Privacy privacyProfile = await GetUserPrivacyProfileAsync(user.Id);
        _db.User.Remove(user);
        _db.ProfessionalProfile.Remove(proProfile);
        _db.Privacy.Remove(privacyProfile);
        await _db.SaveChangesAsync();
    }

    public async Task<List<UserVerification>> GetAllPendingUserVerificationsAsync() =>
        await _db.UserVerification.ToListAsync();

    public async Task<UserVerification> GetPendingUserVerificationAsync(string userId) =>
        await _db.UserVerification.FirstOrDefaultAsync(verifier => verifier.UserId.Equals(userId));

    public async Task DeletePendingUserVerificationAsync(string userId)
    {
        UserVerification userVerification = await GetPendingUserVerificationAsync(userId);
        if (userVerification is not null)
        {
            _db.UserVerification.Remove(userVerification);
        }
    }

    public async Task<string> AddPendingUserVerificationAsync(string userId)
    {
        await DeletePendingUserVerificationAsync(userId);
        string uniqueString = userId + Convert.ToString(Guid.NewGuid());
        UserVerification userVerification = new()
        {
            UserId = userId,
            UniqueString = HashPassword(uniqueString),
            CreationTime = Convert.ToString(DateTime.Now),
            ExpirationTime = Convert.ToString(DateTime.Now.AddMinutes(15))
        };

        await _db.UserVerification.AddAsync(userVerification);
        await _db.SaveChangesAsync();
        return uniqueString;
    }

    public async Task UpdateUserPasswordAsync(string userId, string newPassword)
    {
        User user = await GetUserAsync(userId);
        if (user is not null)
        {
            user.Password = HashPassword(newPassword);
            await _db.SaveChangesAsync();
        }
    }
}
