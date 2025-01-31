﻿// <auto-generated />
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Permissions.Domain.Entities;
using Value.Objects.Helper.FluentApiConverters.Primitives;
using Value.Objects.Helper.Values.Primitives;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Persistence.PreCopiledEntities
{
    internal partial class PermissionEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Permissions.Domain.Entities.Permission",
                typeof(Permission),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(int),
                propertyInfo: typeof(Permission).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permission).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                afterSaveBehavior: PropertySaveBehavior.Throw,
                sentinel: 0);
            id.TypeMapping = IntTypeMapping.Default.Clone(
                comparer: new ValueComparer<int>(
                    (int v1, int v2) => v1 == v2,
                    (int v) => v,
                    (int v) => v),
                keyComparer: new ValueComparer<int>(
                    (int v1, int v2) => v1 == v2,
                    (int v) => v,
                    (int v) => v),
                providerValueComparer: new ValueComparer<int>(
                    (int v1, int v2) => v1 == v2,
                    (int v) => v,
                    (int v) => v));
            id.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            var employeeLastName = runtimeEntityType.AddProperty(
                "EmployeeLastName",
                typeof(StringObject),
                propertyInfo: typeof(Permission).GetProperty("EmployeeLastName", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permission).GetField("<EmployeeLastName>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                maxLength: 150,
                valueConverter: new StringObjectConverter());
            employeeLastName.TypeMapping = SqlServerStringTypeMapping.Default.Clone(
                comparer: new ValueComparer<StringObject>(
                    (StringObject v1, StringObject v2) => object.Equals(v1, v2),
                    (StringObject v) => v.GetHashCode(),
                    (StringObject v) => v),
                keyComparer: new ValueComparer<StringObject>(
                    (StringObject v1, StringObject v2) => object.Equals(v1, v2),
                    (StringObject v) => v.GetHashCode(),
                    (StringObject v) => v),
                providerValueComparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "nvarchar(150)",
                    size: 150,
                    unicode: true,
                    dbType: System.Data.DbType.String),
                converter: new ValueConverter<StringObject, string>(
                    (StringObject instance) => instance.Value,
                    (string value) => StringObject.Create(value)),
                jsonValueReaderWriter: new JsonConvertedValueReaderWriter<StringObject, string>(
                    JsonStringReaderWriter.Instance,
                    new ValueConverter<StringObject, string>(
                        (StringObject instance) => instance.Value,
                        (string value) => StringObject.Create(value))));
            employeeLastName.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var employeeName = runtimeEntityType.AddProperty(
                "EmployeeName",
                typeof(StringObject),
                propertyInfo: typeof(Permission).GetProperty("EmployeeName", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permission).GetField("<EmployeeName>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                maxLength: 150,
                valueConverter: new StringObjectConverter());
            employeeName.TypeMapping = SqlServerStringTypeMapping.Default.Clone(
                comparer: new ValueComparer<StringObject>(
                    (StringObject v1, StringObject v2) => object.Equals(v1, v2),
                    (StringObject v) => v.GetHashCode(),
                    (StringObject v) => v),
                keyComparer: new ValueComparer<StringObject>(
                    (StringObject v1, StringObject v2) => object.Equals(v1, v2),
                    (StringObject v) => v.GetHashCode(),
                    (StringObject v) => v),
                providerValueComparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "nvarchar(150)",
                    size: 150,
                    unicode: true,
                    dbType: System.Data.DbType.String),
                converter: new ValueConverter<StringObject, string>(
                    (StringObject instance) => instance.Value,
                    (string value) => StringObject.Create(value)),
                jsonValueReaderWriter: new JsonConvertedValueReaderWriter<StringObject, string>(
                    JsonStringReaderWriter.Instance,
                    new ValueConverter<StringObject, string>(
                        (StringObject instance) => instance.Value,
                        (string value) => StringObject.Create(value))));
            employeeName.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var permissionDateOnUtc = runtimeEntityType.AddProperty(
                "PermissionDateOnUtc",
                typeof(DateTimeOffset),
                propertyInfo: typeof(Permission).GetProperty("PermissionDateOnUtc", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permission).GetField("<PermissionDateOnUtc>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
            permissionDateOnUtc.TypeMapping = SqlServerDateTimeOffsetTypeMapping.Default.Clone(
                comparer: new ValueComparer<DateTimeOffset>(
                    (DateTimeOffset v1, DateTimeOffset v2) => v1.EqualsExact(v2),
                    (DateTimeOffset v) => v.GetHashCode(),
                    (DateTimeOffset v) => v),
                keyComparer: new ValueComparer<DateTimeOffset>(
                    (DateTimeOffset v1, DateTimeOffset v2) => v1.EqualsExact(v2),
                    (DateTimeOffset v) => v.GetHashCode(),
                    (DateTimeOffset v) => v),
                providerValueComparer: new ValueComparer<DateTimeOffset>(
                    (DateTimeOffset v1, DateTimeOffset v2) => v1.EqualsExact(v2),
                    (DateTimeOffset v) => v.GetHashCode(),
                    (DateTimeOffset v) => v));
            permissionDateOnUtc.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var permissionTypeId = runtimeEntityType.AddProperty(
                "PermissionTypeId",
                typeof(int),
                propertyInfo: typeof(Permission).GetProperty("PermissionTypeId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permission).GetField("<PermissionTypeId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: 0);
            permissionTypeId.TypeMapping = IntTypeMapping.Default.Clone(
                comparer: new ValueComparer<int>(
                    (int v1, int v2) => v1 == v2,
                    (int v) => v,
                    (int v) => v),
                keyComparer: new ValueComparer<int>(
                    (int v1, int v2) => v1 == v2,
                    (int v) => v,
                    (int v) => v),
                providerValueComparer: new ValueComparer<int>(
                    (int v1, int v2) => v1 == v2,
                    (int v) => v,
                    (int v) => v));
            permissionTypeId.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var version = runtimeEntityType.AddProperty(
                "version",
                typeof(byte[]),
                nullable: true,
                concurrencyToken: true,
                valueGenerated: ValueGenerated.OnAddOrUpdate,
                beforeSaveBehavior: PropertySaveBehavior.Ignore,
                afterSaveBehavior: PropertySaveBehavior.Ignore);
            version.TypeMapping = SqlServerByteArrayTypeMapping.Default.Clone(
                comparer: new ValueComparer<byte[]>(
                    (Byte[] v1, Byte[] v2) => StructuralComparisons.StructuralEqualityComparer.Equals(v1, v2),
                    (Byte[] v) => StructuralComparisons.StructuralEqualityComparer.GetHashCode(v),
                    (Byte[] v) => v.ToArray()),
                keyComparer: new ValueComparer<byte[]>(
                    (Byte[] v1, Byte[] v2) => StructuralComparisons.StructuralEqualityComparer.Equals((object)v1, (object)v2),
                    (Byte[] v) => StructuralComparisons.StructuralEqualityComparer.GetHashCode((object)v),
                    (Byte[] source) => source.ToArray()),
                providerValueComparer: new ValueComparer<byte[]>(
                    (Byte[] v1, Byte[] v2) => StructuralComparisons.StructuralEqualityComparer.Equals((object)v1, (object)v2),
                    (Byte[] v) => StructuralComparisons.StructuralEqualityComparer.GetHashCode((object)v),
                    (Byte[] source) => source.ToArray()),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "rowversion",
                    size: 8),
                storeTypePostfix: StoreTypePostfix.None);
            version.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var key = runtimeEntityType.AddKey(
                new[] { id });
            runtimeEntityType.SetPrimaryKey(key);

            var index = runtimeEntityType.AddIndex(
                new[] { permissionTypeId });

            var index0 = runtimeEntityType.AddIndex(
                new[] { employeeName, employeeLastName, permissionTypeId });

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("PermissionTypeId") },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                required: true);

            var permissionType = declaringEntityType.AddNavigation("PermissionType",
                runtimeForeignKey,
                onDependent: true,
                typeof(PermissionType),
                propertyInfo: typeof(Permission).GetProperty("PermissionType", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permission).GetField("<PermissionType>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", "permissions");
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "Permission");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
