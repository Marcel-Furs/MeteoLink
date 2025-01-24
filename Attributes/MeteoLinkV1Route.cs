using Microsoft.AspNetCore.Mvc;

namespace MeteoLink.Attributes
{
    public class MeteoLinkV1Route : RouteAttribute
    {
        public MeteoLinkV1Route() : base("api/v1/[controller]") 
        { }
    }
}
