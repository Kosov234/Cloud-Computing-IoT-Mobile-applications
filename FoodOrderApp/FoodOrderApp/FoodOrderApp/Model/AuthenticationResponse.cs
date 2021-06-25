using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderApp.Model
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }

        public bool Result { get; set; }

        public string Errors { get; set; }
    }
}
