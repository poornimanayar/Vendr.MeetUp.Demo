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
	// Mixin Content Type with alias "checkoutStep"
	/// <summary>Checkout Step</summary>
	public partial interface ICheckoutStep : IPublishedContent
	{
		/// <summary>Can Go Back</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		bool CanGoBack { get; }

		/// <summary>Step Name</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		string StepName { get; }
	}

	/// <summary>Checkout Step</summary>
	[PublishedModel("checkoutStep")]
	public partial class CheckoutStep : PublishedContentModel, ICheckoutStep
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public new const string ModelTypeAlias = "checkoutStep";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<CheckoutStep, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public CheckoutStep(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Can Go Back: Toggle to indicate whether the user can go back from the current checkout step
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		[ImplementPropertyType("canGoBack")]
		public bool CanGoBack => GetCanGoBack(this);

		/// <summary>Static getter for Can Go Back</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public static bool GetCanGoBack(ICheckoutStep that) => that.Value<bool>("canGoBack");

		///<summary>
		/// Step Name: Step name to display on the page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		[ImplementPropertyType("stepName")]
		public string StepName => GetStepName(this);

		/// <summary>Static getter for Step Name</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public static string GetStepName(ICheckoutStep that) => that.Value<string>("stepName");
	}
}
