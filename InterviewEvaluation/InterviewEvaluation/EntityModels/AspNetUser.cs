//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterviewEvaluation.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class AspNetUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int AccessFailedCount { get; set; }
        public string BioText { get; set; }
        public string FullName { get; set; }
        public string ImgName { get; set; }
        public byte[] ImgData { get; set; }
    
        public virtual AspNetRole AspNetRoles { get; set; }
    }
}