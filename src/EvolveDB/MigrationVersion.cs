using EvolveDB.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EvolveDB
{
    public sealed class MigrationVersion : IComparable<MigrationVersion>, IComparable, IEquatable<MigrationVersion>
    {
        #region Champs
        public static readonly MigrationVersion MaxVersion = new MigrationVersion(long.MaxValue.ToString());
        public static readonly MigrationVersion MinVersion = new MigrationVersion("0");
        private const string VersionPattern = @"\.(?=\d)";
        private static readonly Regex _parser = new Regex(VersionPattern);
        #endregion

        #region Constructeurs
        public MigrationVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentNullException(nameof(version));
            }
            NormalizedVersion = version.Replace("_", ".");
            string[] array = _parser.Split(NormalizedVersion);
            Parts = array.Select(long.Parse).ToArray();
        }
        #endregion

        #region Propriétés
        public string NormalizedVersion
        {
            get;
        }
        public IList<long> Parts
        {
            get;
        }
        #endregion

        #region Méthodes
        public static bool operator !=(MigrationVersion v1, MigrationVersion v2)
        {
            return !(v1 == v2);
        }
        public static bool operator <(MigrationVersion v1, MigrationVersion v2)
        {
            if((object)v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }
            return (v1.CompareTo(v2) < 0);
        }

        public static bool operator <=(MigrationVersion v1, MigrationVersion v2)
        {
            if((object)v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }
            return (v1.CompareTo(v2) <= 0);
        }

        public static bool operator ==(MigrationVersion v1, MigrationVersion v2)
        {
            if (v2 is null)
            {
                return (v1 is null);
            }

            return ReferenceEquals(v2, v1) || v2.Equals(v1);
        }
        public static bool operator >(MigrationVersion v1, MigrationVersion v2)
        {
            if((object)v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }
            return (v1.CompareTo(v2) > 0);
        }

        public static bool operator >=(MigrationVersion v1, MigrationVersion v2)
        {
            if((object)v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }
            return (v1.CompareTo(v2) >= 0);
        }
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            Version other = obj as Version;
            if (other == null)
            {
                throw new ArgumentException(Strings.Arg_MustBeVersion);
            }
            return CompareTo(other);
        }

        public int CompareTo(MigrationVersion other)
        {
            if (object.ReferenceEquals(other, this))
            {
                return 0;
            }
            else if (other is null)
            {
                return 1;
            }

            using (IEnumerator<long> e1 = Parts.GetEnumerator())
            using (IEnumerator<long> e2 = other.Parts.GetEnumerator())
            {
                while (e1.MoveNext())
                {
                    if (!e2.MoveNext())
                    {
                        return 1;
                    }
                    int comparison = e1.Current.CompareTo(e2.Current);
                    if (comparison == 0)
                    {
                        continue;
                    }
                    return comparison;
                }
                return e2.MoveNext() ? -1 : 0;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MigrationVersion);
        }

        public bool Equals(MigrationVersion other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return NormalizedVersion.GetHashCode();
        }

        public override string ToString() => NormalizedVersion;
        #endregion
    }
}