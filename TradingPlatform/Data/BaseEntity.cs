﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using TradingPlatform.Models;

namespace TradingPlatform.Data
{
    /// <summary>
    /// Base entity
    /// </summary>
    public abstract partial class BaseEntity
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      //  [LDisplayName("displayName", "BaseEntityCreated", "Час створення", "Время создания")]
        [Column(TypeName ="datetime2")]
        public DateTime Created { get; set; }
       // [LDisplayName("displayName", "BaseEntityUpdated", "Час оновлення", "Время обновления")]
        [Column(TypeName = "datetime2")]
        public DateTime Updated { get; set; }

        public string CreatedByUserId { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set;}
        public string UpdatedByUserId { get; set; }
        public virtual ApplicationUser UpdatedByUser { get; set; }
        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        private static bool IsTransient(BaseEntity obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }
    }
}