# Geo Location

# Interfaces
* IGeoLocation - Interface to store a Geo Location with decimal and DMS Coordinates
* IGeoCoordinate - Interface to store a Geo Coordinate in (DMS) degrees, minute, second (and millisecond) format. 
* IGeoCoordinateMath - Interface to convert decimal coordinates into DMS (IGeoCoordinate) coordinates.
* IGeoCoordinateFormatter - Interface to format a DMS coordinate to string

# Factory Interfaces
* IGeoFactory - Interface to create a IGeoLocation and IGeoCircle instances

# Examples

```csharp
var geoFactory  = new GeoFactory();
var location = geoFactory.CreateLocation(52.518639, 13.376090);

// access longitude and latitude in decimal and DMS format via properties.
// Note: calculation of DMS coordinates (IGeoCoordinateMath) and formatting (IGeoCoordinateFormatter) is exchangeable
```

Format Geo Coordinates (DMS) to string
```csharp
var location = new GeoFactory().CreateLocation(52.518639, 13.376090);

// ToString is overwritten and uses the default formatter
var latDMSStr = location.LatitudeDMS.ToString();  // N 52° 31' 07.1"
var lonDMSStr = location.LongitudeDMS.ToString(); // E 13° 22' 33.9"
```

Custom formatting of Geo Coordinates (DMS)
```csharp
var location = new GeoFactory().CreateLocation(52.518639, 13.376090);

// TODO: implement your own DMS coordinate formatter and replace the default formatter
IGeoCoordinateFormatter formatter = new DefaultGeoCoordinateFormatter();

var latDMSViaFormatter = formatter.WriteToString(location.LatitudeDMS); // N 52° 31' 07.1"
var lonDMSViaFormatter = formatter.WriteToString(location.LatitudeDMS); // E 13° 22' 33.9"
```
