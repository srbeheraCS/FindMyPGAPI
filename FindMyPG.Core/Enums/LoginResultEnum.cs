using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Enums
{
    public enum LoginResultEnum
    {
        Successful=1,
        UserNotExist,
        WrongPassword,
        LockedOut,
        PhoneNumberNotConfirmed,
        EmailNotConfirmed
    }
}
