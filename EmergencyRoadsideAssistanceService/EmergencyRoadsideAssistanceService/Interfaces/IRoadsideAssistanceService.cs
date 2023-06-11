using EmergencyRoadsideAssistanceService.Common;
using EmergencyRoadsideAssistanceService.DTOs;

namespace EmergencyRoadsideAssistanceService.Interfaces
{
    public interface IRoadsideAssistanceService
    {

        /// <summary>
        /// This method is used to update the location of the roadside assistance service provider.
        /// </summary>
        /// <param name="assistant">represents the roadside assistance service provider</param>
        /// <param name="assistantLocation">represents the location of the roadside assistant</param>
        Task UpdateAssistantLocation(Assistant assistant, Geolocation assistantLocation);

        /// <summary>
        /// This method returns a collection of roadside assistants ordered by their distance from the input geo location.
        /// </summary>
        /// <param name="geolocation">geolocation from which to search for assistants</param>
        /// <param name="limit">the number of assistants to return</param>
        /// <returns>a sorted collection of assistants ordered ascending by distance from geoLocation</returns>
        Task<SortedSet<Assistant>> FindNearestAssistants(Geolocation geolocation, int limit);

        /// <summary>
        /// This method reserves an assistant for a Geico customer that is stranded on the roadside due to a disabled vehicle.
        /// </summary>
        /// <param name="customer">Represents a Geico customer</param>
        /// <param name="customerLocation">Location of the customer</param>
        /// <returns>The Assistant that is on their way to help. Note that this is optional. So there is a possibility that there might not be any assistants on their way for help</returns>
        Task<Optional<Assistant>> ReserveAssistant(Customer customer, Geolocation customerLocation);

        /// <summary>
        /// This method releases an assistant either after they have completed work, or the customer no longer needs help.
        /// </summary>
        /// <param name="customer">Represents a Geico customer</param>
        /// <param name="assistant">An assistant that was previously reserved by the customer</param>
        Task ReleaseAssistant(Customer customer, Assistant assistant);

    }
}
