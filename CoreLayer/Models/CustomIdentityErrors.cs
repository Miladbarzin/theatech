using Microsoft.AspNetCore.Identity;

namespace CoreLayer.Models
{
	public class CustomIdentityErrors : IdentityErrorDescriber
	{
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = $"This email '{email}' registered by another person."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateUserName),
                Description = $"This user name {userName} registered by another person."
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidEmail),
                Description = $"This email '{email}' is invalid!"
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateRoleName),
                Description = $"This role '{role}' Added before"
            };
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidRoleName),
                Description = $"This role name '{role}' is invalid"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresDigit),
                Description = $"Password must has a number character"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresLower),
                Description = $"Password must has minimum one lower character ('a'-'z')"
            };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresUpper),
                Description = $"Password must has minimum one upper character ('A'-'Z')"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = $"Password must has minimum one symbol '@#%^&'"
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = $"Password must has {uniqueChars} symbols"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = $"Password must bigger than {length} character"
            };
        }
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidUserName),
                Description = $"This username '{userName}' is invalid."
            };
        }
        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError()
            {
                Code = nameof(UserNotInRole),
                Description = $"User not in this role '{role}'"
            };
        }
        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError()
            {
                Code = nameof(UserAlreadyInRole),
                Description = $"User is in role '{role}'"
            };
        }
        public override IdentityError PasswordMismatch()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordMismatch),
                Description = $"Password not match!"
            };
        }
        public override IdentityError DefaultError()
        {
            return new IdentityError()
            {
                Code = nameof(DefaultError),
                Description = $"Server Error, Please try later"
            };
        }
    }
}
