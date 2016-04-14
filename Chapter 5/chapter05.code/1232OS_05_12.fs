namespace chapter05.code

module _1232OS_05_12 =

    open System
    open System.IO
    open System.Data
    open System.Data.Linq
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq
    open Microsoft.FSharp.Reflection

    type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Chapter05;Integrated Security=SSPI;", StoredProcedures = true>

    type ContentPage = { pageID : int; PageName : string; VisibleInMenu : bool; MainBody : string }

    // map dataRecord to pageType
    let convert<'pageType> (dataRecord : IDataRecord) = 
        let pageType = typeof<'pageType>
        let values = FSharpType.GetRecordFields(pageType) 
                     |> Array.map (fun field -> Convert.ChangeType(dataRecord.[field.Name], field.PropertyType))
        FSharpValue.MakeRecord(pageType, values) :?> 'pageType
       
       
    // get all pages of typeg
    let getPagesOfType<'pageType> (db : dbSchema.ServiceTypes.SimpleDataContextTypes.Chapter05) =
        seq {
            let command = db.Connection.CreateCommand()
            command.CommandText <- "GetPagesOfPageType"
            command.CommandType <- CommandType.StoredProcedure
            
            let pageTypeParameter = new System.Data.SqlClient.SqlParameter("PageType", typeof<'pageType>.Name)
            command.Parameters.Add(pageTypeParameter) |> ignore
            
            db.Connection.Open()

            try
                use reader = command.ExecuteReader()
                while reader.Read() do
                    yield (reader :> IDataRecord) |> convert<'pageType>

            finally
                db.Connection.Close()
        }
        

    open NUnit.Framework
    open FsUnit

    // create page function
    let createPage created author pageType = 
        new dbSchema.ServiceTypes.Page(Created = created, Author = author, PageType = pageType)

    // create property on page
    type dbSchema.ServiceTypes.Page with
        member this.addPropertyValue propertyDefinition value = 
            this.PropertyValue.Add(new dbSchema.ServiceTypes.PropertyValue(Value = value, Page = this, Property = propertyDefinition)) |> ignore
            this

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
        
        // create pages
        let startPage = 
            (((createPage DateTime.Now "Mikael Lundin" contentPage)
                .addPropertyValue pageNameProperty "Home")
                .addPropertyValue visibleInMenuProperty "true")
                .addPropertyValue mainBodyProperty "Welcome to my homepage"
         
        let aboutPage = 
            (((createPage DateTime.Now "Mikael Lundin" contentPage)
                .addPropertyValue pageNameProperty "About Me")
                .addPropertyValue visibleInMenuProperty "true")
                .addPropertyValue mainBodyProperty "I am a software developer"

        let servicesPage = 
            (((createPage DateTime.Now "Mikael Lundin" contentPage)
                .addPropertyValue pageNameProperty "My Services")
                .addPropertyValue visibleInMenuProperty "true")
                .addPropertyValue mainBodyProperty "I build high quality softare in F#"  

        // insert
        db.Page.InsertAllOnSubmit [startPage; aboutPage; servicesPage]
        db.DataContext.SubmitChanges()
        
    [<Test>]
    let ``get all pages of a page type`` () =
        // arrange
        let db = dbSchema.GetDataContext() 

        // act
        let page1 :: page2 :: page3 :: [] = getPagesOfType<ContentPage>(db) |> Seq.toList

        // assert
        page1.PageName |> should equal "Home"
        page1.VisibleInMenu |> should equal true
        page1.MainBody |> should equal "Welcome to my homepage"

        page2.PageName |> should equal "About me"
        page2.VisibleInMenu |> should equal true
        page2.MainBody |> should equal "I am a software developer"

        page3.PageName |> should equal "My Services"
        page3.VisibleInMenu |> should equal true
        page3.MainBody |> should equal "I build high quality softare in F#"
        