//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v8.9.1
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
	/// <summary>Checkout Payment Method</summary>
	[PublishedModel("checkoutPaymentMethod")]
	public partial class CheckoutPaymentMethod : PublishedContentModel, ICheckoutStep, IContentBase, INavigationBase
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public new const string ModelTypeAlias = "checkoutPaymentMethod";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<CheckoutPaymentMethod, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public CheckoutPaymentMethod(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Can Go Back: Toggle to indicate whether the user can go back from the current checkout step
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		[ImplementPropertyType("canGoBack")]
		public bool CanGoBack => global::Vendr.MeetUp.Demo.CMSModels.CheckoutStep.GetCanGoBack(this);

		///<summary>
		/// Step Name: Step name to display on the page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		[ImplementPropertyType("stepName")]
		public string StepName => global::Vendr.MeetUp.Demo.CMSModels.CheckoutStep.GetStepName(this);

		///<summary>
		/// Content
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		[ImplementPropertyType("bodyText")]
		public global::Newtonsoft.Json.Linq.JToken BodyText => global::Vendr.MeetUp.Demo.CMSModels.ContentBase.GetBodyText(this);

		///<summary>
		/// Page Title: The title of the page, this is also the first text in a google search result. The ideal length is between 40 and 60 characters
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		[ImplementPropertyType("pageTitle")]
		public string PageTitle => global::Vendr.MeetUp.Demo.CMSModels.ContentBase.GetPageTitle(this);

		///<summary>
		/// Keywords: Keywords that describe the content of the page. This is considered optional since most modern search engines don't use this anymore
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		[ImplementPropertyType("keywords")]
		public global::System.Collections.Generic.IEnumerable<string> Keywords => global::Vendr.MeetUp.Demo.CMSModels.NavigationBase.GetKeywords(this);

		///<summary>
		/// Description: A brief description of the content on your page. This text is shown below the title in a google search result and also used for Social Sharing Cards. The ideal length is between 130 and 155 characters
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		[ImplementPropertyType("seoMetaDescription")]
		public string SeoMetaDescription => global::Vendr.MeetUp.Demo.CMSModels.NavigationBase.GetSeoMetaDescription(this);

		///<summary>
		/// Hide in Navigation: If you don't want this page to appear in the navigation, check this box
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		[ImplementPropertyType("umbracoNavihide")]
		public bool UmbracoNavihide => global::Vendr.MeetUp.Demo.CMSModels.NavigationBase.GetUmbracoNavihide(this);
	}
}
