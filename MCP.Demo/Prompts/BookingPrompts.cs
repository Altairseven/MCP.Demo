using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MCP.Demo.Prompts;

[McpServerPromptType]
public static class BookingPrompts
{
    [McpServerPrompt(Name = "booking_workflow_guide"), Description("Provides guidance on how to use the booking tools effectively")]
    public static string BookingWorkflowGuide()
    {
        return """
        # Booking Tools Workflow Guide
        
        ## Overview
        This system provides three main tools for managing apartment bookings. Users can browse apartments freely, and only need to register when ready to make a booking.
        
        ## Step 1: Search for Available Apartments (get_apartments)
        Start by searching for apartments available during desired dates.
        
        **Required Parameters:**
        - from: Start date (DateOnly format: YYYY-MM-DD)
        - to: End date (DateOnly format: YYYY-MM-DD)
        - Country: Country name to filter by (case-insensitive)
        
        **Returns:** A list of available apartments with their details including:
        - Apartment ID (required for booking)
        - Name, Description
        - Price and Currency
        - Address information
        
        **Note:** Users can search for apartments without being registered. Registration is only required when they decide to book.
        
        ## Step 2: Register User When Ready to Book (register_user)
        ONLY when the user wants to proceed with a booking, ask for registration details.

        **Important:** YOU NEED TO ASK the user for his details to register him, you cant invent or suggest a password or email, or first or last name.
        
        **Required Parameters to Ask User:**
        - firstName: User's first name
        - lastName: User's last name
        - email: User's email address
        - password: User's password
        
        **Returns:** A userId (GUID) that will be used immediately for booking
        
        **Important:** The user credentials are cached for 3 days. The userId must be used within this timeframe.
        
        ## Step 3: Book the Selected Apartment (book_apartment)
        Immediately after registration, proceed with the booking using the userId just received.
        
        **Required Parameters:**
        - userId: The GUID received from registration (Step 2)
        - apartmentId: The GUID of the desired apartment (from Step 1)
        - from: Check-in date (DateOnly format: YYYY-MM-DD)
        - to: Check-out date (DateOnly format: YYYY-MM-DD)
        
        **Returns:** A booking ID (GUID) confirming the reservation
        
        ## Important Notes:
        
        1. **Browse First, Register Later:** Allow users to search and browse apartments without requiring registration. Only ask for registration details when they explicitly want to book.
        
        2. **Date Availability:** The dates used for booking MUST match dates where the apartment was shown as available in the search results.
        
        3. **User Cache:** User credentials are cached for 3 days. If you encounter authentication errors:
           - The user may need to be registered again
           - Ensure the userId is correct and recent
        
        4. **Workflow Order:** 
           - First: get_apartments (browse available options)
           - Second: register_user (ONLY when user decides to book)
           - Third: book_apartment (immediately after registration)
        
        5. **Date Format:** All dates must be in YYYY-MM-DD format (e.g., "2025-09-15")
        
        6. **Error Handling:** If booking fails, try:
           - Different dates (the apartment may not be available for those specific dates)
           - Verifying the apartmentId exists in the search results
           - Re-registering the user if the cache has expired
        
        ## Example Workflow:
        
        User: "Show me apartments in Spain for September 1-5, 2025"
        1. Search: get_apartments("2025-09-01", "2025-09-05", "Spain")
           → Returns list with apartmentId: "xyz-789-uvw", name: "Beachfront Villa"
        
        User: "I want to book the Beachfront Villa"
        2. Ask for registration: "To complete your booking, I'll need some information:"
           - First name: "John"
           - Last name: "Doe"
           - Email: "john@example.com"
           - Password: "Pass123"
           
        3. Register: register_user("John", "Doe", "john@example.com", "Pass123")
           → Returns userId: "abc-123-def"
        
        4. Book: book_apartment("abc-123-def", "xyz-789-uvw", "2025-09-01", "2025-09-05")
           → Returns bookingId: "booking-456"
           
        Confirm: "Your booking is confirmed! Booking ID: booking-456"
        """;
    }
}
