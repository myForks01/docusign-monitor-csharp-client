﻿using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using RestSharp;
using System.Text;
using System.Collections.Generic;
using System;

namespace DocuSign.Monitor.Client.Auth
{
    public class OAuth
    {
        //  OAuth Scope constants
        //  create and send envelopes, and obtain links for starting signing sessions.
        public static string Scope_SIGNATURE = "signature";
        //  obtain a refresh token with an extended lifetime.
        public static string Scope_EXTENDED = "extended";
        //  obtain access to the user’s account when the user is not present.
        public static string Scope_IMPERSONATION = "impersonation";

        //  OAuth Base path constants
        //  Demo server base path
        public static string Demo_OAuth_BasePath = "account-d.docusign.com";
        // Production server base path
        public static string Production_OAuth_BasePath = "account.docusign.com";
        // Stage server base path
        public static string Stage_OAuth_BasePath = "account-s.docusign.com";

        //  OAuth ResponseType constants
        //  used by public/native client applications.
        public static string CODE = "code";
        //  used by private/trusted client application.
        public static string TOKEN = "token";

        // OAuth Grant Types
        // JWT Grant Type
        public const string Grant_Type_JWT = "urn:ietf:params:oauth:grant-type:jwt-bearer";

        //    /**
        //	 * 
        //	 * UserInfo model with the following properties:
        //	 * <br><b>sub</b>: the user ID GUID.
        //	 * <br><b>accounts</b>: this is list of DocuSign accounts associated with the current user.
        //	 * <br><b>name</b>: the user's full name.
        //	 * <br><b>givenName</b>: the user's given name.
        //	 * <br><b>familyName</b>: the user's family name.
        //	 * <br><b>email</b>: the user's email address.
        //	 * <br><b>created</b>: the UTC DateTime when the user login was created.
        //	 *
        //	 * @see Account
        //	 *
        //	 */
        [DataContract]
        public class UserInfo : IEquatable<UserInfo>, IValidatableObject
        {
            /// <summary>
            /// Account model with the following properties:
            /// accountId: the account ID GUID.
            /// isDefault: whether this is the default account, when the user has access to multiple accounts.
            /// accountName: the human-readable name of the account.
            /// baseUri: the base URI associated with this account.
            /// It also tells which DocuSign data center the account is hosted on.
            /// </summary>
            [DataContract]
            public class Account : IEquatable<Account>, IValidatableObject
            {
                [DataMember(Name = "account_id", EmitDefaultValue = false)]
                private string account_id { get; set; }

                [DataMember(Name = "is_default", EmitDefaultValue = false)]
                private string is_default { get; set; }

                [DataMember(Name = "account_name", EmitDefaultValue = false)]
                private string account_name { get; set; }

                [DataMember(Name = "base_uri", EmitDefaultValue = false)]
                private string base_uri { get; set; }

                [DataMember(Name = "organization", EmitDefaultValue = false)]
                private Organization organization { get; set; }

                public string AccountId
                {
                    get { return this.account_id; }
                    set { this.account_id = value; }
                }

                public string IsDefault
                {
                    get { return this.is_default; }
                    set { this.is_default = value; }
                }

                [Obsolete("This method is deprecated. Use IsDefault property instead", false)]
                public string GetIsDefault()
                {
                    return this.is_default;
                }

                public string AccountName
                {
                    get { return this.account_name; }
                    set { this.account_name = value; }
                }

                public string BaseUri
                {
                    get { return this.base_uri; }
                    set { this.base_uri = value; }
                }

                [Obsolete("This method is deprecated. Use BaseUri property instead", false)]
                public string GetBaseUri()
                {
                    return this.base_uri;
                }

                public Organization Organization
                {
                    get { return this.organization; }
                    set { this.organization = value; }
                }

                public override bool Equals(object obj)
                {
                    return this.Equals(obj as Account);
                }

