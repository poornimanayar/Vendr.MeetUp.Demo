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
	// Mixin Content Type with alias "productConfig"
	/// <summary>Product Config</summary>
	public partial interface IProductConfig : IPublishedContent
	{
		/// <summary>Price</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		global::Vendr.Web.Models.PricePropertyValue Price { get; }

		/// <summary>Sku</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		string Sku { get; }

		/// <summary>Stock</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		global::System.Nullable<decimal> Stock { get; }
	}

	/// <summary>Product Config</summary>
	[PublishedModel("productConfig")]
	public partial class ProductConfig : PublishedContentModel, IProductConfig
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public new const string ModelTypeAlias = "productConfig";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ProductConfig, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public ProductConfig(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Price: Vendr specific
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		[ImplementPropertyType("price")]
		public global::Vendr.Web.Models.PricePropertyValue Price => GetPrice(this);

		/// <summary>Static getter for Price</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public static global::Vendr.Web.Models.PricePropertyValue GetPrice(IProductConfig that) => that.Value<global::Vendr.Web.Models.PricePropertyValue>("price");

		///<summary>
		/// Sku: Vendr specific
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		[ImplementPropertyType("sku")]
		public string Sku => GetSku(this);

		/// <summary>Static getter for Sku</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public static string GetSku(IProductConfig that) => that.Value<string>("sku");

		///<summary>
		/// Stock: Vendr specific
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		[ImplementPropertyType("stock")]
		public global::System.Nullable<decimal> Stock => GetStock(this);

		/// <summary>Static getter for Stock</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.0")]
		public static global::System.Nullable<decimal> GetStock(IProductConfig that) => that.Value<global::System.Nullable<decimal>>("stock");
	}
}
