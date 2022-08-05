import { IGetTermsContent } from "../../../Redux/States/Content/getTermsContentState";

export const GetTermsContentDefault: IGetTermsContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        items: []
    }
}
