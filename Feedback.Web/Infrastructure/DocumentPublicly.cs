using System;

namespace LiteReg.Web.Infrastructure
{
    /// <summary>
    /// Used to opt-in to public API documentation.  API methods remain undocumented by default until this attribute
    /// is applied to the action method.
    /// </summary>
    public class DocumentPublicly : Attribute { }
}