                public override int GetHashCode()
                {
                    unchecked // Overflow is fine, just wrap
                    {
                        int hash = 41;
                        if (this.account_id != null)
                            hash = hash * 59 + this.account_id.GetHashCode();
                        if (is_default != null)
                            hash = hash * 59 + this.is_default.GetHashCode();
                        if (account_name != null)
                            hash = hash * 59 + this.account_name.GetHashCode();
                        if (base_uri != null)
                            hash = hash * 59 + this.base_uri.GetHashCode();
                        if (organization != null)
                            hash = hash * 59 + this.organization.GetHashCode();
                        return hash;
                    }
                }

                public override string ToString()
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("class Account {\n");

                    sb.Append("    account_id: ").Append(ToIndentedString(account_id)).Append("\n");
                    sb.Append("    is_default: ").Append(ToIndentedString(is_default)).Append("\n");
                    sb.Append("    account_name: ").Append(ToIndentedString(account_name)).Append("\n");
                    sb.Append("    base_uri: ").Append(ToIndentedString(base_uri)).Append("\n");
                    sb.Append("    organization: ").Append(ToIndentedString(organization)).Append("\n");
                    return sb.ToString();
                }

                /**
                 * Convert the given object to string with each line indented by 4
                 * spaces (except the first line).
                 */
                private string ToIndentedString(object o)
                {
                    if (o == null)
                    {
                        return "null";
                    }
                    return o.ToString().Replace("\n", "\n    ");
                }

                public bool Equals(Account other)
                {
                    if (other == null)
                        return false;

                    return
                  (
                      this.account_id == other.account_id ||
                      this.account_id != null &&
                      this.account_id.Equals(other.account_id)
                  ) &&
                  (
                      this.account_name == other.account_name ||
                      this.account_name != null &&
                      this.account_name.Equals(other.account_name)
                  ) &&
                  (
                      this.is_default == other.is_default ||
                      this.is_default != null &&
                      this.is_default.Equals(other.is_default)
                  ) &&
                  (
                      this.base_uri == other.base_uri ||
                      this.base_uri != null &&
                      this.base_uri.Equals(other.base_uri)
                  ) &&
                  (
                      this.organization == other.organization ||
                      this.organization != null &&
                      this.organization.Equals(other.organization)
                  ); ;
                }

                /// <summary>
                /// Returns the JSON string presentation of the object
                /// </summary>
                /// <returns>JSON string presentation of the object</returns>
                public string ToJson()
                {
                    return JsonConvert.SerializeObject(this, Formatting.Indented);
                }

                public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
                {
                    yield break;
                }
            }

            /// <summary>
            /// Organization model with the following properties:
            /// organizationId: the organization ID GUID if DocuSign Org Admin is enabled.
            /// links: this is list of organization direct links associated with the DocuSign account.
            /// </summary>
            [DataContract]
            public class Organization : IEquatable<Organization>, IValidatableObject
            {
                [DataMember(Name = "organization_id", EmitDefaultValue = false)]
                private string organization_id { get; set; }

                [DataMember(Name = "links", EmitDefaultValue = false)]
                private List<Link> links { get; set; } = new List<Link>();

                public string OrganizationId
                {
                    get { return this.organization_id; }
                    set { this.organization_id = value; }
                }

                public List<Link> Links
                {
                    get
                    {
                        if (this.links == null)
                        {
                            this.links = new List<Link>();
                        }
                        return this.links;
                    }
                }

                public void AddLinks(Link link)
                {
                    if (this.links == null) { this.links = new List<Link>(); }
                    this.links.Add(link);
                }

                public override bool Equals(object obj)
                {
                    return this.Equals(obj as Organization);
                }

                public override int GetHashCode()
                {
                    unchecked // Overflow is fine, just wrap
                    {
                        int hash = 41;
                        if (this.organization_id != null)
                            hash = hash * 59 + this.organization_id.GetHashCode();
                        if (links != null)
                            hash = hash * 59 + this.links.GetHashCode();
                        return hash;
                    }
                }

                public override string ToString()
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("class Link {\n");

