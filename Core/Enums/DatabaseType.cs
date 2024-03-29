﻿// ReSharper disable InconsistentNaming
namespace Core.Enums;

/// <summary>
/// Database Type
/// </summary>
public enum DatabaseType
{
    /// <summary>
    /// Indicated that no Database is set. Typically Used to indicate an uninitialized state.
    /// </summary>
    None,
    /// <summary>
    /// SQLite
    /// </summary>
    SQLite,
    /// <summary>
    /// Microsoft SQL-Server
    /// </summary>
    SQLServer,
    /// <summary>
    /// MySQL/
    /// </summary>
    MySQL,
    /// <summary>
    /// PostgreSQL - Open Source Relational Database
    /// </summary>
    PostgreSQL
}