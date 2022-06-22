using Microsoft.AspNetCore.Identity;

namespace AuthConsole;

public class UserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IUserEmailStore<IdentityUser> _emailStore;

    public UserService(UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _userStore = userStore;
        _signInManager = signInManager;
        _emailStore = (IUserEmailStore<IdentityUser>) _userStore;
    }

    public async Task<bool> CreateUser(string email, string password)
    {
        var user = Activator.CreateInstance<IdentityUser>();
        await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }
}