﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PruebaUsers.Models;

namespace PruebaUsers.ActualModels
{
    public class ActualUser
    {
        public string Id_ActualUser { get; set; }
        public string ActualUserName { get; set; }
        public Boolean IsSysAdmin { get; set; }
        public ICollection<UserRole> ActualUserRoles { get; set; }


        public ActualUser(string UserName)
        {
            using (var _context = new ApplicationDbContext())
            {
                try
                {
                    this.Id_ActualUser = _context.Users.FirstOrDefault(p => p.UserName == UserName).Id;

                    this.ActualUserName = UserName;
                    this.IsSysAdmin = _context.UserDetails.FirstOrDefault(p => p.UserName == this.ActualUserName).isSysAdmin;

                }
                catch (Exception ex)
                {
                    //manejar error
                    throw;
                }

            }

            this.SetUserRoles();

        }


        // 1. Extrae de la base de datos los Roles de un usuario
        // 2. verifica si es Sysadmin o no
        // 3. Almacena en cada rol los permisos que este posee
        public void SetUserRoles()
        {

            using (var _context = new ApplicationDbContext())
            {
                
                List<IdentityUserRole> Roles = _context.Users.FirstOrDefault(p => p.UserName == this.ActualUserName).Roles.ToList();
                foreach (var rol in Roles)
                {
                    UserRole userRole = new UserRole
                    {
                        
                        RoleName = _context.Roles.FirstOrDefault(P => P.Id == rol.RoleId).Name
                    };

                    int a;
                    int.TryParse(rol.RoleId, out a);

                    userRole.Id_UserRole = a;

                    userRole.SetUserRolePermissions();

                    if (IsSysAdmin == false)
                    {
                        this.IsSysAdmin = userRole.IsSysAdmin();
                    }

                    ActualUserRoles.Add(userRole);
                }
            }
        }


        // Verifica que un Usuario tenga un rol especifico
        public Boolean HasRole(string Rol)
        {
            if (this.ActualUserRoles != null)
            {
                foreach (var rol in this.ActualUserRoles)
                {
                    if (rol.RoleName.ToLower() == Rol.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        // Verifica que el usuario tiene alguno de los Roles dados.
        public bool HasRoles(string Roles)
        {
            bool bFound = false;
            string[] _Roles = Roles.ToLower().Split(';');

            if (this.ActualUserRoles != null)
            {
                foreach (UserRole Role in this.ActualUserRoles)
                {
                    try
                    {
                        bFound = _Roles.Contains(Role.RoleName.ToLower());
                        if (bFound)
                            return bFound;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return bFound;
        }

        // Verifica si un Usuario tiene un permiso especifico

        public Boolean HasPermission(string Permission)
        {
            if (this.ActualUserRoles != null)
            {
                foreach (var rol in this.ActualUserRoles)
                {
                    foreach (var RolPermission in rol.UserPermissions)
                    {
                        if (Permission.ToLower() == RolPermission.PermissionDescription.ToLower())
                        {
                            return true;
                        }
                    }


                }
            }

            return false;
        }

    }
}