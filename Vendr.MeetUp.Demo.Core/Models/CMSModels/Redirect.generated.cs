//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v8.9.0
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder.Embedded;

namespace Vendr.MeetUp.Demo.CMSModels
{
	// Mixin Content Type with alias "redirect"
	/// <summary>Redirect</summary>
	public partial interface IRedirect : IPublishedContent
	{
		/// <summary>Redirect Page</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		global::Umbraco.Core.Models.PublishedContent.IPublishedContent UmbracoRedirect { get; }
	}

	/// <summary>Redirect</summary>
	[PublishedModel("redirect")]
	public partial class Redirect : PublishedContentModel, IRedirect
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public new const string ModelTypeAlias = "redirect";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Redirect, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public Redirect(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Redirect Page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		[ImplementPropertyType("umbracoRedirect")]
		public global::Umbraco.Core.Models.PublishedContent.IPublishedContent UmbracoRedirect => GetUmbracoRedirect(this);

		/// <summary>Static getter for Redirect Page</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public static global::Umbraco.Core.Models.PublishedContent.IPublishedContent GetUmbracoRedirect(IRedirect that) => that.Value<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>("umbracoRedirect");
	}
}
