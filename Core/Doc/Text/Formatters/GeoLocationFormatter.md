# GeoLocationFormatter

CoreLib makes it easy to create well formatted hyperlinks to show/search for the given location via selected cloud map providers.

## Intended Usage

* Use the extension methods for quick creation of the search links.
* Makes it easy to create links for **Google Maps**, **Bing Maps** and **OpenStreetMaps**.
* If you're forced to create many links: Use the formatters instead of the extension methods.

## Example

```c#
// your test location
const double latitudeOfBerlin  = 52.518639;
const double longitudeOfBerlin = 13.376090;

// create a location with a GeoLocationFactory
var geoFactory = new GeoFactory();
var locationOfBerlin = geoFactory.CreateLocation(latitudeOfBerlin, longitudeOfBerlin);

// create your search links for Google Maps, Bing Maps and OpenSteetMap
var googleMapsLink = locationOfBerlin.ToGoogleMapsLink();
var bingLink = locationOfBerlin.ToBingMapsLink();
var openStreetMapsLink = locationOfBerlin.ToOpenStreetMapsLink();
```

**Example Results:**

* https://www.google.com/maps/search/?api=1&query=52.518639,13.37609
* https://bing.com/maps/default.aspx?cp=52.518639~13.37609&lvl=16&dir=0&sty=a&sp=point.52.518639_13.37609
* http://www.openstreetmap.org/?mlat=52.518639&mlon=13.37609&zoom=17