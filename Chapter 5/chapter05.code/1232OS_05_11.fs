namespace chapter05.code

module _1232OS_05_11 =

    open System
    open System.IO
    open System.Data
    open System.Data.Linq
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq

    type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Chapter05;Integrated Security=SSPI;">

    open NUnit.Framework
    open FsUnit

    [<SetUp>]
    let ``insert stub data into cms`` () : unit =
        let db = dbSchema.GetDataContext()

        // truncate the tables
        db.Truncate() |> ignore

        // create page type
        let contentPage = new dbSchema.ServiceTypes.PageType(Name = "ContentPage")
        
        // create property types
        let stringPropertyType = new dbSchema.ServiceTypes.PropertyType(Name = "PropertyString")
        let booleanPropertyType = new dbSchema.ServiceTypes.PropertyType(Name = "PropertyBoolean")
        let htmlPropertyType = new dbSchema.ServiceTypes.PropertyType(Name = "PropertyHtml")

        // create properties for content page
        let pageNameProperty = new dbSchema.ServiceTypes.Property(Name = "PageName", PageType = contentPage, PropertyType = stringPropertyType)
        let visibleInMenuProperty = new dbSchema.ServiceTypes.Property(Name = "VisibleInMenu", PageType = contentPage, PropertyType = booleanPropertyType)
        let mainBodyProperty = new dbSchema.ServiceTypes.Property(Name = "MainBody", PageType = contentPage, PropertyType = htmlPropertyType)
        
        // insert
        db.Property.InsertAllOnSubmit [pageNameProperty; visibleInMenuProperty; mainBodyProperty]
        db.DataContext.SubmitChanges()

    [<Test>]
    let ``truncate should clear all tables`` () =
        // arrange
        let db = dbSchema.GetDataContext()

        // act
        db.Truncate() |> ignore

        // assert
        let properties = query { for property in db.Property do select property } |> Seq.toList
        properties.IsEmpty |> should be True

        let propertyTypes = query { for propertyType in db.PropertyType do select propertyType} |> Seq.toList
        propertyTypes.IsEmpty |> should be True

        let pageTypes = query { for pageType in db.PageType do select pageType } |> Seq.toList
        pageTypes.IsEmpty |> should be True