                    sb.Append("    organization_id: ").Append(ToIndentedString(organization_id)).Append("\n");
                    sb.Append("    links: ").Append(ToIndentedString(links)).Append("\n");
                    return sb.ToString();
                }

                private string ToIndentedString(object o)
                {
                    if (o == null)
                    {
                        return "null";
                    }
                    return o.ToString().Replace("\n", "\n    ");
                }

                public bool Equals(Organization other)
                {
                    if (other == null)
                        return false;

                    return
                  (
                      this.organization_id == other.organization_id ||
                      this.organization_id != null &&
                      this.organization_id.Equals(other.organization_id)
                  ) &&
                  (
                      this.links == other.links ||
                      this.links != null &&
                      this.links.Equals(other.links)
                  );
                }

                /// <summary>
                /// Returns the JSON string presentation of the object
                /// </summary>
                /// <returns>JSON string presentation of the object</returns>
                public string ToJson()
                {
                    return JsonConvert.SerializeObject(this, Formatting.Indented);
                }

                public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
                {
                    yield break;
                }
            }

            /// <summary>
            /// Link model with the following properties:
            /// rel: currently the only value is "self".
            /// href: the direct link of the organization.
            /// </summary>
            [DataContract]
            public class Link : IEquatable<Link>, IValidatableObject
            {
                [DataMember(Name = "rel", EmitDefaultValue = false)]
                private string rel { get; set; }

                [DataMember(Name = "href", EmitDefaultValue = false)]
                private string href { get; set; }

                public string Rel
                {
                    get { return this.rel; }
                    set { this.rel = value; }
                }

                public string Href
                {
                    get { return this.href; }
                    set { this.href = value; }
                }

                public override bool Equals(object obj)
                {
                    return this.Equals(obj as Link);
                }

                public override int GetHashCode()
                {
                    unchecked // Overflow is fine, just wrap
                    {
                        int hash = 41;
                        if (this.rel != null)
                            hash = hash * 59 + this.rel.GetHashCode();
                        if (href != null)
                            hash = hash * 59 + this.href.GetHashCode();
                        return hash;
                    }
                }

                public override string ToString()
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("class Link {\n");

                    sb.Append("    rel: ").Append(ToIndentedString(rel)).Append("\n");
                    sb.Append("    href: ").Append(ToIndentedString(href)).Append("\n");
                    return sb.ToString();
                }

                private string ToIndentedString(object o)
                {
                    if (o == null)
                    {
                        return "null";
                    }
                    return o.ToString().Replace("\n", "\n    ");
                }

                public bool Equals(Link other)
                {
                    if (other == null)
                        return false;

                    return
                  (
                      this.rel == other.rel ||
                      this.rel != null &&
                      this.rel.Equals(other.rel)
                  ) &&
                  (
                      this.href == other.href ||
                      this.href != null &&
                      this.href.Equals(other.href)
                  );
                }

                /// <summary>
                /// Returns the JSON string presentation of the object
                /// </summary>
                /// <returns>JSON string presentation of the object</returns>
                public string ToJson()
                {
                    return JsonConvert.SerializeObject(this, Formatting.Indented);
                }

