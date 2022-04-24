import { IGetAccountContent } from "../../States/Content/getAccountContentState";

export const GetAccountContentDefault: IGetAccountContent = 
{
    isLoading: false,
    content: 
    {
        sectionAccessDenied: 
        {
            accessDeniedCaption: "",
            accessDeniedPrompt: "",
            homeButtonText: ""        
        },
        sectionBasicInformation: 
        {
            caption: "",
            labelUserId: "",
            labelUserAlias: "",
            labelFirstName: "",
            labelLastName: "",
            labelShortBio: "",
            labelUserAvatar: "",
            updateButtonText: "",
            uploadAvatarButtonText: ""
        }
    }
}
