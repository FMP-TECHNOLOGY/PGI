using Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PasswordChangeDto
    {
        [Required(AllowEmptyStrings = false)]
        public required string UserId { get; set; }

        /// <summary>
        /// User Old password (required on change password)
        /// </summary>
        public string? OldPassword { get; set; }

        /// <summary>
        /// New User Password
        /// </summary>
        [Required(AllowEmptyStrings = false), MinLength(AppConstants.PASSWORD_MIN_LENGTH)]
        [DataType(DataType.Password)]
        public required string NewPassword { get; set; }
    }
}
