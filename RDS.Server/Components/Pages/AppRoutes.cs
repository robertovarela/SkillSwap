namespace RDS.Server.Components.Pages;

public static class AppRoutes
{
    public const string Categories = "/Categories";
    public const string CategoriesCreate = "/Categories/Create";
    public const string CategoriesEdit = "/Categories/Edit/{id}";

    public const string Messages = "/Messages";
    public const string MessagesCreate = "/Messages/Create";
    public const string MessagesAnswer = "/Messages/Answer/{id}";
    public const string MessagesDetails = "/Messages/Details/{id}";

    public const string ProfesionalProfiles = "/ProfessionalProfiles";
    public const string ProfesionalProfilesCreate = "/ProfessionalProfiles/Create";
    public const string ProfesionalProfilesEdit = "/ProfessionalProfiles/Edit/{id}";

    public const string Reviews = "/Reviews";
    public const string ReviewsCreate = "/Reviews/Create";
    public const string ReviewsEdit = "/Reviews/Edit/{id}";

    public const string ServicesOffered = "/ServicesOffered";
    public const string ServicesOfferedCreate = "/ServicesOffered/Create";
    public const string ServicesOfferedEdit = "/ServicesOffered/Edit/{id}";

    public const string ServiceRequests = "/ServiceRequests";
    public const string ServiceRequestsCreate = "/ServiceRequests/Create";
    public const string ServiceRequestsEdit = "/ServiceRequests/Edit/{id}";

    public const string SkillSwapRequests = "/SkillSwap";
    public const string SkillSwapRequestsProposal = "/SkillSwap/Proposal/{id}";
    public const string SkillSwapRequestsCreate = "/SkillSwap/Create";
    public const string SkillSwapRequestsListProposal = "/SkillSwap/ListProposal";
    public const string SkillSwapRequestsOfferEdit = "/SkillSwap/Edit/Offer/{id}";
    public const string SkillSwapRequestsDetailsOffer = "/SkillSwap/Details/Offer/{id}/{ProfessionalBId}/{ProfessionalAId}";

    public const string SkillSwapRequestsReceiveEdit = "/SkillSwap/Edit/Receive/{id}";


    public const string Home = "/";
    public const string Start = "/Start";

    public const string Login = "/Account/Login";
    public const string Register = "/Account/Register";
    public const string UserNotFound = "/UserNotFound";

    // Otras rutas
}