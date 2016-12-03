﻿using PasswordManager.Entities;
using PasswordManager.Globals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PasswordManager.Filer
{
    public static class Filer
    {
        public static bool Save(List<User> Users)
        {
            string FilePath = Globals.Settings.DatabaseFilePath;

            foreach (User user in Users)
            {
                foreach (Password password in user.Passwords)
                {
                    //get all stuff & write xml
                }
            }

            return false;
        }

        public static List<User> Read()
        {
            //return type should be a list of users with their password details
            string FilePath = Globals.Settings.DatabaseFilePath;

            List<User> Users = new List<User>();

            User newUser = null;

            // Create an XML reader for this file.
            using (XmlReader xReader = XmlReader.Create(FilePath))
            {
                while (xReader.Read())
                {
                    // Only detect start elements.
                    if (xReader.IsStartElement())
                    {
                        switch (xReader.Name)
                        {
                            case "User":
                                if (newUser == null)
                                    newUser = new User();

                                XmlReader userreader = xReader.ReadSubtree();

                                while (userreader.Read())
                                {
                                    switch (userreader.Name)
                                    {
                                        case "Name":
                                            if (userreader.Read())
                                            {
                                                string Name = userreader.Value.Trim();
                                                if (Verifier.Text(Name))
                                                    newUser.Name = Name;
                                            }
                                            break;
                                        case "Username":
                                            if (userreader.Read())
                                            {
                                                string Username = userreader.Value.Trim();
                                                if (Verifier.Text(Username))
                                                    newUser.Username = Username;
                                            }
                                            break;
                                        case "Email":
                                            if (userreader.Read())
                                            {
                                                string Email = userreader.Value.Trim();
                                                if (Verifier.Text(Email)) // well i am not validating email here for now. Although i can
                                                    newUser.Email = Email;
                                            }
                                            break;
                                        case "Login":
                                            if (userreader.Read())
                                            {
                                                string Login = userreader.Value.Trim();
                                                if (Verifier.Text(Login))
                                                    newUser.LoginPassword = Login;
                                            }
                                            break;
                                        case "Passwords":
                                            List<Password> PasswordsList = new List<Password>();

                                            XmlReader PasswordsReader = userreader.ReadSubtree();

                                            Password newPassword = null;

                                            while (PasswordsReader.Read())
                                            {
                                                switch (PasswordsReader.Name)
                                                {
                                                    case "Password":
                                                        if (newPassword == null)
                                                            newPassword = new Password();
                                                        else
                                                        {
                                                            PasswordsList.Add(newPassword);
                                                            newPassword = null;
                                                        }
                                                        break;

                                                    case "ID":
                                                        if (PasswordsReader.Read())
                                                        {
                                                            string ID = PasswordsReader.Value.Trim();
                                                            if (Verifier.Text(ID))
                                                                newPassword.ID = Convert.ToInt32(ID);
                                                        }
                                                        break;
                                                    case "Name":
                                                        if (PasswordsReader.Read())
                                                        {
                                                            string Name = PasswordsReader.Value.Trim();
                                                            if (Verifier.Text(Name))
                                                                newPassword.Name = Name;
                                                        }
                                                        break;
                                                    case "Email":
                                                        if (PasswordsReader.Read())
                                                        {
                                                            string Email = PasswordsReader.Value.Trim();
                                                            if (Verifier.Text(Email))
                                                                newPassword.Email = Email;
                                                        }
                                                        break;
                                                    case "Username":
                                                        if (PasswordsReader.Read())
                                                        {
                                                            string Username = PasswordsReader.Value.Trim();
                                                            if (Verifier.Text(Username))
                                                                newPassword.Username = Username;
                                                        }
                                                        break;
                                                    case "Text":
                                                        if (PasswordsReader.Read())
                                                        {
                                                            string Text = PasswordsReader.Value.Trim();
                                                            if (Verifier.Text(Text))
                                                                newPassword.Text = Text;
                                                        }
                                                        break;
                                                    case "DateCreated":
                                                        if (PasswordsReader.Read())
                                                        {
                                                            string DateCreated = PasswordsReader.Value.Trim();
                                                            if (Verifier.Text(DateCreated))
                                                                newPassword.DateCreated = Convert.ToDateTime(DateCreated);
                                                        }
                                                        break;
                                                    case "DateModified":
                                                        if (PasswordsReader.Read())
                                                        {
                                                            string DateModified = PasswordsReader.Value.Trim();
                                                            if (Verifier.Text(DateModified))
                                                                newPassword.DateModified = Convert.ToDateTime(DateModified);
                                                        }
                                                        break;
                                                }
                                            }
                                            newUser.Passwords = PasswordsList;
                                            break;
                                    }
                                }
                                Users.Add(newUser);
                                newUser = null;
                                break;
                        }
                    }
                }
                return Users;
            }
        }
    }
}