                public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
                {
                    yield break;
                }
            }

            [DataMember(Name = "sub", EmitDefaultValue = false)]
            private string sub { get; set; }

            [DataMember(Name = "email", EmitDefaultValue = false)]
            private string email { get; set; }

            [DataMember(Name = "accounts", EmitDefaultValue = false)]
            private List<Account> accounts { get; set; } = new List<Account>();

            [DataMember(Name = "name", EmitDefaultValue = false)]
            private string name { get; set; }

            [DataMember(Name = "givenName", EmitDefaultValue = false)]
            private string givenName { get; set; }

            [DataMember(Name = "familyName", EmitDefaultValue = false)]
            private string familyName { get; set; }

            [DataMember(Name = "created", EmitDefaultValue = false)]
            private string created { get; set; }

            public string Sub
            {
                get { return this.sub; }
                set { this.sub = value; }
            }

            public string Email
            {
                get { return this.email; }
                set { this.email = value; }
            }

            public List<Account> Accounts
            {
                get { return this.accounts; }
            }

            public void AddAccount(Account account)
            {
                if (this.accounts == null) { this.accounts = new List<Account>(); }
                this.accounts.Add(account);
            }

            [Obsolete("This method is deprecated. Use Accounts property instead", false)]
            public List<Account> GetAccounts()
            {
                if (this.accounts == null) { this.accounts = new List<Account>(); }
                return this.accounts;
            }

            public string Name
            {
                get { return this.name; }
                set { this.name = value; }
            }

            public string GivenName
            {
                get { return this.givenName; }
                set { this.givenName = value; }
            }

            public string FamilyName
            {
                get { return this.familyName; }
                set { this.familyName = value; }
            }

            public string Created
            {
                get { return this.created; }
                set { this.created = value; }
            }

            public override bool Equals(object o)
            {
                return this.Equals(o as UserInfo);
            }

            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 41;
                    if (this.sub != null)
                        hash = hash * 59 + this.sub.GetHashCode();
                    if (email != null)
                        hash = hash * 59 + this.email.GetHashCode();
                    if (accounts != null)
                        hash = hash * 59 + this.accounts.GetHashCode();
                    if (name != null)
                        hash = hash * 59 + this.name.GetHashCode();
                    if (givenName != null)
                        hash = hash * 59 + this.givenName.GetHashCode();
                    if (familyName != null)
                        hash = hash * 59 + this.familyName.GetHashCode();
                    if (created != null)
                        hash = hash * 59 + this.created.GetHashCode();
                    return hash;
                }
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("class UserInfo {\n");

                sb.Append("    sub: ").Append(ToIndentedString(sub)).Append("\n");
                sb.Append("    email: ").Append(ToIndentedString(email)).Append("\n");
                sb.Append("    name: ").Append(ToIndentedString(name)).Append("\n");
                sb.Append("    givenName: ").Append(ToIndentedString(givenName)).Append("\n");
                sb.Append("    familyName: ").Append(ToIndentedString(familyName)).Append("\n");
                sb.Append("    created: ").Append(ToIndentedString(created)).Append("\n");
                sb.Append("    accounts: ").Append(ToIndentedString(accounts)).Append("\n");
                return sb.ToString();
            }

            private string ToIndentedString(object o)
            {
                if (o == null)
                {
                    return "null";
                }
                return o.ToString().Replace("\n", "\n    ");
            }

            /// <summary>
            /// Returns the JSON string presentation of the object
            /// </summary>
            /// <returns>JSON string presentation of the object</returns>
            public string ToJson()
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                yield break;
            }

            public bool Equals(UserInfo other)
            {
                if (other == null)
                    return false;

                return
                    (
                        this.accounts == other.accounts ||
                        this.accounts != null &&
                        this.accounts.Equals(other.accounts)
                    ) &&
                    (
                        this.created == other.created ||
                        this.created != null &&
                        this.created.Equals(other.created)
                    ) &&
                    (
                        this.email == other.email ||
                        this.email != null &&
                        this.email.Equals(other.email)
                    ) &&
                    (
                        this.familyName == other.familyName ||
                        this.familyName != null &&
                        this.familyName.Equals(other.familyName)
                    ) &&
                    (
                        this.givenName == other.givenName ||
                        this.givenName != null &&
                        this.givenName.Equals(other.givenName)
                    ) &&
                    (
                        this.name == other.name ||
                        this.name != null &&
                        this.name.Equals(other.name)
                    ) &&
                    (
                        this.sub == other.sub ||
                        this.sub != null &&
                        this.sub.Equals(other.sub)
                    );
            }
        }

        public class OAuthToken
        {
            public string access_token { get; set; }

            public string token_type { get; set; }

            public string refresh_token { get; set; }

            public int? expires_in { get; set; }
        }
    }
}
