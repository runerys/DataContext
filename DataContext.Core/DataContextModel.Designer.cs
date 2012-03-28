﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("DataContextModel", "ContextPerson", "Context", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(DataContext.Core.Context), "Person", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(DataContext.Core.Person), true)]

#endregion

namespace DataContext.Core
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class DataContextModelContainer : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new DataContextModelContainer object using the connection string found in the 'DataContextModelContainer' section of the application configuration file.
        /// </summary>
        public DataContextModelContainer() : base("name=DataContextModelContainer", "DataContextModelContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new DataContextModelContainer object.
        /// </summary>
        public DataContextModelContainer(string connectionString) : base(connectionString, "DataContextModelContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new DataContextModelContainer object.
        /// </summary>
        public DataContextModelContainer(EntityConnection connection) : base(connection, "DataContextModelContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Context> Contexts
        {
            get
            {
                if ((_Contexts == null))
                {
                    _Contexts = base.CreateObjectSet<Context>("Contexts");
                }
                return _Contexts;
            }
        }
        private ObjectSet<Context> _Contexts;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Person> People
        {
            get
            {
                if ((_People == null))
                {
                    _People = base.CreateObjectSet<Person>("People");
                }
                return _People;
            }
        }
        private ObjectSet<Person> _People;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Contexts EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToContexts(Context context)
        {
            base.AddObject("Contexts", context);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the People EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToPeople(Person person)
        {
            base.AddObject("People", person);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="DataContextModel", Name="Context")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Context : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Context object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="isTest">Initial value of the IsTest property.</param>
        /// <param name="dateCreated">Initial value of the DateCreated property.</param>
        public static Context CreateContext(global::System.Int32 id, global::System.String name, global::System.Boolean isTest, global::System.DateTime dateCreated)
        {
            Context context = new Context();
            context.Id = id;
            context.Name = name;
            context.IsTest = isTest;
            context.DateCreated = dateCreated;
            return context;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean IsTest
        {
            get
            {
                return _IsTest;
            }
            set
            {
                OnIsTestChanging(value);
                ReportPropertyChanging("IsTest");
                _IsTest = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IsTest");
                OnIsTestChanged();
            }
        }
        private global::System.Boolean _IsTest;
        partial void OnIsTestChanging(global::System.Boolean value);
        partial void OnIsTestChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateCreated
        {
            get
            {
                return _DateCreated;
            }
            set
            {
                OnDateCreatedChanging(value);
                ReportPropertyChanging("DateCreated");
                _DateCreated = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DateCreated");
                OnDateCreatedChanged();
            }
        }
        private global::System.DateTime _DateCreated;
        partial void OnDateCreatedChanging(global::System.DateTime value);
        partial void OnDateCreatedChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="DataContextModel", Name="Person")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Person : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Person object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="firstName">Initial value of the FirstName property.</param>
        /// <param name="lastName">Initial value of the LastName property.</param>
        /// <param name="contextId">Initial value of the ContextId property.</param>
        public static Person CreatePerson(global::System.Int32 id, global::System.String firstName, global::System.String lastName, global::System.Int32 contextId)
        {
            Person person = new Person();
            person.Id = id;
            person.FirstName = firstName;
            person.LastName = lastName;
            person.ContextId = contextId;
            return person;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                OnFirstNameChanging(value);
                ReportPropertyChanging("FirstName");
                _FirstName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("FirstName");
                OnFirstNameChanged();
            }
        }
        private global::System.String _FirstName;
        partial void OnFirstNameChanging(global::System.String value);
        partial void OnFirstNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                OnLastNameChanging(value);
                ReportPropertyChanging("LastName");
                _LastName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("LastName");
                OnLastNameChanged();
            }
        }
        private global::System.String _LastName;
        partial void OnLastNameChanging(global::System.String value);
        partial void OnLastNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ContextId
        {
            get
            {
                return _ContextId;
            }
            set
            {
                OnContextIdChanging(value);
                ReportPropertyChanging("ContextId");
                _ContextId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ContextId");
                OnContextIdChanged();
            }
        }
        private global::System.Int32 _ContextId;
        partial void OnContextIdChanging(global::System.Int32 value);
        partial void OnContextIdChanged();

        #endregion
    
    }

    #endregion
    
}
