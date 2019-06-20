using RedCorners.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.ContactsDemo
{
    public class App : Application2
    {
        public override Page GetFirstPage() 
            => new MainPage();
    }
}
