LinqContrib
====================

This project aims to provide useful additions to the existing LINQ operators available out-of-the-box on the .NET Framework.

Usage
-----

The library consists of extension methods to *IEnumerable* and to *IEnumerable&lt;T&gt;*, so you simply need to import the *LinqContrib* namespace and take advantage of the new operators.

Examples
-----

    controls.AnyOfType<UserControl>()	// shorthand for controls.OfType<UserControl>().Any()

    controls.NotOfType<UserControl>()	// shorthand for controls.Where(control => !(control is UserControl))


